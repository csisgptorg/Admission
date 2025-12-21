using Csis.Admission.Application.Features.People.Commands;
using Csis.Utilities.Validation;
using FluentValidation;

namespace Csis.Admission.Application.Features.People.Validators;

public sealed class CreatePersonCommandValidator : BaseValidator<CreatePersonCommand>
{
    public CreatePersonCommandValidator() {
        RuleFor(x => x.BankAccountNumber).MaximumLength(13).WithName("شماره حساب");
        RuleFor(x => x.ShebaNumber).MaximumLength(26).WithName("شماره شبا");
        RuleFor(x => x.BirthCertDescription).MaximumLength(100).WithName("توضیحات شناسنامه");
        RuleFor(x => x.FatherName).NotEmpty().MaximumLength(30).WithName("نام پدر");
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(40).WithName("نام");
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(35).WithName("نام خانوادگی");
        RuleFor(x => x.Mobile).NotEmpty().MaximumLength(11).WithName("تلفن همراه");
        RuleFor(x => x.DeathCause).IsInEnum().WithName("علت فوت");
        RuleFor(x => x.Gender).IsInEnum().WithName("جنسیت");
        RuleFor(x => x.Religion).IsInEnum().WithName("مذهب");
        RuleFor(x => x.SingleStatus).IsInEnum().WithName("وضعیت تجرد");

        When(x => x.Citizenship == Citizenship.Iranian, () => {
            RuleFor(x => x.BirthCertIssuePlace).NotEmpty().MaximumLength(50).WithName("محل صدور شناسنامه");
            RuleFor(x => x.BirthCertIssueProvince).NotEmpty().MaximumLength(50).WithName("استان محل صدور شناسنامه");
            RuleFor(x => x.BirthCertNumber).NotEmpty().MaximumLength(10).WithName("شماره شناسنامه");
            RuleFor(x => x.BirthCertSeri).NotEmpty().MaximumLength(6).WithName("سری شناسنامه");
            RuleFor(x => x.BirthCertSerial).NotEmpty().Numeric().MaximumLength(6).WithName("سریال شناسنامه");
            RuleFor(x => x.NationalCode).NotEmpty().MaximumLength(10).WithName("کد ملی");
        });

        When(x => x.Citizenship == Citizenship.NonIranian, () => {
            RuleFor(x => x.YektaCode).NotEmpty().MaximumLength(12).WithName("شناسه یکتا");
            RuleFor(x => x.PassportNumber).NotEmpty().MaximumLength(20).WithName("شماره پاسپورت");
        });
    }
}
