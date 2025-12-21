using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.NonIranianStudent.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.NonIranianStudent.Validators;

public sealed class UpdateNonIranianWifeDivorceRequestCommandValidator : BaseValidator<UpdateNonIranianWifeDivorceRequestCommand>
{
    public UpdateNonIranianWifeDivorceRequestCommandValidator() {
        RuleFor(x => x.DependentId)
            .NotNull().WithMessage("آیدی همسر نمی تواند خالی باشد.")
            .GreaterThan(0).WithMessage("آیدی همسر باید بزرگتر از صفر باشد.");

        RuleFor(x => x.DivorceDate)
            .NotEmpty().WithMessage("تاریخ طلاق نمی تواند خالی باشد.")
            .Must(x => x.HasValue()).WithMessage("تاریخ طلاق وارد شده معتبر نیست.")
            .Must(x => x.StringDateToInt().Value <= DateTime.Now.ToPersianInteger()).WithMessage("تاریخ طلاق وارد شده معتبر نیست.");
    }
}
