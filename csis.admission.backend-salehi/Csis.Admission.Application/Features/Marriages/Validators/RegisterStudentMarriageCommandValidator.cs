using Csis.Admission.Application.Features.Marriages.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.Marriages.Validators;

public sealed class RegisterStudentMarriageCommandValidator : BaseValidator<UpdateStudentSisterMarriageCommand>
{
    public RegisterStudentMarriageCommandValidator() {
        RuleFor(x => x.MarriageDate).NotEmpty().WithName("تاریخ ازدواج");
        RuleFor(x => x.SpouseBirthDate).NotEmpty().WithName("تاریخ تولد همسر");
        RuleFor(x => x.SpouseNationalCode).NotEmpty().WithName("کد ملی همسر");
    }
}
