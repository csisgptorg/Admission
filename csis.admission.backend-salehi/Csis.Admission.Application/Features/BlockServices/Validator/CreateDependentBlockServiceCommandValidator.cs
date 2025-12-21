using FluentValidation;
using Csis.Admission.Application.Features.BlockServices.Commands;

namespace Csis.Admission.Application.Features.CaseFilings.Validator;

internal sealed class CreateDependentBlockServiceCommandValidator : AbstractValidator<CreateDependentBlockServiceCommand>
{
    public CreateDependentBlockServiceCommandValidator() {
        RuleFor(x => x.DependentId).NotEmpty().WithName("شناسه تکفل");
        RuleFor(x => x.BlockDate).NotEmpty().WithName("تاریخ انسداد");
        RuleFor(x => x.ServiceId).NotEmpty().WithName("خدمت");
        RuleFor(x => x.Reason).NotEmpty().WithName("علت");
    }
}
