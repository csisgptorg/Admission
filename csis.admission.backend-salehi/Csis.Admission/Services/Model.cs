using System.ComponentModel.DataAnnotations;

namespace Csis.Admission;

#region Enums
/// <summary>
/// جنسیت
/// </summary>
public enum Gender : short
{
    /// <summary>
    /// مرد
    /// </summary>
    [Display(Name = "مرد")]
    Male = 1,

    /// <summary>
    /// زن
    /// </summary>
    [Display(Name = "زن")]
    Female = 2
}

/// <summary>
/// وضعیت نقش آفرینی
/// </summary>
public enum ReligiousRoleStatus : short
{
    /// <summary>نقش آفرینی دارد</summary>
    [Display(Name = "نقش آفرینی دارد")]
    HasRole = 1,
    /// <summary>نقش آفرینی دارد ولی در قم یا مشهد مانده است</summary>
    [Display(Name = "نقش آفرینی دارد ولی در قم یا مشهد مانده است")]
    HasRoleButInQomOrMashhad = 2,
    /// <summary>نقش آفرینی ندارد</summary>
    HasNoRole = 3
}

/// <summary>مذهب</summary>
public enum Religion : short
{
    /// <inheritdoc/>
    /// <summary>شیعه</summary>
    [Display(Name = "شیعه")]
    Shia = 1,

    /// <inheritdoc/>
    /// <summary>سنی</summary>
    [Display(Name = "سنی")]
    Sunni = 2
}

/// <summary>وضعیت سکونت</summary>
public enum HouseStatus : short
{
    /// <summary>اجاره ای یا رهنی</summary>
    [Display(Name = "اجاره ای یا رهنی")]
    RentalOrMortgage = 2,

    /// <summary>شخصی</summary>
    [Display(Name = "شخصی")]
    Private = 5,

    /// <summary>حمایتی</summary>
    [Display(Name = "حمایتی")]
    Supportive = 11
}

/// <summary>عنوان فرم های اعتراضات</summary>
public enum ProtestFormTitle : short
{
    None = 0,
    [Display(Name = "فرم جیم")]
    FormJim = 1,

    [Display(Name = "سابقه مالکیت در سازمان ثبت اسناد")]
    OwnershipHistory = 2,

    [Display(Name = "سابقه دریافت تسهیلات از بانک مسکن")]
    HousingLoanHistory = 3,

    [Display(Name = "سابقه موجر بودن")]
    BeingLandlord = 4,

    [Display(Name = "سابقه خرید و فروش مسکن")]
    HousingBuySellHistory = 5,

    [Display(Name = "سابقه مسکن شخصی در پذیرش")]
    PersonalHousingHistory = 6,

    [Display(Name = "شناسایی اشتغال از طریق سامانه‌ها")]
    EmploymentIdentificationSystems = 8,

    [Display(Name = "دهک معیشتی")]
    LivelihoodDecile = 9,

    [Display(Name = "اشتغال تکفل")]
    EmploymentSupport = 10
}


/// <summary>
/// نوع تبلیغ
/// </summary>
public enum PreachKind : short
{
    /// <summary>
    /// امام جماعت
    /// </summary>
    [Display(Name = "امام جماعت")]
    ImamJamaat = 1,

    /// <summary>
    /// امام جمعه
    /// </summary>
    [Display(Name = "امام جمعه")]
    ImamJomeh = 2,

    /// <summary>
    /// مبلغ
    /// </summary>
    [Display(Name = "مبلغ")]
    Mobalegh = 3,

    /// <summary>
    /// طرح هجرت بلند مدت
    /// </summary>
    [Display(Name = "طرح هجرت بلند مدت")]
    TarhHejratBolandModat = 4,

    /// <summary>
    /// روحاني مستقر
    /// </summary>
    [Display(Name = "روحانی مستقر")]
    RohaniMostaghar = 5,

    /// <summary>
    /// ملا محلی سیستان و بلوچستان
    /// </summary>
    [Display(Name = "ملا محلی سیستان و بلوچستان")]
    MollaMahaliSistanBaluchestan = 6,

    /// <summary>
    /// خطبا خوزستان
    /// </summary>
    [Display(Name = "خطبا خوزستان")]
    KhatabaKhuzestan = 7,

    /// <summary>
    /// نوین (مجازی)
    /// </summary>
    [Display(Name = "(نوین (مجازی")]
    NovinMajazi = 8,

