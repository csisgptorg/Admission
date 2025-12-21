//using FluentValidation;
//using Csis.Admission.Application.Features.Employments.Commands;

//namespace Csis.Admission.Application.Features.Employments.Validators;

///// <summary>اعتبار سنجی اشتغال</summary>
//public sealed class CreateOrUpdateStudentEmploymentRequestCommandValidator : AbstractValidator<CreateOrUpdateStudentEmploymentRequestCommand>
//{
//    public CreateOrUpdateStudentEmploymentRequestCommandValidator() {

//        RuleFor(x => x.HasIncome).NotNull().WithMessage("تعیین وضعیت داشتن درآمد الزامی است.");

//        RuleFor(x => x.HasSufficientIncome).NotNull().WithMessage("تعیین کفایت مجموع درآمد نسبت به هزینه‌های زندگی الزامی است.");

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
