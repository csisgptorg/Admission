using Csis.Admission.Application.Features.CaseFilings.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.CaseFilings.Validator;

public sealed class ConfirmIdentityInformationCommandValidator : BaseValidator<ConfirmIdentityInformationCommand>
{
    public ConfirmIdentityInformationCommandValidator() {
        RuleFor(x => x.Token).NotEmpty().WithName("توکن");
    }
}
