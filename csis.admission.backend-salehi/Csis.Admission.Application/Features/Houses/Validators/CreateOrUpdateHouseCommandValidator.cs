using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.Houses.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.Houses.Validators;

public sealed class CreateOrUpdateHouseCommandValidator : BaseValidator<CreateOrUpdateHouseRequestCommand>
{
    public CreateOrUpdateHouseCommandValidator() {

        RuleFor(x => x.HouseStatus)
            .IsInEnum().WithMessage("وضعیت سکونت نامعتبر است.");

        // ـــــــــ حمایتی ـــــــــ
        When(x => x.HouseStatus == HouseStatus.Supportive, () => {
            RuleFor(x => x.HouseStatusItem)
                .NotNull().WithMessage("انتخاب جزئیات وضعیت سکونت برای خانه حمایتی الزامی است.");

            RuleFor(x => x.HasHouse)
                .NotNull().WithMessage("وضعیت «دارای مسکن شخصی» الزامی است.");

            RuleFor(x => x.HasLand)
                .NotNull().WithMessage("وضعیت «دارای زمین شخصی» الزامی است.");

            RuleFor(x => x.LiveInCell)
                .NotNull().WithMessage("وضعیت «سکونت در حجره/خوابگاه» الزامی است.");
        });

        // ـــــــــ اجاره‌ای/رهنی ـــــــــ
        When(x => x.HouseStatus == HouseStatus.RentalOrMortgage, () => {

            RuleFor(x => x.Documents)
                .Must(list => list.Count() >= 2)
                .WithMessage("مدارک اجاره نامه به صورت پشت و رو الزامی است.");

            // باید مشخص کند خانه شخصی دارد یا خیر
            RuleFor(x => x.HasHouse)
                .NotNull().WithMessage("در حالت اجاره/رهن، تعیین «وضعیت مسکن شخصی» الزامی است.");

            // اطلاعات موجر
            RuleFor(x => x.Tenant.HostName)
                .NotEmpty().WithMessage("نام صاحب‌خانه الزامی است.")
                .MaximumLength(100).WithMessage("حداکثر ۱۰۰ کاراکتر.");

            RuleFor(x => x.Tenant.HostMobile)
                .MaximumLength(100).When(x => !string.IsNullOrWhiteSpace(x.Tenant.HostMobile))
                .WithMessage("حداکثر ۱۰۰ کاراکتر.");

            // مبالغ: حداقل یکی از رهن یا اجاره باید > 0 باشد
            RuleFor(x => x)
                .Must(x => (x.Tenant.MortgageAmount.GetValueOrDefault() > 0M)
                        || (x.Tenant.RentAmount.GetValueOrDefault() > 0M))
                .WithMessage("حداقل یکی از مبلغ رهن یا اجاره باید وارد شود.");

            RuleFor(x => x.Tenant.MortgageAmount)
                .GreaterThanOrEqualTo(0).When(x => x.Tenant.MortgageAmount.HasValue)
                .WithMessage("مبلغ رهن نامعتبر است.");

            RuleFor(x => x.Tenant.RentAmount)
                .GreaterThanOrEqualTo(0).When(x => x.Tenant.RentAmount.HasValue)
                .WithMessage("مبلغ اجاره نامعتبر است.");

            // تاریخ اتمام قرارداد: الزامی و در آینده
            RuleFor(x => x.Tenant.EndDate)
                .NotNull().WithMessage("تاریخ پایان قرارداد الزامی است.")
                .Must(d => d!.StringDateToInt().Value.ToDateTime().Date.ToPersianInteger() > DateTime.Now.Date.ToPersianInteger())
                .WithMessage("تاریخ پایان قرارداد باید در آینده باشد.");

        });

        When(x => x.Documents != null && x.Documents.Any(), () => {
            RuleForEach(x => x.Documents).ChildRules(
                x => x.RuleFor(d => d.FileId).NotEmpty().WithName("شناسه فایل"));
        });
    }
}
