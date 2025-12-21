using FluentValidation;
using Csis.Admission.Application.Features.BlockServices.Commands;

namespace Csis.Admission.Application.Features.CaseFilings.Validator;

internal sealed class UpdateDependentBlockServiceCommandValidator : AbstractValidator<UpdateDependentBlockServiceCommand>
{
    public UpdateDependentBlockServiceCommandValidator() {
        RuleFor(x => x.Reason).NotEmpty().WithName("علت");
    }
}
