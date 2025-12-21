using FluentValidation;
using System.Text.RegularExpressions;
using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Features.Students.Validators;

public sealed class UpdateNonIranianDependentCitizenshipCommandValidator : BaseValidator<UpdateNonIranianDependentCitizenshipCommand>
{
    public UpdateNonIranianDependentCitizenshipCommandValidator() {
        RuleFor(x => x.Id).GreaterThan(0).WithName("شناسه");

        RuleFor(x => x.NationalCode).Matches(new Regex(Utilities.Constants.Regex.PersonNationalId)).WithMessage("کد ملی معتبر نمی باشد.");
        
        RuleFor(x => x.BirthDate)
            .NotEmpty().WithName("تاریخ تولد")
            .Matches(new Regex(Constants.StringDateFormatRegex)).WithMessage("تاریخ تولد وارد شده معتبر نیست.");
    }
}
