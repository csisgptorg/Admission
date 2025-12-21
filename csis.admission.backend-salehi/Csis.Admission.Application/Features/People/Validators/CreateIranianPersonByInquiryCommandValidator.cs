using Csis.Admission.Application.Features.People.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.People.Validators;

// validation
internal sealed class CreateIranianPersonByInquiryCommandValidator : AbstractValidator<CreateIranianPersonByInquiryCommand>
{
    public CreateIranianPersonByInquiryCommandValidator()
    {
        RuleFor(x => x.NationalCode)
            .NotEmpty().WithMessage("کد ملی الزامی است.")
            .Length(10).WithMessage("کد ملی باید 10 رقم باشد.")
            .Matches("^[0-9]{10}$").WithMessage("کد ملی باید فقط شامل ارقام باشد.");
        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("تاریخ تولد الزامی است.");
        RuleFor(x => x.Mobile)
            .NotEmpty().WithMessage("شماره موبایل الزامی است.")
            .Matches("^(09)([0-9]{9})$").WithMessage("شماره موبایل وارد شده معتبر نمی‌باشد.");
    }
}
