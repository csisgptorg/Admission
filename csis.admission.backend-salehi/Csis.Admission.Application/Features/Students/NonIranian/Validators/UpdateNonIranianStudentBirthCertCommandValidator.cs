using FluentValidation;
using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Features.Students.Validators;

public sealed class UpdateNonIranianStudentBirthCertCommandValidator : BaseValidator<UpdateNonIranianStudentBirthCertCommand>
{
    public UpdateNonIranianStudentBirthCertCommandValidator() {
        RuleFor(x => x.Codm).GreaterThan(0).WithName("کد مرکز خدمات");
        RuleFor(x => x.YektaCode).NotEmpty().WithName("کد یکتا.");
        RuleFor(x => x.Religion).IsInEnum().WithName("مذهب");
    }
}
