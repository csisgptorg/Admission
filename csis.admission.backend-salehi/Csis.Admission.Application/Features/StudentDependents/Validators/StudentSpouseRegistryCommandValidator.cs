using Csis.Admission.Application.Common.Helpers;
using Csis.Admission.Application.Features.StudentDependents.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.StudentDependents.Validators;

/// <summary>
/// اعتبارسنجی فرم ثبت همسر طلبه
/// </summary>
public sealed class StudentSpouseRegistryCommandValidator : BaseValidator<StudentSpouseRegistryRequestCommand>
{
    public StudentSpouseRegistryCommandValidator()
    {
        RuleFor(x => x.SpouseNationalCode)
            .NotEmpty()
            .WithMessage("کد ملی الزامی است.")
            .Matches(NationalCodeHelper.NationalCodeRegexPattern)
            .WithMessage("کد ملی باید ۱۰ رقم باشد.")
            .Must(NationalCodeHelper.IsValidNationalCode)
            .WithMessage("کد ملی معتبر نمی‌باشد.");

        RuleFor(x => x.SpouseBirthDate)
            .NotEmpty()
            .WithMessage("تاریخ تولد الزامی است.")
            .Matches(Constants.StringDateFormatRegex)
            .WithMessage("قالب تاریخ تولد صحیح نمی‌باشد. قالب مورد انتظار: ۱۴۰۳/۰۱/۰۱");

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
