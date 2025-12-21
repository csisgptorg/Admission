namespace Csis.Admission.Application.Features.HousingAdmissionInfo.Dtos;

/// <summary>
/// اطلاعات پایه طلبه برای سامانه مسکن
/// </summary>
public sealed class HousingBasicInfoDto
{
    /// <summary>
    /// کدملی
    /// </summary>
    public string NationalCode { get; init; }

    /// <summary>
    /// تلفن همراه
    /// </summary>
    public string Mobile { get; init; }

    /// <summary>
    /// (وضعیت فعال بودن (کد و نام
    /// </summary>
    public bool IsActive { get; init; }

    /// <summary>
    /// وضعیت تجرد
    /// </summary>  
    public SingleStatus? SingleStatus { get; init; }

    /// <summary>
    /// (شعبه (کد و نام
    /// </summary>
    public short? BranchId { get; init; }

    /// <summary>
    /// شعبه
    /// </summary>
    public string Branch { get; init; }

    /// <summary>
    /// (نمایندگی (کد و نام
    /// </summary>
    public short? AgencyId { get; init; }


    /// <summary>
    /// نمایندگی
    /// </summary>
    public string Agency { get; init; }

    /// <summary>
    /// تراز تحصیلی
    /// </summary>
    public float? Taraz { get; init; }

    /// <summary>
    /// (وضعیت اشتغال (کد و نام
    /// </summary>
    public bool EmploymentStatus { get; init; }

    /// <summary>
    /// درامد مکفی است؟
    /// </summary>
    public bool IsSufficientIncome { get; init; }

    /// <summary>
    /// (وضعیت حیات (کد و نام
    /// </summary>
    public bool LifeStatus { get; init; }

    /// <summary>
    /// (مذهب (کد و نام
    /// </summary>
    public Religion? Religion { get; init; }

    /// <summary>
    /// (جنسیت (کد و نام
    /// </summary>
    public Gender Gender { get; init; }

    /// <summary>
    /// (استان صادره (کد و نام
    /// </summary>
    public short? IssueProvince { get; init; }

    /// <summary>
    /// (تلبس (کد و نام
    /// </summary>
    public bool IsMolabbas { get; init; }

    /// <summary>
    /// (استان (کد و نام
    /// </summary>
    public short? ProvinceId { get; init; }


    /// <summary>
    /// استان
    /// </summary>
    public string Province { get; init; }

    /// <summary>
    /// (شهرستان (کد و نام
    /// </summary>
    public short? CityId { get; init; }

    /// <summary>
    /// شهرستان
    /// </summary>
    public string City { get; init; }

    /// <summary>
    /// تعداد تدریس در سال تحصیلی جاری
    /// </summary>
    public int TeachingCountCurrentAcademicYear { get; init; }

    /// <summary>
    /// (ممتاز در سال تحصیلی جاری (کد و نام
    /// </summary>
    public bool IsExcellentInCurrentAcademicYear { get; init; }

    /// <summary>
    /// تعداد جزء قرآن حفظ، نهج البلاغه تعداد خطبه
    /// </summary>
    public int QuranHifzCount { get; init; }

    /// <summary>
    /// (سرپرست خانوار (برای خانم ها) (کد و نام
    /// </summary>
    public bool IsHeadOfHousehold { get; init; }

    /// <summary>
    /// امتیاز هدف مندی کل
    /// </summary>
    public float TotalTargetingScore { get; init; }

    /// <summary>
    /// تعداد افراد تحت تکفل
    /// </summary>
    public int NumberOfSpouses { get; init; }

    /// <summary>
    /// تعداد فرزندان تحت تکفل
    /// </summary>
    public int NumberOfDependents { get; init; }

    /// <summary>
    /// (تابعیت (کد و نام
    /// </summary>
    public Nationality? Nationality { get; init; }

    /// <summary>
    /// (وضعیت تاهل (کد و نام
    /// </summary>
    public bool IsMarried { get; init; }

}
