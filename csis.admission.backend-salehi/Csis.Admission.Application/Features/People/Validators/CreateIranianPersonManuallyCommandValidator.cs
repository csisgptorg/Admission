using Csis.Admission.Application.Features.People.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.People.Validators;

// validation
internal sealed class CreateIranianPersonManuallyCommandValidator : AbstractValidator<CreateIranianPersonManuallyCommand>
{
    public CreateIranianPersonManuallyCommandValidator()
    {
        RuleFor(x => x.NationalCode)
            .NotEmpty().WithMessage("کد ملی الزامی است.")
            .Length(10).WithMessage("کد ملی باید 10 رقم باشد.")
            .Matches("^[0-9]{10}$").WithMessage("کد ملی باید فقط شامل ارقام باشد.");
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("نام الزامی است.")
            .MaximumLength(100).WithMessage("نام نمی‌تواند بیش از 100 کاراکتر باشد.");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("نام خانوادگی الزامی است.")
            .MaximumLength(100).WithMessage("نام خانوادگی نمی‌تواند بیش از 100 کاراکتر باشد.");
        RuleFor(x => x.FatherName)
            .NotEmpty().WithMessage("نام پدر الزامی است.")
            .MaximumLength(100).WithMessage("نام پدر نمی‌تواند بیش از 100 کاراکتر باشد.");
        RuleFor(x => x.Mobile)
            .NotEmpty().WithMessage("تلفن همراه الزامی است.")
            .Matches(@"^09\d{9}$").WithMessage("فرمت تلفن همراه صحیح نمی‌باشد.");
        When(x => x.IsDead, () =>
        {
            RuleFor(x => x.DeathDate)
                .NotEmpty().WithMessage("تاریخ فوت الزامی است برای فرد مرحوم.");
        });
        RuleFor(x => x.BankAccountNumber)
            .NotEmpty().WithMessage("شماره حساب بانکی الزامی است.")
            .MaximumLength(13).WithMessage("شماره حساب بانکی نمی‌تواند بیش از 13 کاراکتر باشد.")
            .MinimumLength(13).WithMessage("شماره حساب بانکی باید 13 کاراکتر باشد.");
    }
}
