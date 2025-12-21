using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Extensions;
using Csis.Authorization.Services;
using static Csis.Admission.Application.Common.Models.ValidateNonIranianRelationshipResponse;

namespace Csis.Admission.Application.Features.NonIranianStudent.Commands;
/// <summary>
///  انتساب نسبت به غیرایرانی ها
/// </summary>
public sealed record AssignRelationToNonIranianCommand : IRequest
{
    /// <summary>
    /// کد مرکز
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// کد یکتا طلبه
    /// </summary>
    public string StudentYektaCode { get; init; }

    /// <summary>
    /// کد یکتا تکفل
    /// </summary>
    public string DependentYektaCode { get; init; }

    /// <summary>
    /// نسبت
    /// </summary>
    public DependentRelation NonIranianDependentRelation { get; init; }
}

/// <summary>
/// هندلر انتساب نسبت به غیرایرانی ها
/// </summary>
/// <param name="logger"></param>
/// <param name="authenticatedUserService"></param>
/// <param name="dependentRepository"></param>
/// <param name="studentSummaryRepository"></param>
/// <param name="dependentSummaryRepository"></param>
/// <param name="csisWsmService"></param>
public sealed class AssignRelationToNonIranianCommandHandler(IStudentDependentRepository dependentRepository, IRepository<DependentSummary, long> dependentSummaryRepository, ICsisWsmService csisWsmService)
    : IRequestHandler<AssignRelationToNonIranianCommand>
{
    /// <inheritdoc />
    public async Task Handle(AssignRelationToNonIranianCommand request, CancellationToken cancellationToken) {

        var (prcRequest, validateYektaCodeResponse) = await SabteAhvalHoviat(request.DependentYektaCode, cancellationToken);

        var existingDependent = await dependentSummaryRepository.GetAllAsync(
            x => x.YektaCode == request.DependentYektaCode ||
                 (validateYektaCodeResponse.FidaCode.HasValue &&
                  x.FidaCode == validateYektaCodeResponse.FidaCode.Value.ToString()),
            cancellationToken: cancellationToken);

        // ۱. اگر تحت تکفل همین Codm بود خطا
        if ( existingDependent.Any(x => x.Codm == request.Codm) ) {
            throw new CommandValidationException("این فرد قبلاً ثبت شده است.");
        }

        // ۲-۵. بررسی شرایط دیگر
        if ( existingDependent.Any(x =>
            x.IsActive || // فعال بود خطا
            (x.Relation == DependentRelation.Spouse && x.IsMarried) || // همسر + متاهل خطا
            (x.Relation == DependentRelation.Child && !x.IsMarried && x.Gender == Gender.Female) || // دختر + مجرد خطا
            (x.Relation != DependentRelation.Spouse && (x.Relation != DependentRelation.Child && x.Gender != Gender.Female)) // نه همسر نه دختر خطا
        ) ) {
            throw new CommandValidationException("این فرد قبلاً به‌عنوان تکفل برای کُد دیگری ثبت شده است.");
        }

        // ۶. اگر همین شخص نبود خطا (چک نام)
        if ( existingDependent.Any(x =>
            x.YektaCode == request.DependentYektaCode &&
            (Utilities.Extensions.StringExtensions.ArabicToPersian(x.FirstName) != Utilities.Extensions.StringExtensions.ArabicToPersian(prcRequest.FirstName) ||
             Utilities.Extensions.StringExtensions.ArabicToPersian(x.LastName) != Utilities.Extensions.StringExtensions.ArabicToPersian(prcRequest.LastName) ||
             Utilities.Extensions.StringExtensions.ArabicToPersian(x.FatherName) != Utilities.Extensions.StringExtensions.ArabicToPersian(prcRequest.FatherName))) ) {
            throw new CommandValidationException("کد یکتا با این هویت برای کُد دیگری ثبت شده است.");
        }

        // 3) اعتبارسنجی رابطه
        var isNonIranianStudentHasRelation = await ValidateRelation(request, prcRequest, cancellationToken);
        if ( !isNonIranianStudentHasRelation ) {
            throw new CommandValidationException("رابطه بین طلبه و تکفل یافت نشد");
        }

        var result = await dependentRepository.Create(prcRequest);
        if ( !result.IsSuccess ) {
            throw new CommandValidationException(result.Message);
        }
    }

    /// <summary>
    /// اعتبارسنجی رابطه
    /// </summary>
    /// <param name="request"></param>
    /// <param name="prcRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="CommandValidationException"></exception>
    private async Task<bool> ValidateRelation(AssignRelationToNonIranianCommand request, StudentDependentRegistryPrcRequest prcRequest, CancellationToken cancellationToken) {

        var iranianRelationshipRequest = new ValidateNonIranianRelationshipRequest(request.StudentYektaCode, request.DependentYektaCode);

        var relation = await csisWsmService.ValidateNonIranianRelationship(iranianRelationshipRequest, cancellationToken);

        if ( Enum.TryParse<NonIranianDependentRelation>(relation.RelationId, out var parsedRelation) ) {

            var result = relation.GetResult() == Result.ValidRelation;
            if ( result ) {
                if ( parsedRelation == NonIranianDependentRelation.Spouse && request.NonIranianDependentRelation == DependentRelation.Spouse ) {
                    prcRequest.Relation = DependentRelation.Spouse;
                    prcRequest.IsMarried = true;
                } else if ( parsedRelation == NonIranianDependentRelation.Child && request.NonIranianDependentRelation == DependentRelation.Child ) {
                    prcRequest.Relation = DependentRelation.Child;
                } else {
                    throw new CommandValidationException("فقط امکان ثبت رابطه  همسر و فرزندی وجود دارد.");
                }
                prcRequest.Codm = request.Codm;
            }

            return result;
        }

        throw new CommandValidationException("رابطه بین طلبه و تکفل یافت نشد");
    }

    /// <summary>
    /// ثبت احوال هویتی
    /// </summary>
    /// <param name="dependentYektaCode"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<(StudentDependentRegistryPrcRequest prcRequest, ValidateNonIranianYektaCodeResponse validateYektaCodeResponse)> SabteAhvalHoviat(string dependentYektaCode, CancellationToken cancellationToken) {

        var hoviat = await csisWsmService.ValidateNonIranianYektaCode(-1, dependentYektaCode, cancellationToken);
        var command = new StudentDependentRegistryPrcRequest { //Todo:
            FirstName = Utilities.Extensions.StringExtensions.ArabicToPersian(hoviat.FirstName),
            LastName = Utilities.Extensions.StringExtensions.ArabicToPersian(hoviat.LastName),
            FatherName = Utilities.Extensions.StringExtensions.ArabicToPersian(hoviat.FatherName),
            BirthDate = hoviat.ShamsiBirthDate.StringDateToInt(),
            BirthCertSerial = null,
            NationalCode = null,
            YektaCode = hoviat.UniqeCode.ToString(),
            BirthCertIssuePlace = null,
            BirthCertSeri = null,
            Citizenship = Citizenship.NonIranian,
            Nationality = (short) hoviat.NationalityId,
            Gender = (Gender?) hoviat.Gender,
            DeathDate = 0,
            IsDead = false,
            IsSadat = hoviat.FirstName.StartsWith("سید") || hoviat.FirstName.EndsWith("سادات"),
            SingleStatus = SingleStatus.Single,
            BirthCertNumber = null,
            IsMarried = false,
            PassportNumber = hoviat.PassportNumber ?? null,
            //FidaCode = hoviat.FidaCode.HasValue ? hoviat.FidaCode.Value.ToString() : null,
        };

        return new(command, hoviat);
    }
    // به گفته سید استفاده نشود
    ///// <summary>
    ///// قاعده عدم تکرار: یک فرد نمی‌تواند به‌عنوان تکفل برای چندین طلبه ثبت شود
    ///// </summary>
    ///// <param name="prcRequest"></param>
    ///// <param name="codm"></param>
    ///// <param name="cancellationToken"></param>
    ///// <returns></returns>
    ///// <exception cref="CommandValidationException"></exception>
    //private async Task EnsureNoDuplicateDependentAsync(StudentDependentRegistryPrcRequest prcRequest, int codm, CancellationToken cancellationToken) {

    //    var foundedDependent = await dependentSummaryRepository.GetAllAsync(x => x.Codm == codm, cancellationToken: cancellationToken);

    //    var isDuplicate = foundedDependent.Any(x => Utilities.Extensions.StringExtensions.ArabicToPersian(x.FirstName) == prcRequest.FirstName
    //                                                && Utilities.Extensions.StringExtensions.ArabicToPersian(x.LastName) == prcRequest.LastName
    //                                                && Utilities.Extensions.StringExtensions.ArabicToPersian(x.FatherName) == prcRequest.FatherName
    //                                                && x.BirthDate == prcRequest.BirthDate
    //                                                && x.PassportNumber == prcRequest.PassportNumber);
    //    if ( isDuplicate ) {
    //        throw new CommandValidationException("این فرد قبلاً ثبت شده است.");
    //    }
    //}
    // به گفته سید استفاده نشود
    ///// <summary>
    ///// قاعده پاسپورت+ملیت: برای سایر کُدها فقط در صورت تطابق کامل هویت مجاز است
    ///// </summary>
    ///// <param name="prcRequest"></param>
    ///// <param name="codm"></param>
    ///// <param name="cancellationToken"></param>
    ///// <returns></returns>
    ///// <exception cref="CommandValidationException"></exception>
    //private async Task EnsureNoDuplicatePassportNationalityAsync(StudentDependentRegistryPrcRequest prcRequest, int codm, CancellationToken cancellationToken) {

    //    var dupStudents = await studentSummaryRepository.ExistsAsync(
    //         x => x.Codm != codm
    //               && x.Nationality == prcRequest.Nationality
    //               && x.PassportNumber == prcRequest.PassportNumber &&
    //               (x.FirstName != prcRequest.FirstName
    //                || x.LastName != prcRequest.LastName
    //                || x.FatherName != prcRequest.FatherName
    //                || x.BirthDate != prcRequest.BirthDate),
    //         cancellationToken: cancellationToken);

    //    var dupDependents = await dependentSummaryRepository.ExistsAsync(
    //        x => x.Codm != codm
    //              && x.Nationality == (Nationality?) prcRequest.Nationality
    //              && x.PassportNumber == prcRequest.PassportNumber &&
    //              (x.FirstName != prcRequest.FirstName
    //               || x.LastName != prcRequest.LastName
    //               || x.FatherName != prcRequest.FatherName
    //              || x.BirthDate != prcRequest.BirthDate),
    //        cancellationToken: cancellationToken);

    //    if ( dupDependents || dupStudents ) {
    //        throw new CommandValidationException("شماره پاسپورت با این ملیت برای کُد دیگری ثبت شده و هویت با رکورد موجود تطابق کامل ندارد.");
    //    }
    //}
}
