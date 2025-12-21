using FluentValidation;
using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Features.Students.Validators;

public sealed class SyncDependentBirthCertCommandValidator : BaseValidator<SyncDependentBirthCertCommand>
{
    public SyncDependentBirthCertCommandValidator() {
        RuleFor(x => x.Id).GreaterThan(0).WithName("شناسه تکفل");
    }
}
