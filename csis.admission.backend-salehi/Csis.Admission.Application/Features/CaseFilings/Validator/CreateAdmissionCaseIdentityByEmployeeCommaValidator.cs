using FluentValidation;
using System.Text.RegularExpressions;
using Csis.Admission.Application.Features.CaseFilings.Commands;

namespace Csis.Admission.Application.Features.CaseFilings.Validator;

internal sealed class CreateAdmissionCaseIdentityByEmployeeCommandValidator : AbstractValidator<CreateAdmissionCaseIdentityByEmployeeCommand>
{
    public CreateAdmissionCaseIdentityByEmployeeCommandValidator() {
        RuleFor(x => x.ApprovalCenter).IsInEnum().NotEmpty().WithName("مرکز تایید کننده");
        RuleFor(x => x.ApprovalCenterCaseId).GreaterThan(0).NotEmpty().WithName("شماره پرونده");
        
        RuleFor(x => x.Citizenship).IsInEnum().NotEmpty().WithName("تابعیت");
        When(x => x.Citizenship == Citizenship.Iranian, () => {
            RuleFor(x => x.NationalCode).NotEmpty().Matches(new Regex(Utilities.Constants.Regex.PersonNationalId)).WithMessage("کد ملی معتبر نمی باشد.");
        });
        When(x => x.Citizenship == Citizenship.NonIranian, () => {
            RuleFor(x => x.YektaCode).NotEmpty().Matches(new Regex(Constants.YektaCodeFormatRegex)).WithMessage("کد یکتا معتبر نمی باشد.");
        });

        RuleFor(x => x.Mobile).NotEmpty().Matches(new Regex(Utilities.Constants.Regex.Mobile)).WithMessage("موبایل معتبر نمی باشد.");
        RuleFor(x => x.Religion).NotEmpty().IsInEnum().WithName("مذهب");
        RuleFor(x => x.BirthDate).NotEmpty().Matches(new Regex(Constants.StringDateFormatRegex)).WithMessage("تاریخ معتبر نمی باشد.");
    }
}
