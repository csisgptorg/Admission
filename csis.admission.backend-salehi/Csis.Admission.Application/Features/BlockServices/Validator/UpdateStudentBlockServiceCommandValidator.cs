using FluentValidation;
using Csis.Admission.Application.Features.BlockServices.Commands;

namespace Csis.Admission.Application.Features.CaseFilings.Validator;

internal sealed class UpdateStudentBlockServiceCommandValidator : AbstractValidator<UpdateStudentBlockServiceCommand>
{
    public UpdateStudentBlockServiceCommandValidator() {
        RuleFor(x => x.Reason).NotEmpty().WithName("علت");
    }
}
