using Csis.Admission.Application.Features.ImamJamaat.Commands;
using Csis.Admission.Application.Features.ImamJamaat.Dtos.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.ImamJamaat.Validators;
public class CreateMosqueWithDetailsCommandValidator : AbstractValidator<CreateMosqueWithDetailsCommand>
{
    public CreateMosqueWithDetailsCommandValidator() {

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
            .SetValidator(new ImamJamaatDtoValidator());

        RuleFor(x => x.MosqueActivity)
            .NotNull()
            .SetValidator(new ActivityDtoValidator());
    }
}

public class MosqueDtoValidator : AbstractValidator<MosqueCommandDto>
{
    public MosqueDtoValidator() {
        RuleFor(x => x.OfficialName)
            .NotEmpty().WithMessage("نام رسمی مسجد الزامی است.");

        RuleFor(x => x.PostalCode)
            .NotNull()
            .When(x => !x.MosqueHasNotPostalCode.HasValue)
            .WithMessage("کد پستی الزامی است.")
            .Null()
            .When(x => x.MosqueHasNotPostalCode.Value)
            .WithMessage("اگر مسجد کد پستی ندارد، کد پستی باید فاقد مقدار باشد.");


        RuleFor(x => x.MosqueFormRole)
            .IsInEnum().WithMessage("نقش پرکننده فرم مسجد نامعتبر است.");

        RuleFor(x => x.PlaceType)
            .IsInEnum().WithMessage("نوع محل فعالیت نامعتبر است.");

        RuleFor(x => x.ClergyHouseStatus)
            .NotNull().When(x => (bool) x.HasClergyHouse)
            .WithMessage("وضعیت خانه عالم الزامی است اگر خانه عالم وجود دارد.")
            .Null().When(x => (bool) !x.HasClergyHouse)
            .WithMessage("اگر خانه عالم وجود ندارد، وضعیت آن باید 'فاقد مقدار' باشد.");

        RuleFor(x => x.MosqueAnnualActivityStatus)
            .IsInEnum()
            .WithMessage("وضعیت فعالیت سالانه مسجد الزامی است.");

    }
}

