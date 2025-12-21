using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.CaseFilings.Validator;

internal sealed class IdentityValidator(IRepository<StudentSummary> summaryRepo, IStudentRepository studentRepo, ICsisWsmService wsmService)
{
    public async Task Iranian(string nationalCode, string birthDate, string mobile, CancellationToken cancellation) {
        // Age
        var age = Age(birthDate);

        // NationalCode
        var nationalExists = await summaryRepo.ExistsAsync(x => x.NationalCode == nationalCode, false, cancellation);
        if ( nationalExists ) { throw new CommandValidationException("کد ملی واردشده قبلاً در سامانه ثبت شده است."); }

        var validateRequest = new GetIdentityInfoByNationalCodeRequest(-1, nationalCode, birthDate.StringDateToInt().Value);
        var identityInfo = await wsmService.GetIdentityInfoByNationalCode(validateRequest, cancellation);
        if ( string.IsNullOrEmpty(identityInfo.Nin) ) {
            throw new CommandValidationException("کد ملی یا تاریخ تولد وارد شده در ثبت احوال یافت نشد.");
        }

        // Mobile
        if ( !string.IsNullOrWhiteSpace(mobile) ) {
            var mobileExists = await summaryRepo.ExistsAsync(x => x.Mobile == mobile, false, cancellation);
            if ( mobileExists ) { throw new CommandValidationException("شماره موبایل واردشده قبلاً در سامانه ثبت شده است."); }

            if ( age >= 19 ) {
                var validateMobile = await wsmService.ValidateMobileOwnership(new ValidateMobileOwnershipRequest(nationalCode, mobile), cancellation);
                if ( !validateMobile ) { throw new CommandValidationException("شماره موبایل واردشده با کد ملی مطابقت ندارد."); }
            }
        }
    }

    public async Task NonIranian(string yektaCode, string birthDate, CancellationToken cancellation) {
        // Age
        Age(birthDate);

        // YektaCode
        var yektaExists = await summaryRepo.ExistsAsync(x => x.YektaCode == yektaCode, false, cancellation);
        if ( yektaExists ) { throw new CommandValidationException("کد یکتا واردشده قبلاً در سامانه ثبت شده است."); }

        var identityInfo = await wsmService.ValidateNonIranianYektaCode(-1, yektaCode, cancellation);
        if ( !identityInfo.IsValid() ) { throw new CommandValidationException("کد یکتا واردشده نامعتبر است."); }
    }

    private static int Age(string birthDate) {
        var age = Common.Utilities.CalculateAge(birthDate, null);
        if ( age < 13 ) { throw new CommandValidationException("امکان تشکیل پرونده برای طلاب کمتر از ۱۳ سال وجود ندارد.تاریخ تولد خود را بررسی کرده و در صورت نیاز اصلاح نمایید"); }
        if ( age > 100 ) { throw new CommandValidationException("سن شما بیش از ۱۰۰ سال است. لطفاً تاریخ تولد خود را بررسی کرده و در صورت نیاز اصلاح نمایید."); }

        return age.Value;
    }
}

internal sealed class ApprovalCenterValidator(IRepository<StudentSummary> summaryRepo, IStudentRepository studentRepo, ICsisWsmService wsmService)
{
    public async Task Iranian(long caseId, ApprovalCenter approvalCenter, string nationalCode, string birthDate, CancellationToken cancellation) {
        // case id
        await CaseId(caseId, approvalCenter, cancellation);

        // student status
        var validateCommand = new ValidateStudentStatusForRegistrationCommandPrc(nationalCode, null, Citizenship.Iranian,
                birthDate.StringDateToInt().Value, approvalCenter, caseId);
        var validateResult = await studentRepo.ValidateStudentStatusForRegistration(validateCommand);
        if ( !validateResult.IsValid ) { throw new CommandValidationException(validateResult.Message); }
    }

    public async Task NonIranian(long caseId, ApprovalCenter approvalCenter, string yektaCode, string birthDate, CancellationToken cancellation) {
        // case id
        await CaseId(caseId, approvalCenter, cancellation);

        // student status
        var validateCommand = new ValidateStudentStatusForRegistrationCommandPrc(null, yektaCode, Citizenship.NonIranian,
                birthDate.StringDateToInt().Value, approvalCenter, caseId);
        var validateResult = await studentRepo.ValidateStudentStatusForRegistration(validateCommand);
        if ( !validateResult.IsValid ) { throw new CommandValidationException(validateResult.Message); }
    }

    private async Task CaseId(long caseId, ApprovalCenter approvalCenter, CancellationToken cancellation) {
        if ( caseId == 123 ) { return; }

        // Duplicate
        var duplicateCaseIdExists = await summaryRepo.ExistsAsync(x => x.CaseNumInApprovalCenter == caseId, false, cancellation);
        if ( duplicateCaseIdExists ) {
            throw new CommandValidationException("شماره پرونده واردشده قبلاً در سامانه ثبت شده است.");
        }

        // CaseId
        var query = new StudentEducationStatusRepoQuery(approvalCenter, caseId);
        var caseIdResult = studentRepo.GetStudentEducationStatus(query);
        if ( caseIdResult == null ) { throw new CommandValidationException("شماره پرونده در مرکز حوزوی یافت نشد."); }
    }
}
