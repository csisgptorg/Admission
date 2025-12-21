//using Csis.Admission.Application.Features.Employments.Commands;
//using FluentValidation;

//namespace Csis.Admission.Application.Features.Employments.Validators;

//public class CreateOrUpdateDependentEmploymentRequestCommandValidator
//    : AbstractValidator<CreateOrUpdateDependentEmploymentRequestCommand>
//{
//    public CreateOrUpdateDependentEmploymentRequestCommandValidator() {
        
//        RuleFor(x => x.DependentId).GreaterThan(0).WithMessage("شناسه فرد تحت تکفل الزامی است.");

//        RuleFor(x => x.IsEmployee).NotNull().WithMessage("تعیین وضعیت اشتغال الزامی است.");

//        When(x => x.IsEmployee == true, () => {
//            RuleFor(x => x.EmployeeName).NotEmpty().WithMessage("ثبت نام محل کار الزامی است.");
//            RuleFor(x => x.EmployeeAddress).NotEmpty().WithMessage("ثبت آدرس محل کار الزامی است.");
//        });

//        When(x => x.IsEmployeeInHowze == true, () => {
//            RuleFor(x => x.HowzeTypeId).NotNull().WithMessage("انتخاب مرکز حوزوی محل اشتغال الزامی است.");
//        });

//        RuleFor(x => x.IsRetried).NotNull().WithMessage("تعیین وضعیت بازنشستگی الزامی است.");

//        When(x => x.HasAnotherBaseInsurance == true, () => {
//            RuleFor(x => x.InsurancePlaceName).NotEmpty().WithMessage("ثبت نام محل بیمه پایه الزامی است.");
//            RuleFor(x => x.InsurancePlaceAddress).NotEmpty().WithMessage("ثبت آدرس محل بیمه پایه الزامی است.");
//            RuleFor(x => x.InsuranceTypeId).NotNull().WithMessage("انتخاب نوع بیمه پایه الزامی است.");
//        });

//        When(x => x.HasAnotherSupInsurance == true, () => {
//            RuleFor(x => x.Reference).NotNull().WithMessage("انتخاب مرجع بیمه تکمیلی الزامی است.");
//        });
//    }
//}