public class ImamJamaatDtoValidator : AbstractValidator<ImamJamaatCommandDto>
{
    public ImamJamaatDtoValidator() {
        RuleFor(x => x.CodM)
            .NotEmpty().WithMessage("کد مرکز الزامی است.");

        RuleFor(x => x.StartYear.Value.Year)
            .InclusiveBetween(1921, DateTime.Now.Year)
            .WithMessage("سال شمسی باید بین 1300 تا سال جاری باشد.");

        RuleFor(x => x.IsReceivingMonthlyNonCashAssistance)
            .NotNull().WithMessage("وضعیت دریافت کمک مالی از سازمان‌ها باید مشخص باشد.");

        RuleFor(x => x.DailyPresenceType)
            .IsInEnum()
            .WithMessage("انتخاب وضعیت حضور روزانه الزامی است.");

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

        RuleFor(x => x.EndYear)
            .Null()
            .When(x => x.IsCurrentlyImam == true)
            .WithMessage("اگر امام جماعت در حال امامت است، سال پایان باید فاقد مقدار باشد.");

        RuleFor(x => x.IsCurrentlyImam)
            .NotNull()
            .When(x => x.EndYear == null)
            .WithMessage("اگر سال پایان فاقد مقدار است، وضعیت امامت مربوط به امام جماعت باید مشخص باشد.");

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
            .IsInEnum();

        When(x => x.ReportsSubmitted.Value, () => {
            RuleFor(x => x.ReportingOrganization)
                .NotNull()
                .When(x => x.ReportingOrganizationType == AppointedByType.Other)
                .WithMessage("در صورت ارسال گزارش، به سازمانی خارج از لیست وارد کردن نام آن سازمان الزامیست!");

            RuleFor(x => x.ReportingOrganizationType)
                .NotNull()
                .WithMessage("نوع سازمان الزامیست!");
        });


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

public class ActivityDtoValidator : AbstractValidator<MosqueActivityCommandDto>
{
    public ActivityDtoValidator() {
        RuleFor(x => x.ActivityStatus)
           .IsInEnum().WithMessage("وضعیت فعالیت مسجد نامعتبر است.");

        // فقط نماز
        When(x => x.ActivityStatus == MosqueActivityType.OnlyPrayer, () => {
            RuleFor(x => x.PrayerTimesFlags)
                .NotNull().Must(flags => flags.Any(f => f != 0))
                .WithMessage("حداقل یک وعده نماز باید انتخاب شود.");

            RuleFor(x => x.QuranProgramFlags).Empty().WithMessage("در حالت 'فقط نماز' نباید فعالیت قرآنی ثبت شود.");
            RuleFor(x => x.EducationalClassFlags).Empty().WithMessage("در حالت 'فقط نماز' نباید کلاس آموزشی ثبت شود.");

            RuleFor(x => x.RegularLectures)
            .Null().WithMessage("در حالت 'فقط نماز'، سخنرانی منظم نباید انتخاب شود.");

            RuleForEach(x => new[] { x.MorningPrayerStatus, x.NoonPrayerStatus, x.EveningPrayerStatus })
                .IsInEnum().WithMessage("وضعیت امام جماعت نامعتبر است.");

            RuleFor(x => x.MorningPrayerStatus)
                .IsInEnum()
                .When(x => x.PrayerTimesFlags.Contains((short) PrayerTimes.Morning))
                .WithMessage("در صورت انتخاب نماز صبح، وضعیت امام جماعت صبح باید مشخص شود.");

            RuleFor(x => x.NoonPrayerStatus)
                .IsInEnum()
                .When(x => x.PrayerTimesFlags.Contains((short) PrayerTimes.Noon))
                .WithMessage("در صورت انتخاب نماز ظهر، وضعیت امام جماعت ظهر باید مشخص شود.");

            RuleFor(x => x.EveningPrayerStatus)
                .IsInEnum()
                .When(x => x.PrayerTimesFlags.Contains((short) PrayerTimes.Evening))
                .WithMessage("در صورت انتخاب نماز مغرب، وضعیت امام جماعت مغرب باید مشخص شود.");

        });

        // نماز + فعالیت فرهنگی اجتماعی
        When(x => x.ActivityStatus == MosqueActivityType.PrayerWithSocialCultural, () => {
            RuleFor(x => x.PrayerTimesFlags)
                .NotNull().Must(flags => flags.Any(f => f != 0))
                .WithMessage("حداقل یک وعده نماز باید انتخاب شود.");

            RuleFor(x => x.QuranProgramFlags)
                .NotNull().Must(flags => flags.Any(f => f != 0))
                .WithMessage("حداقل یک فعالیت قرآنی باید انتخاب شود.");

            RuleFor(x => x.EducationalClassFlags)
                .NotNull().Must(flags => flags.Any(f => f != 0))
                .WithMessage("حداقل یک کلاس آموزشی باید انتخاب شود.");

            RuleFor(x => x.RegularLectures)
                .NotNull().WithMessage("در حالت 'نماز + فعالیت فرهنگی اجتماعی'، سخنرانی منظم باید انتخاب شود.");

            RuleForEach(x => new[] { x.MorningPrayerStatus, x.NoonPrayerStatus, x.EveningPrayerStatus })
                .IsInEnum().WithMessage("وضعیت امام جماعت نامعتبر است.");

            RuleFor(x => x.MorningPrayerStatus)
                .IsInEnum()
                .When(x => x.PrayerTimesFlags.Contains((short) PrayerTimes.Morning))
                .WithMessage("در صورت انتخاب نماز صبح، وضعیت امام جماعت صبح باید مشخص شود.");

            RuleFor(x => x.NoonPrayerStatus)
                .IsInEnum()
                .When(x => x.PrayerTimesFlags.Contains((short) PrayerTimes.Noon))
                .WithMessage("در صورت انتخاب نماز ظهر، وضعیت امام جماعت ظهر باید مشخص شود.");

            RuleFor(x => x.EveningPrayerStatus)
                .IsInEnum()
                .When(x => x.PrayerTimesFlags.Contains((short) PrayerTimes.Evening))
                .WithMessage("در صورت انتخاب نماز مغرب، وضعیت امام جماعت مغرب باید مشخص شود.");

        });

        When(x => x.OrganizerOfClassesAndProgramsInMosque.Contains((short) OrganizerOfClassesAndProgramsInMosque.Other), () => {
            RuleFor(x => x.OrganizerOfClassesAndProgramsInMosqueOther)
                .NotNull()
                .WithMessage("لطفا نام برگزار کننده مراسمات را وارد نمایید.");
        });


    }

    public class MosqueAddressDtoValidator : AbstractValidator<MosqueAddressCommandDto>
    {
        public MosqueAddressDtoValidator() {
            RuleFor(x => x.ProvinceId)
                .NotNull().WithMessage("استان الزامی است.");

            RuleFor(x => x.CityId)
                .NotNull().WithMessage("شهرستان الزامی است.");

            RuleFor(x => x.PortionId)
                .NotNull().WithMessage("بخش الزامی است.");

            RuleFor(x => x.TownId)
                .NotNull().WithMessage("شهر الزامی است.");

            RuleFor(x => x.RuralId)
                .NotNull().WithMessage("دهستان الزامی است.");

            RuleFor(x => x.Township)
                .MaximumLength(100).WithMessage("حداکثر طول شهرک ۱۰۰ کاراکتر است.");

            RuleFor(x => x.Village)
                .MaximumLength(100).WithMessage("حداکثر طول روستا ۱۰۰ کاراکتر است.");

            RuleFor(x => x.District)
                .MaximumLength(100).WithMessage("حداکثر طول محله ۱۰۰ کاراکتر است.");

            RuleFor(x => x.Dorp)
                .MaximumLength(100).WithMessage("حداکثر طول شهرک ۱۰۰ کاراکتر است.");

            RuleFor(x => x.Avenue)
                .MaximumLength(100).WithMessage("حداکثر طول خیابان اصلی ۱۰۰ کاراکتر است.");

            RuleFor(x => x.Street)
                .MaximumLength(100).WithMessage("حداکثر طول خیابان فرعی ۱۰۰ کاراکتر است.");

            RuleFor(x => x.Alley)
                .MaximumLength(100).WithMessage("حداکثر طول کوچه اصلی ۱۰۰ کاراکتر است.");

            RuleFor(x => x.Lane)
                .MaximumLength(100).WithMessage("حداکثر طول کوچه فرعی ۱۰۰ کاراکتر است.");

            RuleFor(x => x.Number)
                .MaximumLength(20).WithMessage("حداکثر طول پلاک ۲۰ کاراکتر است.");

            RuleFor(x => x.Complex)
                .MaximumLength(100).WithMessage("حداکثر طول مجتمع ۱۰۰ کاراکتر است.");

            RuleFor(x => x.Block)
                .MaximumLength(20).WithMessage("حداکثر طول بلوک ۲۰ کاراکتر است.");

            RuleFor(x => x.Unit)
                .MaximumLength(20).WithMessage("حداکثر طول واحد ۲۰ کاراکتر است.");

            RuleFor(x => x.Floor)
                .InclusiveBetween((short) 0, (short) 100).When(x => x.Floor.HasValue)
                .WithMessage("شماره طبقه باید بین ۰ تا ۱۰۰ باشد.");

            RuleFor(x => x.ZipCode)
                .InclusiveBetween(1000000000, 9999999999)
                .When(x => x.ZipCode.HasValue)
                .WithMessage("کد پستی باید ۱۰ رقمی باشد.");
        }
    }

}
