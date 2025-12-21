using Csis.Admission.Application.Common.Configuration;
using Csis.Admission.Application.Common.Dtos.RequestService;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.Addresses.Commands;
using Csis.Admission.Application.Features.BankAccounts.Commands;
using Csis.Admission.Application.Features.CaseFilings.Dtos;
using Csis.Admission.Application.Features.Employments.Commands;
using Csis.Admission.Application.Features.StudentMobiles.Commands;
using Csis.Admission.Application.Features.Students.Commands;
using Csis.Authorization.Services;
using Csis.Notification;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Csis.Admission.Application.Features.CaseFilings.Commands;

/// <summary>
/// گام چهارم تشکیل پرونده
/// </summary>
public sealed record CompleteInformationCaseFilingCommand(AdmissionCaseUserDto CaseUser, RequestFlow Flow) : IRequest<long>;

internal sealed class CompleteInformationCaseFileCommandHandler(
    ILogger<CompleteInformationCaseFileCommandHandler> logger,
    ICsisAuthenticatedUserService authenticatedUserService,
    IEmployeeDataService employeeService,
    IOptions<ImageAnalysisOption> options,
    IRepository<AdmissionCaseUser, Guid> caseUserRepo,
    IRepository<CaseFillingRequest, long> casefilingRepo,
    ICsisWsmService csisWsmService,
    IStudentRepository studentRepo,
    IMediator mediator,
    ICsisNotificationAdvancedService csisNotificationAdvancedService)
    : IRequestHandler<CompleteInformationCaseFilingCommand, long>
{
    public async Task<long> Handle(CompleteInformationCaseFilingCommand request, CancellationToken cancellationToken) {

        var codm = 0;

        var caseUserToUpdate =
            await caseUserRepo.GetOneAsTrackingAsync(x => x.Id == request.CaseUser.Id,
                cancellationToken: cancellationToken);
        var command = new StudentRegistrationCommandPrc();
        switch ( caseUserToUpdate.Citizenship ) {
            case Citizenship.Iranian:
                var iranianInfo = await csisWsmService.GetIdentityInfoByNationalCode(
                    new GetIdentityInfoByNationalCodeRequest(-1, request.CaseUser.NationalCode,
                        request.CaseUser.BirthDate.Value), cancellationToken);

                command = new StudentRegistrationCommandPrc {
                    //Todo:
                    FirstName = iranianInfo.Name,
                    LastName = iranianInfo.Family,
                    FatherName = iranianInfo.FatherName,
                    BirthDate = iranianInfo.BirthDate.StringDateToInt().Value,
                    BirthCertSerial = Convert.ToInt32(iranianInfo.ShenasnameSerial),
                    NationalCode = request.CaseUser.NationalCode,
                    YektaCode = request.CaseUser.YektaCode,
                    BirthCertIssuePlace = iranianInfo.ShenasnameIssuePlace,
                    BirthCertSeri = iranianInfo.ShenasnameSeri,
                    ApprovalCenter = request.CaseUser.ApprovalCenter.Value,
                    CaseNumInApprovalCenter = request.CaseUser.CaseNumInApprovalCenter,
                    Citizenship = request.CaseUser.Citizenship.Value,
                    Gender = short.Parse(iranianInfo.Gender),
                    DeathDate =
                        !string.IsNullOrEmpty(iranianInfo.DeathDate)
                            ? iranianInfo.DeathDate.StringDateToInt().Value
                            : null,
                    IsDead = !string.IsNullOrEmpty(iranianInfo.DeathDate),
                    IsSadat = iranianInfo.Name.StartsWith("سید") || iranianInfo.Name.EndsWith("سادات"),
                    SingleStatus = (short) SingleStatus.Single,
                    BirthCertNumber = int.Parse(iranianInfo.ShenasnameNo),
                    MarriageDate = null,
                    DivorceDate = null,
                    CaseValidityDate = await IsEducationStudent(caseUserToUpdate, cancellationToken) ? DateTime.Now.AddYears(1).ToPersianInteger() : DateTime.Now.AddMonths(3).ToPersianInteger(),
                    ResidenceExpireDate = null,
                    IsActive = true,
                    IsBlock = false,
                    IsMarried = false,
                };
                break;
            case Citizenship.NonIranian:
                var nonIranianInfo =
                    await csisWsmService.ValidateNonIranianYektaCode(-1, caseUserToUpdate.YektaCode, cancellationToken);
                command = new StudentRegistrationCommandPrc {
                    //Todo:
                    FirstName = nonIranianInfo.FirstName,
                    LastName = nonIranianInfo.LastName,
                    FatherName = nonIranianInfo.FatherName,
                    BirthDate = nonIranianInfo.ShamsiBirthDate.StringDateToInt().HasValue ? nonIranianInfo.ShamsiBirthDate.StringDateToInt().Value : null,
                    BirthCertSerial = null,
                    NationalCode = null,
                    YektaCode = request.CaseUser.YektaCode,
                    BirthCertIssuePlace = null,
                    BirthCertSeri = null,
                    ApprovalCenter = request.CaseUser.ApprovalCenter.Value,
                    CaseNumInApprovalCenter = request.CaseUser.CaseNumInApprovalCenter,
                    Citizenship = request.CaseUser.Citizenship.Value,
                    Gender = (short) nonIranianInfo.Gender,
                    DeathDate = null,
                    IsDead = false,
                    IsSadat = nonIranianInfo.FirstName.StartsWith("سید") || nonIranianInfo.FirstName.EndsWith("سادات"),
                    SingleStatus = (short) SingleStatus.Single,
                    CaseValidityDate = await IsEducationStudent(caseUserToUpdate, cancellationToken) ? DateTime.Now.AddYears(1).ToPersianInteger() : DateTime.Now.AddMonths(3).ToPersianInteger(),
                    ResidenceExpireDate = null,
                    MarriageDate = null,
                    DivorceDate = null,
                    BirthCertNumber = 0,
                    IsActive = true,
                    IsBlock = false,
                    IsMarried = false,
                };
                break;
        }

        var result = await studentRepo.CreateStudentRegistrationAsync(command);
        if ( result.IsSuccess ) {
            codm = result.Codm;


            // set Codm in Request
            var foundedCodm = await casefilingRepo.GetOneAsTrackingAsync(x => x.Codm == caseUserToUpdate.Codm, cancellationToken: cancellationToken);
            foundedCodm.RecordId = codm;
            await casefilingRepo.UpdateAsync(foundedCodm, true, cancellationToken);
            // set Codm in CaseUser
            caseUserToUpdate.RequestId = codm;
        } else {
            throw new CommandValidationException(result.Message);
        }

        await SetStudentMobileAsync(request.CaseUser.Id, codm, cancellationToken);
        await SetStudentBankAccountAsync(codm, request.CaseUser, cancellationToken);
        await SetStudentProfilePictureAsync(codm, request.CaseUser, cancellationToken);
        await SetStudentAddressAsync(codm, request.CaseUser, cancellationToken);
        await SetStudentEmploymentAsync(codm, request.CaseUser, cancellationToken);
        await SetCommissionStatusAsync(codm, request.CaseUser);

        await NotifyUserAsync(codm, cancellationToken);
        caseUserToUpdate.CaseStep = AdmissionCaseStep.CodmSendBySms;

        await caseUserRepo.UpdateAsync(caseUserToUpdate, true, cancellationToken);

        var currentUser = await CurrentEmployee();
        switch ( request.Flow ) {
            case RequestFlow.DirectRegistration when (currentUser?.IsSenior == true):
            case RequestFlow.StudentToEmployee: {
                var randomPassword = Guid.NewGuid().ToString()[..10];
                await mediator.Send(new CreateAdmissionCaseStepCreateUserCommand(codm, randomPassword), cancellationToken);
                break;
            }
        }

        return result.Codm;
    }

    private async Task SetStudentMobileAsync(Guid token, int codm, CancellationToken cancellationToken) {
        try {
            await mediator.Send(new UpdateStudentMobileForCompleteInfoRegistrationCommand(token, codm),
                cancellationToken);
        } catch ( Exception ) {

            throw new CommandValidationException("خطا در به‌روزرسانی موبایل");
        }
    }

    //TODO: Refactor All SetStudent... Methods
    private async Task SetStudentBankAccountAsync(int codm, AdmissionCaseUserDto caseUser,
        CancellationToken cancellationToken) {
        var payload = caseUser.Payloads.First(x => x.Name == nameof(AdmissionCasePayloadName.BankAccount));

        var bankAccountNumber = ((JsonElement) payload.Payload).GetProperty("BankAccountNumber").GetString();

        try {
            await mediator.Send(
                new UpdateStudentBankAccountCommand { Codm = codm, BankAccountNumber = bankAccountNumber },
                cancellationToken);
        } catch ( Exception ) {

            throw new CommandValidationException("خطا در ایجاد شماره حساب");
        }
    }


    private async Task SetStudentProfilePictureAsync(int codm, AdmissionCaseUserDto caseUser,
        CancellationToken cancellationToken) {
        var payload = caseUser.Payloads.First(x => x.Name == nameof(AdmissionCasePayloadName.Picture));

        var analysis =
            ((JsonElement) payload.Payload).Deserialize<UpdateStudentProfilePictureCommand>(
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });


        try {
            await mediator.Send(
                new UpdateStudentProfilePictureCommand(codm, analysis.ImageAnalysisResultDto, analysis.FileId,
                    caseUser.RequestId ?? -1), cancellationToken);
        } catch ( Exception ) {

            throw new CommandValidationException("خطا در آپلود تصویر پروفایل");
        }
    }


    private async Task SetStudentAddressAsync(int codm, AdmissionCaseUserDto caseUser,
        CancellationToken cancellationToken) {
        var payload = caseUser.Payloads.First(x => x.Name == nameof(AdmissionCasePayloadName.Address));

        var address = JsonSerializer.Deserialize<AddressFromExternalServiceDto>(payload.Payload.ToString());
        try {
            await mediator.Send(
                new CreateOrUpdateStudentAddressCommand {
                    Codm = codm,
                    ProvinceId = address?.ProvinceId,
                    CityId = address?.CityId,
                    PortionId = address?.PortionId,
                    TownId = address?.TownId,
                    RuralId = address?.RuralId,
                    Township = address?.Township,
                    Village = address?.Village,
                    District = address?.District,
                    Avenue = address?.Avenue,
                    Street = address?.Street,
                    Alley = address?.Alley,
                    Lane = address?.Lane,
                    Number = address?.Number,
                    Complex = address?.Complex,
                    Block = address?.Block,
                    Unit = address?.Unit,
                    Floor = address?.Floor,
                    ZipCode = address?.ZipCode,
                    ProjectCode = 1,
                    Flag = true,
                    RequiresDualStudentApproval = false,
                    ConfirmedStudentCodms = null,
                    RequestId = caseUser.RequestId ?? -1
                }, cancellationToken);

        } catch ( Exception e ) {
            throw new CommandValidationException("خطا در ثبت آدرس");
        }
    }


    private async Task SetStudentEmploymentAsync(int codm, AdmissionCaseUserDto caseUser,
        CancellationToken cancellationToken) {
        var payload = caseUser.Payloads.First(x => x.Name == nameof(AdmissionCasePayloadName.Employment));

        var employment = JsonSerializer.Deserialize<CreateOrUpdateStudentEmploymentCommand>(payload.Payload.ToString());

        try {
            await mediator.Send(
                new CreateOrUpdateStudentEmploymentCommand() {
                    Codm = codm,
                    HasIncome = employment?.HasIncome,
                    IsEmployee = employment?.IsEmployee,
                    EmployeeName = employment?.EmployeeName,
                    EmployeeAddress = employment?.EmployeeAddress,
                    HasSufficientIncome = employment?.HasSufficientIncome,
                    HasAnotherBaseInsurance = employment?.HasAnotherBaseInsurance,
                    InsurancePlaceName = employment?.InsurancePlaceName,
                    InsurancePlaceAddress = employment?.InsurancePlaceAddress,
                    HasAnotherSupInsurance = employment?.HasAnotherSupInsurance,
                    IsEmployeeInHowze = employment?.IsEmployeeInHowze,
                    HowzeTypeId = employment?.HowzeTypeId,
                    IsRetried = employment?.IsRetried,
                    InsuranceTypeId = employment?.InsuranceTypeId,
                    Reference = employment?.Reference,
                    RequestId = caseUser.RequestId ?? -1
                }, cancellationToken);

        } catch ( Exception ) {
            throw new CommandValidationException("خطا در ثبت اطلاعات شغلی");
        }
    }

    private async Task NotifyUserAsync(int codm, CancellationToken cancellationToken) {

        var message = $"طلبه گرامی کد مرکز شما به شماره {codm} ایجاد شد.";

        await csisNotificationAdvancedService.SendMessageToStudent(
            new SendMessageToStudent(message, [codm], new DeliveryChannelEnum[(int) DeliveryChannelEnum.Sms]),
            cancellationToken);
    }

    private async Task SetCommissionStatusAsync(int codm, AdmissionCaseUserDto caseUser) {
        if ( caseUser.ApprovalCenter != ApprovalCenter.Commission ) { return; }

        await studentRepo.SetCommissionStatus(new SetCommissionRequestRepoCommand {
            CommissionRequestId = caseUser.CaseNumInApprovalCenter.Value,
            Status = Enums.CommissionRequestStatus.BranchExpertActionDone,
            Description = $"Codm:{codm}"
        });
    }

    private async Task<CaseFillingEmployeeDto> CurrentEmployee() {
        var personnelId = await authenticatedUserService.GetPersonnelIdAsync();
        if ( !personnelId.HasValue ) {
            return null;
        }

        try {
            var employee = await employeeService.GetEmployeeInfoAsync(personnelId.Value);
            var fullName = employee != null ? employee.FirstName + " " + employee.LastName : "فاقد مشخصات";
            var isSenior = await authenticatedUserService.IsAuthorizedToAsync(PermissionsEnum.SeniorPersonnel);
            return new CaseFillingEmployeeDto(personnelId.Value, fullName, isSenior);
        } catch ( Exception e ) {
            return new CaseFillingEmployeeDto(null, "فاقد مشخصات", false);
        }

    }

    private async Task<bool> IsEducationStudent(AdmissionCaseUser caseUser, CancellationToken cancellationToken) {
        var getStatus = new StudentEducationStatusRepoQuery(caseUser.ApprovalCenter.Value, caseUser.CaseNumInApprovalCenter.Value);
        var status = await studentRepo.GetStudentEducationStatus(getStatus);
        if ( status != null ) {
            return status.EducationStatus == EducationStatus.Student;
        }
        return false;
    }
}
