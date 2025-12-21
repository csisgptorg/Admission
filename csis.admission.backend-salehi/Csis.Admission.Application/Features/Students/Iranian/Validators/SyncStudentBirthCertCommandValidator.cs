using FluentValidation;
using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Features.Students.Validators;

public sealed class SyncStudentBirthCertCommandValidator : BaseValidator<SyncStudentBirthCertCommand>
{
    public SyncStudentBirthCertCommandValidator()
    {
        RuleFor(x => x.Codm).GreaterThan(0).WithName("کد مرکز خدمات");
    }
}
