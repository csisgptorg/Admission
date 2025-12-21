using Csis.Admission.Application.Features.NonIranianStudent.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.NonIranianStudent.Validators;

public sealed class UpdateNonIranianDependentMarriageRequestCommandValidator : BaseValidator<UpdateNonIranianDependentMarriageRequestCommand>
{
    public UpdateNonIranianDependentMarriageRequestCommandValidator() {
        RuleFor(x => x.MarriageDate).NotEmpty().WithMessage("تاریخ ازدواج نمی تواند خالی باشد");
        RuleFor(x => x.DependentId).NotNull().WithMessage("آیدی تکفل نمی تواند خالی باشد");
    }
}
