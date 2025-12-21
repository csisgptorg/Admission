using Csis.Admission.Application.Features.Students.NonIranian.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.Students.NonIranian.Validators;

// validation
internal sealed class CreateStudentDeathRequestCommandValidator : AbstractValidator<CreateStudentDeathRequestCommand>
{
    public CreateStudentDeathRequestCommandValidator()
    {
        RuleFor(x => x.Codm)
            .GreaterThan(0).WithMessage("کد مرکز طلبه باید بزرگتر از صفر باشد.");

        RuleFor(x => x.DeathDate)
            .NotNull().WithMessage("تاریخ فوت نمی‌تواند خالی باشد.");
    }
 
}