    /// <summary>
    /// امام جماعت مدارس
    /// </summary>
    [Display(Name = "امام جماعت مدارس")]
    ImamJamaatMadarese = 9,

    /// <summary>
    /// سفیران هدایت
    /// </summary>
    [Display(Name = "سفیران هدایت")]
    SafiranHedayat = 10,

    /// <summary>
    /// مستمر ساعتی
    /// </summary>
    [Display(Name = "مستمر ساعتی")]
    MostamarSaati = 11,

    /// <summary>
    /// مدارس امین
    /// </summary>
    [Display(Name = "مدارس امین")]
    MadreseAmin = 12
}

/// <summary>
/// انواع پژوهش
/// </summary>
public enum ResearchType : short
{
    /// <summary>
    /// تالیف کتاب
    /// </summary>
    [Display(Name = "تالیف کتاب")]
    BookWriting = 1,

    /// <summary>
    /// ترجمه مقاله
    /// </summary>
    [Display(Name = "ترجمه مقاله")]
    ArticleTranslation,

    /// <summary>
    /// تحقیق
    /// </summary>
    [Display(Name = "تحقیق")]
    Research,

    /// <summary>
    /// تالیف مقاله
    /// </summary>
    [Display(Name = "تالیف مقاله")]
    ArticleWriting,

    /// <summary>
    /// ترجمه مقاله (مقدار تکراری، ممکن است اصلاح شود)
    /// </summary>
    [Display(Name = "ترجمه مقاله (مقدار تکراری، ممکن است اصلاح شود)")]
    ArticleTranslationDuplicate,

    /// <summary>
    /// پروژه پژوهشی
    /// </summary>
    [Display(Name = "پروژه پژوهشی")]
    ResearchProject
}

/// <summary>وضعیت اشتغال به تحصیل</summary>
public enum EducationStatus : short
{
    /// <summary>محصل</summary>
    [Display(Name = "محصل")]
    Student = 1,

    /// <summary>فارغ التحصیل</summary>
    [Display(Name = "فارغ التحصیل")]
    Graduate = 2,

    /// <summary>انصراف</summary>
    [Display(Name = "انصراف")]
    Withdrawal = 3,

    /// <summary>اخراج</summary>
    [Display(Name = "اخراج")]
    Expelled = 4,

    /// <summary>عدم اشتغال به تحصیل</summary>
    [Display(Name = "عدم اشتغال به تحصیل")]
    NotEnrolled = 9
}

/// <summary>
/// مرجع تایید کنننده حوزوی
/// </summary>
public enum ApprovalCenter : short
{
    /// <summary>فاقد مرجع</summary>
    [Display(Name = "فاقد مرجع")]
    None = 0,

    /// <summary>
    /// مرکز مدیریت حوزه های علمیه برادران سراسر کشور
    /// </summary>
    [Display(Name = "مرکز مدیریت حوزه های علمیه برادران سراسر کشور")]
    CenterForManagementOfIslamicSeminariesOfBrothersNationwide = 1,

    /// <summary>
    /// مرکز مدیریت حوزه های علمیه خواهران سراسر کشور
    /// </summary>
    [Display(Name = "مرکز مدیریت حوزه های علمیه خواهران سراسر کشور")]
    CenterForManagementOfIslamicSeminariesOfSistersNationwide = 2,

    /// <summary>
    /// مرکز مدیریت حوزه های علمیه خراسان
    /// </summary>
    [Display(Name = "مرکز مدیریت حوزه های علمیه خراسان")]
    ManagementCenterOfIslamicSeminariesOfKhorasan = 3,

    /// <summary>
    /// مرکز مدیریت حوزه های علمیه اصفهان
    /// </summary>
    [Display(Name = "مرکز مدیریت حوزه های علمیه اصفهان")]
    ManagementCenterOfIslamicSeminariesOfIsfahan = 4,

    /// <summary>
    /// جامعه الزهرا س
    /// </summary>
    [Display(Name = "جامعه الزهرا س")]
    JameatAlZahra = 5,

    /// <summary>
    /// جامعه المصطفی العالمیه 
    /// </summary>
    [Display(Name = "جامعه المصطفی العالمیه")]
    AlMustafaInternationalUniversity = 6,

    /// <summary>
    /// دبیرخانه شورای برنامه ریزی اهل سنت
    /// </summary>
    [Display(Name = "دبیرخانه شورای برنامه ریزی اهل سنت")]
    SecretariatOfSunniPlanningCouncil = 7,

