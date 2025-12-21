using FluentValidation;
using Csis.Admission.Application.Common;
using Csis.Admission.Application.Common.Helpers;
using Csis.Admission.Application.Features.StudentDependents.Commands;

namespace Csis.Admission.Application.Features.StudentDependents.Validators;

/// <summary>
/// اعتبارسنجی فرمان ثبت همسر از ثبت احوال
/// </summary>
public sealed class RegisterSpouseFromSabteAhvalCommandValidator : BaseValidator<IdentifySpouseFromSabteAhvalCommand>
{
    public RegisterSpouseFromSabteAhvalCommandValidator()
    {
        RuleFor(x => x.SpouseNationalCode)
            .NotEmpty()
            .WithMessage("کد ملی همسر الزامی است.")
            .Matches(NationalCodeHelper.NationalCodeRegexPattern)
            .WithMessage("کد ملی همسر باید ۱۰ رقم باشد.")
            .Must(NationalCodeHelper.IsValidNationalCode)
            .WithMessage("کد ملی همسر معتبر نمی‌باشد.");

        RuleFor(x => x.SpouseBirthDate)
            .NotEmpty()
            .WithMessage("تاریخ تولد همسر الزامی است.")
            .Matches(Constants.StringDateFormatRegex)
            .WithMessage("قالب تاریخ تولد همسر صحیح نمی‌باشد. قالب مورد انتظار: ۱۴۰۳/۰۱/۰۱");

        RuleFor(x => x.MarriageDate)
            .NotEmpty()
            .WithMessage("تاریخ ازدواج الزامی است.")
            .Matches(Constants.StringDateFormatRegex)
            .WithMessage("قالب تاریخ ازدواج صحیح نمی‌باشد. قالب مورد انتظار: ۱۴۰۳/۰۱/۰۱");

        RuleFor(x => x.Religion)
            .IsInEnum()
            .WithMessage("مذهب انتخاب شده نامعتبر است.");
    }
}
