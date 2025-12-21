using Csis.Admission.Application.Features.CaseFilings.Commands;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Csis.Admission.Application.Features.CaseFilings.Validator;

public sealed class CreateAdmissionCaseFirstStepCommandValidator : BaseValidator<CreateAdmissionCaseFirstStepCommand>
{
    public CreateAdmissionCaseFirstStepCommandValidator() {
        RuleFor(x => x.Citizenship).IsInEnum().WithName("تابعیت");
        When(x => x.Citizenship == Citizenship.Iranian, () => {
            RuleFor(x => x.NationalCode).NotEmpty().Matches(new Regex(Utilities.Constants.Regex.PersonNationalId)).WithMessage("کد ملی معتبر نمی باشد.");
        });

        When(x => x.Citizenship == Citizenship.NonIranian, () => {
            RuleFor(x => x.YektaCode).NotEmpty().Matches(new Regex(Constants.YektaCodeFormatRegex)).WithMessage("کد یکتا معتبر نمی باشد.");
        });

        RuleFor(x => x.Mobile).NotEmpty().Matches(new Regex(Utilities.Constants.Regex.Mobile)).WithMessage("شماره موبایل معتبر نمی باشد.");
        RuleFor(x => x.BirthDate).NotEmpty().Matches(new Regex(Constants.StringDateFormatRegex)).WithMessage("تاریخ معتبر نمی باشد.");

        RuleFor(x => x.CaptchaCode).NotEmpty().WithName("کدکپچا");
    }
}
