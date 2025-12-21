using FluentValidation;
using System.Text.RegularExpressions;
using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Features.Students.Validators;

public sealed class UpdateStudentBirthCertCommandValidator : BaseValidator<UpdateStudentBirthCertCommand>
{
    public UpdateStudentBirthCertCommandValidator() {
        RuleFor(x => x.Codm).GreaterThan(0).WithName("کد مرکز خدمات");        
        
        RuleFor(x => x.NationalCode).Matches(new Regex(Utilities.Constants.Regex.PersonNationalId)).WithMessage("کد ملی معتبر نمی باشد.");
        
        RuleFor(x => x.BirthDate)
            .NotEmpty().WithName("تاریخ تولد")
            .Matches(new Regex(Constants.StringDateFormatRegex)).WithMessage("تاریخ تولد وارد شده معتبر نیست.");

        RuleFor(x => x.Religion).IsInEnum().WithName("مذهب");
    }
}