    /// <summary>
    /// کمیسیون
    /// </summary>
    [Display(Name = "کمیسیون")]
    Commission = 8
}

/// <summary>
/// تابعیت
/// </summary>
public enum Nationality : short
{
    /// <summary>
    /// ایرانی
    /// </summary>
    [Display(Name = "ایرانی")]
    Iranian = 1,

    /// <summary>
    /// غیر ایرانی
    /// </summary>
    [Display(Name = "غیر ایرانی")]
    NonIranian = 2
}


/// <summary>
/// وضعیت تجرد
/// </summary>
public enum SingleStatus : byte
{
    /// <summary>
    /// عدم ازدواج
    /// </summary>
    [Display(Name = "عدم ازدواج")]
    Single = 1,

    /// <summary>
    /// فوت همسر
    /// </summary>
    [Display(Name = "فوت همسر")]
    Widowed = 2,

    /// <summary>
    /// طلاق همسر
    /// </summary>
    [Display(Name = "طلاق همسر")]
    Divorced = 3
}

#endregion

#region Sakha
/// <summary>
/// تصویر پروفایل طلبه
/// </summary>
/// <param name="Codm"></param>
/// <param name="Image"></param>
public record StudentProfileImageResult(int Codm, string Image);

/// <param name="CaseCreationDate">تاریخ تشکیل پرونده</param>
/// <param name="CaseValidityDate">تاریخ اعتبار پرونده در صورت وجود)</param>
/// <param name="ValidityExtensionReasonTitle">علت تمدید اعتبار پرونده</param>
/// <param name="IsBlock">نشان‌دهنده اینکه پرونده مسدود شده است یا خیر</param>
/// <param name="BlockDate">تاریخ انسداد پرونده در صورت وجود)</param>
/// <param name="BlockReasonTitle">علت انسداد پرونده</param>
/// <param name="CanExtensionCase">امکان تمدید پرونده وجود دارد</param>
public record StudentCaseDtoResult(int Codm, string CaseCreationDate, bool IsActive, string CaseValidityDate, string ValidityExtensionReasonTitle,
    bool IsStudent, bool IsBlock, string BlockDate, string BlockReasonTitle, bool CanExtensionCase, float TotalScore);

public record StudentTotalReportResult(int Taraz, float TotalScore, float LivelihoodTotalScore, int MaxTaraz, float MaxTotalScore, float MaxLivelihoodTotalScore);

/// <summary>
/// کارت الکترونیکی طلبه
/// </summary>
/// <param name="Codm"></param>
/// <param name="FirstName"></param>
/// <param name="IsBlock"></param>
/// <param name="LastName"></param>
/// <param name="NationalCode"></param>
/// <param name="Grade"></param>
/// <param name="CaseValidityDate"></param>
/// <param name="IsTeacher"></param>
/// <param name="IsResearcher"></param>
/// <param name="Image"></param>
public record StudentElectronicIdCardResult(int Codm, string FirstName, bool IsBlock, string LastName, string NationalCode, int Grade, string? CaseValidityDate, bool IsTeacher, bool IsPreacher, bool IsResearcher);

#endregion

#region Pay Run Info
/// <summary>
/// اطلاعات طلبه در وب سرویس حقوق و دستمزد
/// </summary>
/// <param name="Codm"></param>
/// <param name="CaseCreationDate"></param>
/// <param name="IsActive"></param>
/// <param name="IsMarried"></param>
/// <param name="IsElite"></param>
/// <param name="HasFamily"></param>
/// <param name="Gender"></param>
/// <param name="Decile"></param>
/// <param name="BirthDate"></param>
/// <param name="ReligiousRoleStatus"></param>
/// <param name="TotalTargetScore"></param>
/// <param name="TotalNeedingScore"></param>
public record StudentDataForPayRunResult(int Codm, string? CaseCreationDate, bool? IsActive, bool? IsMarried, bool? IsElite, bool? HasFamily, Gender? Gender, short? Decile, string? BirthDate, ReligiousRoleStatus? ReligiousRoleStatus, float? TotalTargetScore, float? TotalNeedingScore);

#endregion

#region Location Info
/// <summary>
/// دریافت اطلاعات شهرستان ها
/// </summary>
/// <param name="Id"></param>
/// <param name="ProvinceId"></param>
/// <param name="Title"></param>
public sealed record CityResult(short? Id, short? ProvinceId, string Title);

