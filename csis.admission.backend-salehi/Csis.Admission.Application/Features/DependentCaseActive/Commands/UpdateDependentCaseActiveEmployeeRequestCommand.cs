using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Features.UniversityEducations.Commands;

namespace Csis.Admission.Application.Features.DependentCaseActive.Commands;

/// <summary>درخواست محاسبه و بروزرسانی خودکار وضعیت پرونده تکفل</summary>
public sealed record UpdateDependentCaseActiveEmployeeRequestCommand : IRequest<DependentActiveDeactiveReason>
{
    /// <summary>کد مرکز خدمات سرپرست</summary>
    public int Codm { get; init; }

    /// <summary>شناسه تکفل</summary>
    public long DependentId { get; init; }

    /// <summary>تایید نهایی درخواست توسط کارمند</summary>
    public bool Confirmed { get; set; }
}

internal sealed class UpdateDependentCaseActiveEmployeeRequestCommandHandler(
    ILogger<UpdateDependentCaseActiveEmployeeRequestCommandHandler> logger,
    IMediator mediator,
    IStudentRepository studentRepository,
    IStudentDependentRepository studentDependentRepository,
    IRepository<DependentEmployment> dependentEmploymentRepository,
    IRepository<DependentSummary, long> dependentSummaryRepository,
    IRepository<DependentActiveReason, short> dependentActiveReasonRepository,
    IRepository<DependentDeActiveReason, short> dependentDeActiveReasonRepository,
    IRepository<UniversityEducation> UuniversityEducationRepository,
        IRequestService requestService,
        ICurrentUserService currentUser) : IRequestHandler<UpdateDependentCaseActiveEmployeeRequestCommand, DependentActiveDeactiveReason>
{

    public async Task<DependentActiveDeactiveReason> Handle(UpdateDependentCaseActiveEmployeeRequestCommand request, CancellationToken cancellationToken) {
        var isEmployee = await currentUser.IsEmployee();
        if ( !isEmployee )
            throw new CommandValidationException("فقط کارمندان مجاز به استفاده از این سرویس هستند.");

        var student = await studentRepository.GetByCodm(request.Codm);
        var dependent = await dependentSummaryRepository.GetOneAsync(x => x.Id == request.DependentId, cancellationToken: cancellationToken);

        if ( dependent == null )
            throw new CommandValidationException("تکفل مورد نظر یافت نشد.");

        if ( dependent.IsDead )
            throw new CommandValidationException("امکان تغییر وضعیت تکفل فوت شده وجود ندارد.");

        var commission = (await studentRepository.GetDependentCommissionRequestByCodm(request.Codm)).Where(x => x.DependentId == request.DependentId && x.RequestStatus.In(8, 10, 12, 14)).ToList();

        var insurance = await studentRepository.GetTaminInsuranceByCodm(request.Codm);

        var dependentEducations = await UuniversityEducationRepository.GetAllAsync(x => x.Codm == request.Codm && x.DependentId == request.DependentId && x.InStudy, cancellationToken: cancellationToken);

        var azKarOftadegi = await studentDependentRepository.GetDependentPensionCommission(request.Codm, request.DependentId);

        var employement = await dependentEmploymentRepository.GetOneAsync(x => x.DependentId == request.DependentId, cancellationToken: cancellationToken);

        logger.LogInformation("", args: [commission, insurance, dependentEducations, azKarOftadegi, employement]);

        var calculate = await CalculateDependentReason(
            new DependentActiveReasonRequest {
                DependentActiveReason = (short?) (dependent.ActiveReason),
                DependentDeActiveReason = (short?) (dependent.DeActiveReason),
                DependentAge = GetAge(dependent.BirthDate.Value),

                //
                HasValidDependentCommission = commission.Any(x => x.CommissionValidityDate == null || x.CommissionValidityDate >= DateTime.Now.ToPersianInteger()),
                DependentCommissionValidityDate = commission?.Max(x => x.CommissionValidityDate),

                //
                IsDependentInStudy = dependentEducations.Any(x => (x.ValidityDate == null || x.ValidityDate >= DateTime.Now.ToPersianInteger()) && x.StudyLevel.In(StudyLevel.GraduateDiploma, StudyLevel.BachelorDegree, StudyLevel.MasterDegree, StudyLevel.DoctoralDegree)),
                DependentInStudyValidityDate = dependentEducations?.Max(x => x.ValidityDate),

                //
                DependentDeActiveReasonOnExpire = dependent.DeActiveReasonOnExpire,
                DependentExpireDate = dependent.DateExpire,

                //
                IsDependentAzkaroftade = azKarOftadegi != null && azKarOftadegi.Id > 0 && azKarOftadegi.ExpireDate >= DateTime.Now.ToPersianInteger(),
                DependentAzkaroftadeValidityDate = azKarOftadegi?.ExpireDate,

                DependentGender = (short) dependent.Gender,
                DependentIsActive = dependent.IsActive,
                DependentIsDead = dependent.IsDead,
                DependentIsMarried = dependent.IsMarried,
                DependentRelation = (short?) (dependent.Relation),
                DependentSingleStatus = (short?) (dependent.SingleStatus),
                DependentTransferredToCodm = dependent.TransferredToCodm,
                HasActiveTaminInsurance = insurance != null && insurance.Status == Common.Dtos.StudentTaminInsuranceResultDto.StatusEnum.Active,
                IsDependentEmployed = employement != null && employement.IsEmployee.HasValue && employement.IsEmployee.Value,
                StudentGender = (short?) (student.Gender),
                IsStudentBlock = student.IsBlock,
                HasActiveStudentCase = student.IsActive
            });

        if ( request.Confirmed ) {
            UpdateDependentCaseActiveEmployeeCommand payload = new(request.Codm, request.DependentId, (DependentDeActiveReasonEnum?) calculate.DeActiveReason, (DependentActiveReasonEnum?) calculate.ActiveReason);
            var requestCommand = new CreateRequestCommand(payload, RequestFlow.DirectRegistration, RequestType.UpdateDependentCaseActiveEmployee);

            var requestId = await requestService.Create(requestCommand, cancellationToken);
        }

        return calculate;
    }

    public async Task<DependentActiveDeactiveReason> CalculateDependentReason(DependentActiveReasonRequest input) {
        var res = new DependentActiveDeactiveReason();

        short[] manualReason = [
            .. (await dependentActiveReasonRepository.GetAllAsync(x => x.Type == "Manual")).Select(x => x.Id),
            .. (await dependentDeActiveReasonRepository.GetAllAsync(x => x.Type == "Manual")).Select(x => x.Id)
        ];

        short[] canceledReasons = [
            .. (await dependentActiveReasonRepository.GetAllAsync(x => x.Type == "Cancel")).Select(x => x.Id),
            .. (await dependentDeActiveReasonRepository.GetAllAsync(x => x.Type == "Cancel")).Select(x => x.Id)
            ];

        // اگر علت دستی باشد هیچ محاسبه ای انجام نمی شود و همان مقادیر قبلی برگشت داده می شود
        if ( input.DependentActiveReason.HasValue && (manualReason.Contains(input.DependentActiveReason.Value)) || (input.DependentDeActiveReason.HasValue && manualReason.Contains(input.DependentDeActiveReason.Value)) ) {
            res.IsActive = input.DependentIsActive;
            res.ActiveReason = input.DependentActiveReason;
            res.DeActiveReason = input.DependentDeActiveReason;
            res.DeActiveReasonOnExpire = input.DependentDeActiveReasonOnExpire;
            res.ExpireDate = input.DependentExpireDate;

            return res;
        }

        ///================================================================================
        /// باز بودن علت میخواهد چون قبلا اصلا ثبت نبوده
        /// بسته بودن علت می خواهد چون قبلا باز بوده
        /// علت کفالت را نال در نظر گرفتیم
        ///================================================================================
        if ( input.DependentIsDead ) {
            res.IsActive = false;
            res.DeActiveReason = 4; /*مرحوم*/
        } else if ( input.StudentGender == 2 && input.HasValidDependentCommission == false ) {
            res.IsActive = false;
            res.DeActiveReason = 34; /*صرفا عضو خانواده*/
        } else if ( input.IsDependentEmployed && input.DependentRelation != 1 /*همسر*/) {
            res.IsActive = false;
            res.DeActiveReason = 3; /*شاغل*/
        }

          /// طلاق همسر
          else if ( input.DependentSingleStatus == 3 /*طلاق */
                          && input.DependentGender == 2
                          && input.DependentRelation == 1 /*همسر*/
                      ) {
            res.IsActive = false;
            res.DeActiveReason = 7; /*  طلاق  */
        }
          /// ازدواج دختر
          else if ( input.DependentGender == 2 /* دختر */
                      && (input.DependentIsMarried == true || input.DependentSingleStatus == 3 /*طلاق همسر*/)
                      && input.HasValidDependentCommission == true
                          && input.DependentRelation.In(2, 4, 5) /* فرزند نوه فرزند خوانده */
                      ) {
            res.IsActive = false;
            res.DeActiveReason = 2; /* ازدواج  */
        } else if ( input.DependentAge > 28
                      && input.DependentGender == 1
                      && input.IsDependentAzkaroftade == false
                      && input.HasValidDependentCommission == false
                           && input.DependentRelation.In(2, 4, 5) /* فرزند نوه فرزند خوانده */
                  ) {
            res.IsActive = false;
            res.DeActiveReason = 1; /* اتمام سن  */
        } else if ( input.DependentAge > 19
          && input.DependentGender == 1
          && input.IsDependentAzkaroftade == false
          && input.HasValidDependentCommission == false
          && input.IsDependentInStudy == false
          && input.DependentRelation.In(2, 4, 5) /* فرزند نوه فرزند خوانده */
              ) {
            res.IsActive = false;
            res.DeActiveReason = 1; /* اتمام سن  */
        }



          /// بستن تکفل ذکور به علت ازدواج
          else if ( input.DependentGender == 1
              && input.DependentIsMarried == true
              && input.IsDependentAzkaroftade == false
              && input.HasValidDependentCommission == false
              && input.DependentRelation.In(2, 4, 5) /* فرزند نوه فرزند خوانده */
                ) {
            res.IsActive = false;
            res.DeActiveReason = 2; /* ازدواج  */
        }

          /// بستن تکفل ذکور به علت کد مستقل
          /// 
          else if ( input.DependentGender == 1
              && input.HasActiveStudentCase == true
          && input.IsDependentAzkaroftade == false
          && input.HasValidDependentCommission == false
          && input.DependentRelation.In(2, 4, 5) /* فرزند نوه فرزند خوانده */
          ) {
            res.IsActive = false;
            res.DeActiveReason = 6; /* ازدواج  */
        } else if ( input.DependentTransferredToCodm > 0
              && input.DependentRelation == 3 /*پدر مادر*/
          ) {
            res.IsActive = false;
            res.DeActiveReason = 27; /* انتقال به کد دیگر */
        }


          /// نسبت های غیر مجاز
          else if ( input.HasValidDependentCommission == false
                  && input.DependentRelation.In(1, 2, 3, 4, 5) == false
                      ) {
            res.IsActive = false;
            res.DeActiveReason = 35; /* فاقد شرایط  */
        }
          /// بستن پدر زیر 60 سال
          else if ( input.DependentRelation == 3
              && input.HasValidDependentCommission == false
              ) {
            res.IsActive = false;
            res.DeActiveReason = 35; /* فاقد شرایط  */
        } else if ( input.IsStudentBlock ) {
            res.IsActive = false;
            res.DeActiveReason = 31; /* انسداد سرپرست  */
        }

          ///=======================================================================================================================================================================================

          else if (
              input.IsStudentBlock == false
              && input.StudentGender == 1
              && input.DependentGender == 1
              && input.IsDependentAzkaroftade == true
              && input.DependentRelation.In(2, 4, 5) /* فرزند نوه فرزند خوانده */
              ) {
            res.IsActive = true;
            res.ActiveReason = 13; /* از کار افتاده */
            if ( input.DependentAzkaroftadeValidityDate > 0 ) {
                res.ExpireDate = input.DependentAzkaroftadeValidityDate;
                res.DeActiveReasonOnExpire = 35; /* فاقد شرایط */
            }
        } else if ( input.IsStudentBlock == false
              && input.HasValidDependentCommission == true
          ) {
            res.IsActive = true;
            res.ActiveReason = 36; /* کمیسیون */

            if ( input.DependentCommissionValidityDate > 0 ) {
                res.DeActiveReasonOnExpire = 35; /*فاقد شرایط*/
                res.ExpireDate = input.DependentCommissionValidityDate;
            }
        } else if ( input.IsStudentBlock == false
                      && input.StudentGender == 1
                      && input.DependentGender == 2
                      && input.DependentSingleStatus == 2 /* فوت همسر */
                      && input.DependentRelation.In(2, 4, 5) /* فرزند نوه فرزند خوانده */
                       ) {
            res.IsActive = true;
            res.ActiveReason = 32; /* فوت همسر */
        } else if ( input.IsStudentBlock == false
                    && input.StudentGender == 1
                      && input.DependentGender == 1
                      && input.IsDependentInStudy == true
                      && input.DependentAge > 19
                       && input.DependentRelation.In(2, 4, 5) /* فرزند نوه فرزند خوانده */
                      ) {
            res.IsActive = true;
            res.ActiveReason = 18; /* دانشجو */
            res.DeActiveReasonOnExpire = 1; /* اتمام سن */
            res.ExpireDate = input.DependentInStudyValidityDate;
        } else if ( input.IsStudentBlock == false
                    && input.StudentGender == 1 ) {
            res.IsActive = true;
        }

        //===============================================

        /// علت محاسبه شده در علت های منسوخ نباشد

        if ( (res.ActiveReason.HasValue && canceledReasons.Contains(res.ActiveReason.Value)) || (res.DeActiveReason.HasValue && canceledReasons.Contains(res.DeActiveReason.Value)) ) {
            throw new Exception("علت محاسبه شده از علت های منسوخ است");
        }

        /// اگر فقط علت عوض شده بود
        if ( input.DependentIsActive == res.IsActive
            && input.DependentDeActiveReasonOnExpire == res.DeActiveReasonOnExpire
            && input.DependentExpireDate == res.ExpireDate
            //&& input.saveType == Enums.SaveType.Update /* enum: update insert */
            && input.DependentDeActiveReason != 31 /* انسداد سرپرست */
            && (input.DependentActiveReason != res.ActiveReason || input.DependentDeActiveReason != res.DeActiveReason)
            ) {
            // همان علت قبلی را برگردان
            var res2 = new DependentActiveDeactiveReason();
            res2.IsActive = input.DependentIsActive;
            res2.ActiveReason = input.DependentActiveReason;
            res2.DeActiveReasonOnExpire = input.DependentDeActiveReasonOnExpire;
            res2.ExpireDate = input.DependentExpireDate;

            return res2;
        }

        return res;

    }

    private static double GetAge(int birthDate) {
        double age = 0;
        var today = DateTime.Now.ToPersianInteger(); // getPersianDate as int

        age = (today - birthDate) / (10000.0);

        return age;
    }

}
