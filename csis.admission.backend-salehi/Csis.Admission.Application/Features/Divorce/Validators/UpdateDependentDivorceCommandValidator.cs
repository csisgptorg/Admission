using FluentValidation;
using Csis.Admission.Application.Common;
using Csis.Admission.Application.Common.Helpers;
using Csis.Admission.Application.Features.Divorce.Commands;

namespace Csis.Admission.Application.Features.Divorce.Validators;

/// <summary>
/// اعتبارسنج? فرم ثبت طلاق تکفل
/// </summary>
public sealed class UpdateDependentDivorceCommandValidator : BaseValidator<UpdateDependentDivorceCommand>
{
    public UpdateDependentDivorceCommandValidator()
    {
        RuleFor(x => x.Codm)
            .GreaterThan(0)
            .WithMessage("کد مرکز نامعتبر است.");

        RuleFor(x => x.DependentId)
            .NotNull()
            .WithMessage("شناسه تکفل الزامی است.")
            .GreaterThan(0)
            .WithMessage("شناسه تکفل نامعتبر است.");

        RuleFor(x => x.DivorceDate)
            .NotEmpty()
            .WithMessage("تاریخ طلاق الزامی است.")
            .Matches(Constants.StringDateFormatRegex)
            .WithMessage("قالب تاریخ طلاق صحیح نمی‌باشد.");

        RuleFor(x => x.DependentNationalCode)
            .NotEmpty()
            .WithMessage("کد ملی تکفل الزامی است.")
            .Matches(NationalCodeHelper.NationalCodeRegexPattern)
            .WithMessage("کد ملی تکفل باید یی رقم باشد.")
            .Must(NationalCodeHelper.IsValidNationalCode)
            .WithMessage("کد ملی تکفل معتبر نمی‌باشد.");

        RuleFor(x => x.DependentBirthDate)
            .NotEmpty()
            .WithMessage("تاریخ تولد تکفل الزامی است.")
            .Matches(Constants.StringDateFormatRegex)
            .WithMessage("قالب تاریخ تولد تکفل صحیح نمی‌باشد.");
    }
}
