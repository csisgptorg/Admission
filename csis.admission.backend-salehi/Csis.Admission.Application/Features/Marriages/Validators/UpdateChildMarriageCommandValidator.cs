using FluentValidation;
using Csis.Admission.Application.Features.Marriages.Commands;

namespace Csis.Admission.Application.Features.Marriages.Validators;

/// <summary>
/// اعتبارسنجی فرم ثبت ازدواج تکفل
/// </summary>
public sealed class UpdateChildMarriageCommandValidator : BaseValidator<UpdateChildMarriageCommand>
{
    public UpdateChildMarriageCommandValidator()
    {
        RuleFor(x => x.DependentId)
            .GreaterThan(0)
            .WithMessage("شناسه تکفل نامعتبر است.");

        RuleFor(x => x.MarriageDate)
            .NotEmpty()
            .WithMessage("تاریخ ازدواج الزامی است.")
            .Matches(Constants.StringDateFormatRegex)
            .WithMessage("قالب تاریخ ازدواج صحیح نمی‌باشد. قالب مورد انتظار: ۱۴۰۳/۰۱/۰۱");
    }
}
