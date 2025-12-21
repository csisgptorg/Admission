namespace Csis.Admission.Application.Features.HousingAdmissionInfo.Dtos;

/// <summary>
/// اطلاعات فعالیت‌های علمی و فرهنگی برای سامانه مسکن
/// </summary>
public sealed class HousingActivitiesInfoDto
{
    /// <summary>
    /// لیست نخبگی
    /// </summary>
    public List<EliteItemModel> EliteList { get; set; } = [];

    /// <summary>
    /// سطح نخبگی
    /// </summary>
    public EliteLevelModel EliteLevel { get; set; }

    /// <summary>
    /// لیست تدریس
    /// </summary>
    public List<TeachingItemModel> TeachingList { get; set; } = [];

    /// <summary>
    /// سطح تدریس
    /// </summary>
    public GradeLevelModel TeachingLevel { get; set; }

    /// <summary>
    /// لیست تبلیغ
    /// </summary>
    public List<PropagationItemModel> PropagationList { get; set; } = [];

    /// <summary>
    /// سطح تبلیغ
    /// </summary>
    public GradeLevelModel PropagationLevel { get; set; }

    /// <summary>
    /// لیست پژوهش
    /// </summary>
    public List<ResearchItemModel> ResearchList { get; set; } = [];

    /// <summary>
    /// سطح پژوهش
    /// </summary>
    public GradeLevelModel ResearchLevel { get; set; }

    /// <summary>
    /// تحصیلات حوزوی
    /// </summary>
    public List<SeminaryEducationItemModel> SeminaryEducationList { get; set; } = [];
}

/// <summary>
/// مدل آیتم نخبگی
/// </summary>
/// <param name="Id">شناسه نخبگی</param>
/// <param name="Title">عنوان نخبگی</param>
/// <param name="StartDate">تاریخ شروع</param>
/// <param name="EndDate">تاریخ پایان</param>
public sealed record EliteItemModel(short? Id, string Title, string StartDate, string EndDate);

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