/// <summary>
/// دریافت اطلاعات استان ها
/// </summary>
public sealed record ProvinceResult(short? Id, string Title);

#endregion

#region Housing Admission Info

/// <summary>
/// اطلاعات فعالیت‌های علمی و فرهنگی برای سامانه مسکن
/// </summary>
public sealed record HousingActivitiesInfoResult(
     /// <summary>
     /// لیست نخبگی
     /// </summary>
     List<EliteItemModel> EliteList,

/// <summary>
/// سطح نخبگی
/// </summary>
 EliteLevelModel EliteLevel,

/// <summary>
/// لیست تدریس
/// </summary>
 List<TeachingItemModel> TeachingList,

/// <summary>
/// سطح تدریس
/// </summary>
 GradeLevelModel TeachingLevel,

/// <summary>
/// لیست تبلیغ
/// </summary>
 List<PropagationItemModel> PropagationList,

/// <summary>
/// سطح تبلیغ
/// </summary>
 GradeLevelModel PropagationLevel,

/// <summary>
/// لیست پژوهش
/// </summary>
 List<ResearchItemModel> ResearchList,

/// <summary>
/// سطح پژوهش
/// </summary>
 GradeLevelModel ResearchLevel,

/// <summary>
/// تحصیلات حوزوی
/// </summary>
 List<SeminaryEducationItemModel> SeminaryEducationList
);



/// <summary>
/// مدل آیتم نخبگی
/// </summary>
/// <param name="Title">عنوان نخبگی</param>
/// <param name="StartDate">تاریخ شروع</param>
/// <param name="EndDate">تاریخ پایان</param>
public sealed record EliteItemModel(string Title, string StartDate, string EndDate);

/// <summary>
/// مدل آیتم تدریس
/// </summary>
/// <param name="Title">عنوان درس</param>
/// <param name="School">مدرسه</param>
/// <param name="EducationYear">سال تحصیلی</param>
public sealed record TeachingItemModel(string Title, string School, string EducationYear);

/// <summary>
/// مدل آیتم تبلیغ
/// </summary>
/// <param name="Kind">نوع تبلیغ</param>
/// <param name="City">شهر</param>
/// <param name="StartDate">تاریخ شروع</param>
/// <param name="EndDate">تاریخ پایان</param>
public sealed record PropagationItemModel(PreachKind? Kind, string City, string StartDate, string EndDate);

/// <summary>
/// مدل آیتم پژوهش
/// </summary>
/// <param name="Title">عنوان پژوهش</param>
/// <param name="Type">نوع پژوهش</param>
/// <param name="Year">سال انجام</param>
public sealed record ResearchItemModel(string Title, ResearchType? Type, short? Year);

/// <summary>
/// مدل آیتم تحصیلات حوزوی
/// </summary>
/// <param name="EducationStatus">وضعیت تحصیلی</param>
/// <param name="ApprovalCenter">مرکز تایید</param>
/// <param name="EnteringYear">سال ورود</param>
public sealed record SeminaryEducationItemModel(EducationStatus? EducationStatus, ApprovalCenter? ApprovalCenter, int? EnteringYear);

/// <summary>
/// مدل سطح نخبگی
/// </summary>
/// <param name="Id">شناسه سطح</param>
/// <param name="Title">عنوان سطح</param>
public sealed record EliteLevelModel(short? Id, string Title);

/// <summary>
/// مدل سطح رتبه (تدریس، تبلیغ، پژوهش)
/// </summary>
/// <param name="Grade">رتبه</param>
/// <param name="Id">شناسه مرکز تایید</param>
public sealed record GradeLevelModel(int? Id, short? Grade);



