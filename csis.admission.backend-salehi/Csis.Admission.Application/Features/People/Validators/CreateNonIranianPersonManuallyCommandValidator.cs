using Csis.Admission.Application.Features.People.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.People.Validators;

// validation
internal sealed class CreateNonIranianPersonManuallyCommandValidator : AbstractValidator<CreateNonIranianPersonManuallyCommand>
{
    public CreateNonIranianPersonManuallyCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("وارد کردن {PropertyName} الزامی است.")
            .MaximumLength(100).WithMessage("{PropertyName} نمی‌تواند بیشتر از {MaxLength} کاراکتر باشد.");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("وارد کردن {PropertyName} الزامی است.")
            .MaximumLength(100).WithMessage("{PropertyName} نمی‌تواند بیشتر از {MaxLength} کاراکتر باشد.");
        RuleFor(x => x.PassportNumber)
            .NotEmpty().WithMessage("وارد کردن {PropertyName} الزامی است.")
            .MaximumLength(50).WithMessage("{PropertyName} نمی‌تواند بیشتر از {MaxLength} کاراکتر باشد.");
        RuleFor(x => x.YektaCode)
            .NotEmpty().WithMessage("وارد کردن {PropertyName} الزامی است.")
            .MaximumLength(50).WithMessage("{PropertyName} نمی‌تواند بیشتر از {MaxLength} کاراکتر باشد.");
        RuleFor(x => x.BankAccountNumber)
            .NotEmpty().WithMessage("وارد کردن {PropertyName} الزامی است.")
            .MaximumLength(50).WithMessage("{PropertyName} نمی‌تواند بیشتر از {MaxLength} کاراکتر باشد.");
        RuleFor(x => x.ShebaNumber)
            .NotEmpty().WithMessage("وارد کردن {PropertyName} الزامی است.")
            .MaximumLength(50).WithMessage("{PropertyName} نمی‌تواند بیشتر از {MaxLength} کاراکتر باشد.");
    }
}
