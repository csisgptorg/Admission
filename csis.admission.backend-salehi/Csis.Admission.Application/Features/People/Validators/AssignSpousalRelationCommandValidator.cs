using Csis.Admission.Application.Common.Models.ValidateSpousalRelationship;
using Csis.Admission.Application.Features.People.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.People.Validators;

public sealed class AssignSpousalRelationCommandValidator : BaseValidator<AssignSpousalRelationCommand>
{
    public AssignSpousalRelationCommandValidator() {
        // شناسه‌ها
        RuleFor(x => x.HusbandPersonId)
            .NotNull().WithMessage("شناسه شوهر اجباری است")
            .GreaterThan(0).WithMessage("شناسه شوهر نامعتبر است");

        RuleFor(x => x.WifePersonId)
            .NotNull().WithMessage("شناسه همسر اجباری است")
            .GreaterThan(0).WithMessage("شناسه همسر نامعتبر است");

        RuleFor(x => x)
            .Must(x => x.HusbandPersonId != x.WifePersonId)
            .WithMessage("شناسه شوهر و همسر نمی‌تواند یکسان باشد");

        RuleFor(x => x.RelationType)
            .IsInEnum().WithMessage("نوع واقعه نامعتبر است");
    }
}