/// <summary>
/// اطلاعات پایه طلبه برای سامانه مسکن
/// </summary>
public sealed record HousingBasicInfoResult
(
    /// <summary>
    /// کدملی
    /// </summary>
    string NationalCode,

/// <summary>
/// تلفن همراه
/// </summary>
string Mobile,

/// <summary>
/// (وضعیت فعال بودن (کد و نام
/// </summary>
bool IsActive,

/// <summary>
/// وضعیت تجرد
/// </summary>  
SingleStatus? SingleStatus,

/// <summary>
/// (شعبه (کد و نام
/// </summary>
short? BranchId,

/// <summary>
/// شعبه
/// </summary>
string Branch,

/// <summary>
/// (نمایندگی (کد و نام
/// </summary>
short? AgencyId,


/// <summary>
/// نمایندگی
/// </summary>
string Agency,

/// <summary>
/// تراز تحصیلی
/// </summary>
float? Taraz,

/// <summary>
/// (وضعیت اشتغال (کد و نام
/// </summary>
bool EmploymentStatus,

/// <summary>آیا فرد دارای درآمد است؟</summary>
bool? HasIncome,

/// <summary>
/// درامد مکفی است؟
/// </summary>
bool IsSufficientIncome,

/// <summary>
/// (وضعیت حیات (کد و نام
/// </summary>
bool LifeStatus,

/// <summary>
/// (مذهب (کد و نام
/// </summary>
Religion? Religion,

/// <summary>
/// (جنسیت (کد و نام
/// </summary>
Gender Gender,

/// <summary>
/// (استان صادره (کد و نام
/// </summary>
short? IssueProvince,

/// <summary>
/// (تلبس (کد و نام
/// </summary>
bool IsMolabbas,

/// <summary>
/// (استان (کد و نام
/// </summary>
short? ProvinceId,

/// <summary>
/// استان
/// </summary>
string Province,

/// <summary>
/// (شهرستان (کد و نام
/// </summary>
short? CityId,

/// <summary>
/// شهرستان
/// </summary>
string City,

/// <summary>
/// تعداد تدریس در سال تحصیلی جاری
/// </summary>
int TeachingCountCurrentAcademicYear,

/// <summary>
/// (ممتاز در سال تحصیلی جاری (کد و نام
/// </summary>
bool IsExcellentInCurrentAcademicYear,

/// <summary>
/// تعداد جزء قرآن حفظ، نهج البلاغه تعداد خطبه
/// </summary>
int QuranHifzCount,

/// <summary>
/// (سرپرست خانوار (برای خانم ها) (کد و نام
/// </summary>
bool IsHeadOfHousehold,

/// <summary>
/// امتیاز هدف مندی کل
/// </summary>
float TotalTargetingScore,

/// <summary>
/// تعداد افراد تحت تکفل
/// </summary>
int NumberOfSpouses,

/// <summary>
/// تعداد فرزندان تحت تکفل
/// </summary>
int NumberOfDependents,

/// <summary>
/// (تابعیت (کد و نام
/// </summary>
Nationality? Nationality,

/// <summary>
/// (وضعیت تاهل (کد و نام
/// </summary>
bool IsMarried
);

/// <summary>
/// اطلاعات مسکن و اعتراضات برای سامانه مسکن
/// </summary>
public sealed record HousingStatusInfoResult(

    /// <summary>
    /// وضعیت مسکن 
    /// </summary>
    HouseStatus? HousingStatus,

/// <summary>
/// فرم جیم 
/// </summary>
ProtestFormTitle? FormJ,

/// <summary>
/// ثبت اسناد 
/// </summary>
ProtestFormTitle? DocumentRegistration,

/// <summary>
/// بانک مسکن 
/// </summary>
ProtestFormTitle? HousingBank,

/// <summary>
/// وضعیت تایید اعتراض بانک مسکن 
/// </summary>
ProtestFormTitle? HousingBankObjectionStatus,

/// <summary>
/// وضعیت اجاره ملک 
/// </summary>
ProtestFormTitle? PropertyRentStatus,

/// <summary>
/// وضعیت اعتراض اجاره 
/// </summary>
ProtestFormTitle? RentObjectionStatus,

/// <summary>
/// وضعیت خرید و فروش 
/// </summary>
ProtestFormTitle? BuyAndSellStatus,

/// <summary>
/// وضعیت لاگ صاحب منزل در پذیرش 
/// </summary>
ProtestFormTitle? HomeOwnerLogStatus,

/// <summary>
/// وضعیت احراز لاگ صاحب منزل پذیرش 
/// </summary>
ProtestFormTitle? HomeOwnerLogVerificationStatus,

/// <summary>
/// سوال پذیرشی دارای منزل هستید 
/// </summary>
bool HasHouse,

/// <summary>
/// وضعیت سابقه مسکن 
/// </summary>
bool HasHousingHistoryStatus
);

#endregion


public sealed record BranchResult(short Id, string Title, int ProvinceId);



