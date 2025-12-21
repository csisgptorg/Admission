using Csis.Admission.Application.Features.CaseFilings.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.CaseFilings.Validator;

internal sealed class ConfirmEmploymentValidator : AbstractValidator<ConfirmEmploymentCommand>
{
    public ConfirmEmploymentValidator() {
        RuleFor(x => x.Token).NotEmpty().WithMessage("توکن الزامی است.");
        RuleFor(x => x.IsEmployee).NotNull().WithMessage("وضعیت اشتغال الزامی است.");
      
        When(x => x.IsEmployee, () => {
            RuleFor(x => x.EmployeeName).NotEmpty().WithMessage("نام محل کار الزامی است.");
            RuleFor(x => x.EmployeeAddress).NotEmpty().WithMessage("آدرس محل کار الزامی است.");
        });
        When(x => x.HasAnotherBaseInsurance.Value, () => {
            RuleFor(x => x.InsurancePlaceName).NotEmpty().WithMessage("نام محل بیمه پایه دیگر الزامی است.");
            RuleFor(x => x.InsurancePlaceAddress).NotEmpty().WithMessage("آدرس محل بیمه پایه دیگر الزامی است.");
        });
        When(x => x.IsEmployeeInHowze.Value, () => {
            RuleFor(x => x.HowzeTypeId).IsInEnum().WithMessage("نوع اشتغال در حوزه الزامی است.");
        });
    }
}
