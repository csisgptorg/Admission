using FluentValidation;
using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Features.Students.Validators;

/// <summary>اعتبار سنجی</summary>
public sealed class UpdateNonIranianDependentBirthCertCommandValidator : BaseValidator<UpdateNonIranianDependentBirthCertCommand>
{
    /// <summary>اعتبار سنجی</summary>
    public UpdateNonIranianDependentBirthCertCommandValidator() {
        RuleFor(x => x.Id).GreaterThan(0).WithName("شناسه تکفل");
        RuleFor(x => x.YektaCode).NotEmpty().WithName("کد یکتا.");
        RuleFor(x => x.Religion).IsInEnum().WithName("مذهب");
    }
}