/// <summary>
/// تکفل های طلبه
/// </summary>
public sealed record StudentDependentResult(

     /// <summary>
     /// کد مرکز سرپرست
     /// </summary>
     int Codm,

     /// <summary>
     /// شناسه تکفل
     /// </summary>
     long? DependentId,

     /// <summary>
     /// نام
     /// </summary>
     string FirstName,

     /// <summary>
     /// نام خانوادگی
     /// </summary>
     string LastName,

     /// <summary>
     /// وضعیت تأهل
     /// </summary>
     bool IsMarried,

     /// <summary>
     /// تاریخ ازدواج
     /// </summary>
     string? MarriageDate,

     /// <summary>
     /// فعال بودن پرونده
     /// </summary>
     bool IsActive,

     /// <summary>
     /// وضعیت فوت
     /// </summary>
     bool IsDead
    );

/// <summary>
/// اطلاعات طلبه + تکفل ها
/// </summary>
public sealed record StudentWithDependentsResult(

     /// <summary>
     /// اطلاعات طلبه
     /// </summary>
     StudentDependentResult Student,
     /// <summary>
     /// لیست تکفل ها
     /// </summary>
     List<StudentDependentResult> Dependents
);


/// <summary>
/// اطلاعات طلبه براي جهيزيه
/// </summary>
public sealed record InfoForDowryServiceResult(

     /// <summary>
     /// كد مرکز طلبه
     /// </summary>
     int Codm,

     /// <summary>
     /// شناسه فرد تحت تکفل در صورت وجود)
     /// </summary>
     long? DependentId,
     /// <summary>
     /// نام
     /// </summary>
     string FirstName,
     /// <summary>
     /// نام خانوادگی
     /// </summary>
     string LastName,
     /// <summary>
     /// دهک
     /// </summary>
     short? Decile,
     /// <summary>
     /// سرپرست خانوار است؟
     /// </summary>
     bool IsHeadOfHousehold,
     /// <summary>
     /// متاهل است؟
     /// </summary>
     bool IsMarried,
     /// <summary>
     /// تاریخ ازدواج
     /// </summary>
     string? MarriageDate,
     /// <summary>
     /// آیا در منطقه اقلیت شیعه زندگی می کند؟
     /// </summary>
     bool IsLivingInShiaMinorityArea,
     /// <summary>
     /// آیا در منطقه فقیر زندگی می کند؟
     /// </summary>
     /// <remarks>
     /// این فیلد نشان می دهد که آیا فرد در منطقه ای زندگی می کند که به عنوان منطقه فقیر شناخته می شود یا خیر.
     /// </remarks>
     bool? IsLivingInPoorArea
);

/// <summary>
/// دریافت لیست تکفل ها + خود طلبه برای استفاده در نرم افزار جهیزیه
/// </summary>
/// <param name="Student"></param>
/// <param name="Dependents"></param>
public sealed record StudentWithDependentInfoForDowryServiceResult(InfoForDowryServiceResult Student, List<InfoForDowryServiceResult> Dependents);

/// <summary>
/// اجاره نامه فعال
/// </summary>
public sealed record ActiveTenantRentContractResult(

     /// <summary>
     /// Codm
     /// </summary>
     int Codm,

     /// <summary>
     /// مبلغ رهن ریال
     /// </summary>
     long? MortgageAmount,

     /// <summary>
     /// مبلغ اجاره ریال
     /// </summary>
     long? RentAmount,

     /// <summary>
     /// تاریخ شروع قرارداد
     /// </summary>
     string? StartDate,

     /// <summary>
     /// تاریخ پایان قرارداد
     /// </summary>
     string? EndDate,

     /// <summary>
     /// وضعیت اجاره فعال / غیرفعال)
     /// </summary>
     bool IsActive
);

/// <summary>
/// دریافت کدمرکز طلبه با کد ملی
/// </summary>
public sealed record GetStudentCodmByNationalCodeResult
(
     /// <summary>
     /// کد ملی
     /// </summary>
     string NationalCode,
     /// <summary>
     /// کد مرکز طلبه
     /// </summary>
     int Codm,

     /// <summary>
     /// شناسه همسر
     /// </summary>
     GetStudentDependentByNationalCodeResult? Dependent
);

/// <summary>
/// دریافت اطلاعات همسر با کد ملی
/// </summary>
public sealed record GetStudentDependentByNationalCodeResult(
     /// <summary>
     /// کد ملی
     /// </summary>
     string NationalCode,
     /// <summary>
     /// شناسه همسر
     /// </summary>
     long? DependentId
);
