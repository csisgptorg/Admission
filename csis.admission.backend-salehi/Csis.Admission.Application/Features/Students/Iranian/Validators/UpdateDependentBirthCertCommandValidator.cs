using FluentValidation;
using System.Text.RegularExpressions;
using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Features.Students.Validators;

/// <summary>اعتبار سنجی</summary>
public sealed class UpdateDependentBirthCertCommandValidator : BaseValidator<UpdateDependentBirthCertCommand>
{
    /// <summary>اعتبار سنجی</summary>
    public UpdateDependentBirthCertCommandValidator() {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithName("شناسه تکفل");

        RuleFor(x => x.NationalCode)
            .NotEmpty().WithName("کد ملی")
            .Matches(new Regex(Utilities.Constants.Regex.PersonNationalId)).WithMessage("کد ملی معتبر نمی باشد.");
        
        RuleFor(x => x.BirthDate)
            .NotEmpty().WithName("تاریخ تولد")
            .Matches(new Regex(Constants.StringDateFormatRegex)).WithMessage("تاریخ تولد وارد شده معتبر نیست.");

        RuleFor(x => x.Religion)
            .NotEmpty().WithName("مذهب")
            .IsInEnum().WithName("مذهب");
    }
}
