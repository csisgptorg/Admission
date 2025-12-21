using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.People.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.People.Validators;

// validation
internal sealed class CreateNonIranianPersonByInquiryCommandValidator : AbstractValidator<CreateNonIranianPersonByInquiryCommand>
{
    public CreateNonIranianPersonByInquiryCommandValidator() {
        RuleFor(x => x.YektaCode)
            .NotEmpty().WithMessage("شناسه یکتا الزامی است")
            .MaximumLength(50).WithMessage("شناسه یکتا نمی تواند بیشتر از 50 کاراکتر باشد");
        RuleFor(x => x.ResidenceExpireDate)
            .Must(date => string.IsNullOrEmpty(date) || !date.StringDateToInt().HasValue).WithMessage("تاریخ اعتبار اقامت وارد شده معتبر نمی باشد");
        RuleFor(x => x.BankAccountNumber)
            .MaximumLength(50).WithMessage("شماره حساب نمی تواند بیشتر از 50 کاراکتر باشد");
        RuleFor(x => x.ShebaNumber)
            .MaximumLength(50).WithMessage("شماره شبا نمی تواند بیشتر از 50 کاراکتر باشد");
        RuleFor(x => x.Mobile)
            .MaximumLength(20).WithMessage("تلفن همراه نمی تواند بیشتر از 20 کاراکتر باشد");
    }
}
