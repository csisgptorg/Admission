using Csis.Admission.Application.Features.NonIranianStudent.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.NonIranianStudent.Validators;

public sealed class UpdateNonIranianDependentDivorceRequestCommandValidator : BaseValidator<UpdateNonIranianDependentDivorceRequestCommand>
{
    public UpdateNonIranianDependentDivorceRequestCommandValidator() {
        RuleFor(x => x.DivorceDate).NotEmpty().WithMessage("تاریخ طلاق نمی تواند خالی باشد");
        RuleFor(x => x.DependentId).NotNull().WithMessage("آیدی تکفل نمی تواند خالی باشد");
    }
}
