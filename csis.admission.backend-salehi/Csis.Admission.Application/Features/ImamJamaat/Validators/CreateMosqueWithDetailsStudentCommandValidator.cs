using Csis.Admission.Application.Features.ImamJamaat.Commands;
using Csis.Admission.Application.Features.ImamJamaat.Dtos.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.ImamJamaat.Validators;

public class CreateMosqueWithDetailsStudentCommandValidator : AbstractValidator<CreateMosqueWithDetailsStudentCommand>
{
    public CreateMosqueWithDetailsStudentCommandValidator() {

        When(x => x.MosqueAddressId.HasValue, () => {
            RuleFor(x => x.MosqueAddress)
                .Null();
        });

        When(x => !x.MosqueAddressId.HasValue, () => {
            RuleFor(x => x.MosqueAddress)
                .NotNull()
                .SetValidator(new ActivityDtoValidator.MosqueAddressDtoValidator());
        });

        RuleFor(x => x.Mosque)
            .NotNull()
            .SetValidator(new MosqueDtoValidator());

        RuleFor(x => x.ImamJamaat)
            .NotNull()
            .SetValidator(new ImamJamaatStudentDtoValidator());

        RuleFor(x => x.MosqueActivity)
            .NotNull()
            .SetValidator(new ActivityDtoValidator());
    }
}

public class ImamJamaatStudentDtoValidator : AbstractValidator<ImamJamaatStudentCommandDto>
{
    public ImamJamaatStudentDtoValidator() {
        RuleFor(x => x.StartYear.Value.Year)
            .InclusiveBetween(1921, DateTime.Now.Year)
            .WithMessage("سال شمسی باید بین 1300 تا سال جاری باشد.");

        RuleFor(x => x.EndYear)
            .Null()
            .When(x => x.IsCurrentlyImam == true)
            .WithMessage("اگر امام جماعت در حال امامت است، سال پایان باید فاقد مقدار باشد.");

        RuleFor(x => x.IsCurrentlyImam)
            .NotNull()
            .When(x => x.EndYear == null)
            .WithMessage("اگر سال پایان فاقد مقدار است، وضعیت امامت مربوط به امام جماعت باید مشخص باشد.");

        RuleFor(x => x.IsReceivingMonthlyNonCashAssistance)
            .NotNull().WithMessage("وضعیت دریافت کمک مالی از سازمان‌ها باید مشخص باشد.");

        When(x => x.DailyPresenceType == PresenceType.PrayerAndPromotional, () => {
            RuleFor(x => x.AverageDailyPresenceHours)
                .NotNull().WithMessage("میانگین ساعت حضور روزانه الزامی است.")
                .InclusiveBetween(0.1f, 15.0f)
                .WithMessage("عدد ساعت باید بین ۰.۱ تا ۱۵ باشد.");
        });

        When(x => x.DailyPresenceType == PresenceType.PrayerOnly, () => {
            RuleFor(x => x.AverageDailyPresenceHours)
                .Null()
                .WithMessage("برای حالت فقط نماز، نباید عددی به عنوان میانگین ساعت حضور وارد شود.");
        });

        RuleFor(x => x.ImamAnnualActivityStatus)
            .IsInEnum()
            .WithMessage("وضعیت فعالیت سالانه مبلغ الزامی است.");


        When(x => x.IsReceivingMonthlyNonCashAssistance, () => {
            RuleFor(x => x.MonthlyNonCashAssistance)
                .Must(flags => flags != null && flags.Count > 0)
                .WithMessage("مبلغ دریافتی ماهانه از سازمان‌ها باید دارای مقدار باشد.");
        });

        RuleFor(x => x.IsReceivingMonthlyPaymentFromMosque)
            .NotNull().WithMessage("وضعیت دریافت کمک مالی از مردم باید مشخص باشد.");

        RuleFor(x => x.MonthlyPaymentFromMosque)
            .NotNull()
            .When(x => x.IsReceivingMonthlyPaymentFromMosque)
            .WithMessage("مبلغ دریافتی ماهانه از مردم باید دارای مقدار باشد.");

        When(x => x.IsSpouseActiveInSameMosque.HasValue, () => {
            RuleFor(x => x.ActiveSpousesInMosque)
                .NotNull()
                .WithMessage("لیست همسران فعال امام جماعت در مسجد الزامی است.")
                .Must(spouses => spouses.Count >= 0)
                .WithMessage("حداقل یک همسر فعال در مسجد باید مشخص شود.");
        });

        When(x => x.IsCurrentlyImam != true && x.EndYear != null, () => {
            RuleFor(x => x.EndYear)
                .Must((x, end) => end.Value.Year >= x.StartYear.Value.Year)
                .WithMessage("سال پایان باید بزرگتر یا مساوی سال شروع باشد.")
                .Must(end => end.Value.Year >= 1921 && end.Value.Year <= DateTime.Now.Year)
                .WithMessage("سال شمسی باید بین 1300 تا سال جاری باشد.");

            RuleFor(x => x.EndYear)
                .Must((x, end) => {
                    return end.Value.Year != x.StartYear.Value.Year || end.Value.Month >= x.StartYear.Value.Month;
                })
                .WithMessage("اگر سال برابر باشد، ماه پایان باید مساوی یا بعد از ماه شروع باشد.");

            RuleFor(x => x.EndYear)
                .Must((x, end) => {
                    return end.Value.Year != x.StartYear.Value.Year || end.Value.Month != x.StartYear.Value.Month || end.Value.Day >= x.StartYear.Value.Day;
                })
                .WithMessage("اگر سال و ماه برابر باشد، روز پایان باید بزرگتر یا مساوی روز شروع باشد.");
        });

        RuleFor(x => x.AppointedBy)
            .IsInEnum()
            .WithMessage("نهاد صادرکننده حکم امام جماعت الزامی است.");

        When(x => x.AppointedBy == AppointedByType.Other, () => {
            RuleFor(x => x.AppointedByOtherOrganization)
                .NotNull()
                .WithMessage("نام نهاد مربوطه الزامیست!");
        });

        When(x => x.AppointedBy == AppointedByType.WithoutWarrant, () => {
            RuleFor(x => x.IsTrusteesBoardMember)
                .NotNull()
                .WithMessage("نام شخص یا سازمان الزامیست!");
        });

        When(x => x.IsReceivingMonthlyPaymentFromOrganizations.Value, () => {
            RuleFor(x => x.MonthlyPaymentFromOrganizations)
                .NotNull()
                .WithMessage("مبلغ دریافتی ماهانه از سازمان‌ها باید دارای مقدار باشد.");
        });
    }
}
