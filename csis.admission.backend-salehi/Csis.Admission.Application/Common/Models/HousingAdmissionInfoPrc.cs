namespace Csis.Admission.Application.Common.Models;

/// <summary>
/// اطلاعات پذیرش برای سامانه مسکن
/// </summary>
public class HousingAdmissionInfoPrc
{
    /// <summary>
    /// کدملی
    /// </summary>
    public string NationalCode { get; set; }
    
    /// <summary>
    /// تلفن همراه
    /// </summary>
    public string Mobile { get; set; }
    
    /// <summary>
    /// فعال و مسدود
    /// </summary>
    public bool IsActive { get; set; }
    
    /// <summary>
    /// کد شعبه
    /// </summary>
    public string BranchCode { get; set; }
    
    /// <summary>
    /// نام شعبه
    /// </summary>
    public string BranchName { get; set; }
    
    /// <summary>
    /// کد نمایندگی
    /// </summary>
    public string AgencyCode { get; set; }
    
    /// <summary>
    /// نام نمایندگی
    /// </summary>
    public string AgencyName { get; set; }
    
    /// <summary>
    /// تراز تحصیلی
    /// </summary>
    public float EducationalBalance { get; set; }
    
    /// <summary>
    /// کد وضعیت اشتغال
    /// </summary>
    public string EmploymentStatusCode { get; set; }
    
    /// <summary>
    /// نام وضعیت اشتغال
    /// </summary>
    public string EmploymentStatusName { get; set; }
    
    /// <summary>
    /// کد وضعیت حیات
    /// </summary>
    public string LifeStatusCode { get; set; }
    
    /// <summary>
    /// نام وضعیت حیات
    /// </summary>
    public string LifeStatusName { get; set; }
    
    /// <summary>
    /// کد مذهب
    /// </summary>
    public string ReligionCode { get; set; }
    
    /// <summary>
    /// نام مذهب
    /// </summary>
    public string ReligionName { get; set; }
    
    /// <summary>
    /// کد جنسیت
    /// </summary>
    public string GenderCode { get; set; }
    
    /// <summary>
    /// نام جنسیت
    /// </summary>
    public string GenderName { get; set; }
    
    /// <summary>
    /// کد استان صادره
    /// </summary>
    public string IssueProvinceCode { get; set; }
    
    /// <summary>
    /// نام استان صادره
    /// </summary>
    public string IssueProvinceName { get; set; }
    
    /// <summary>
    /// کد تلبس
    /// </summary>
    public string TalabbosCode { get; set; }
    
    /// <summary>
    /// نام تلبس
    /// </summary>
    public string TalabbosName { get; set; }
    
    /// <summary>
    /// کد استان آدرس
    /// </summary>
    public string AddressProvinceCode { get; set; }
    
    /// <summary>
    /// نام استان آدرس
    /// </summary>
    public string AddressProvinceName { get; set; }
    
    /// <summary>
    /// تعداد تدریس در سال تحصیلی جاری
    /// </summary>
    public int TeachingCountCurrentAcademicYear { get; set; }
    
    /// <summary>
    /// ممتاز در سال تحصیلی جاری
    /// </summary>
    public bool IsExcellentInCurrentAcademicYear { get; set; }
    
    /// <summary>
    /// تعداد جزء قرآن حفظ، نهج البلاغه تعداد خطبه
    /// </summary>
    public int QuranHifzCount { get; set; }
    
    /// <summary>
    /// سرپرست خانوار (برای خانم ها)
    /// </summary>
    public bool? IsHeadOfHousehold { get; set; }
    
    /// <summary>
    /// امتیاز هدف مندی کل
    /// </summary>
    public float TotalTargetingScore { get; set; }
    
    /// <summary>
    /// تعداد افراد تحت تکفل
    /// </summary>
    public int NumberOfSpouses { get; set; }
    
    /// <summary>
    /// تعداد فرزندان تحت تکفل
    /// </summary>
    public int NumberOfDependents { get; set; } 
    
    /// <summary>
    /// کد تابعیت
    /// </summary>
    public string NationalityCode { get; set; }
    
    /// <summary>
    /// نام تابعیت
    /// </summary>
    public string NationalityName { get; set; }
    
    /// <summary>
    /// کد وضعیت مسکن
    /// </summary>
    public string HousingStatusCode { get; set; }
    
    /// <summary>
    /// نام وضعیت مسکن
    /// </summary>
    public string HousingStatusName { get; set; }
    
    /// <summary>
    /// کد فرم جیم
    /// </summary>
    public string FormJCode { get; set; }
    
    /// <summary>
    /// نام فرم جیم
    /// </summary>
    public string FormJName { get; set; }
    
    /// <summary>
    /// کد ثبت اسناد
    /// </summary>
    public string DocumentRegistrationCode { get; set; }
    
    /// <summary>
    /// نام ثبت اسناد
    /// </summary>
    public string DocumentRegistrationName { get; set; }
    
    /// <summary>
    /// کد بانک مسکن
    /// </summary>
    public string HousingBankCode { get; set; }
    
    /// <summary>
    /// نام بانک مسکن
    /// </summary>
    public string HousingBankName { get; set; }
    
    /// <summary>
    /// کد وضعیت تایید اعتراض بانک مسکن
    /// </summary>
    public string HousingBankObjectionStatusCode { get; set; }
    
    /// <summary>
    /// نام وضعیت تایید اعتراض بانک مسکن
    /// </summary>
    public string HousingBankObjectionStatusName { get; set; }
    
    /// <summary>
    /// کد وضعیت اجاره ملک
    /// </summary>
    public string PropertyRentStatusCode { get; set; }
    
    /// <summary>
    /// نام وضعیت اجاره ملک
    /// </summary>
    public string PropertyRentStatusName { get; set; }
    
    /// <summary>
    /// کد وضعیت اعتراض اجاره
    /// </summary>
    public string RentObjectionStatusCode { get; set; }
    
    /// <summary>
    /// نام وضعیت اعتراض اجاره
    /// </summary>
    public string RentObjectionStatusName { get; set; }
    
    /// <summary>
    /// کد وضعیت خرید و فروش
    /// </summary>
    public string BuyAndSellStatusCode { get; set; }
    
    /// <summary>
    /// نام وضعیت خرید و فروش
    /// </summary>
    public string BuyAndSellStatusName { get; set; }
    
    /// <summary>
    /// کد وضعیت لاگ صاحب منزل در پذیرش
    /// </summary>
    public string HomeOwnerLogStatusCode { get; set; }
    
    /// <summary>
    /// نام وضعیت لاگ صاحب منزل در پذیرش
    /// </summary>
    public string HomeOwnerLogStatusName { get; set; }
    
    /// <summary>
    /// کد وضعیت احراز لاگ صاحب منزل پذیرش
    /// </summary>
    public string HomeOwnerLogVerificationStatusCode { get; set; }
    
    /// <summary>
    /// نام وضعیت احراز لاگ صاحب منزل پذیرش
    /// </summary>
    public string HomeOwnerLogVerificationStatusName { get; set; }
    
    /// <summary>
    /// کد سوال پذیرشی دارای منزل هستید
    /// </summary>
    public string HasHouseCode { get; set; }
    
    /// <summary>
    /// نام سوال پذیرشی دارای منزل هستید
    /// </summary>
    public string HasHouseName { get; set; }
    
    /// <summary>
    /// کد سوال پذیرش نداشتن سابقه مسکن
    /// </summary>
    public string NoHousingHistoryCode { get; set; }
    
    /// <summary>
    /// نام سوال پذیرش نداشتن سابقه مسکن
    /// </summary>
    public string NoHousingHistoryName { get; set; }
    
    /// <summary>
    /// کد وضعیت سابقه مسکن
    /// </summary>
    public string HousingHistoryStatusCode { get; set; }
    
    /// <summary>
    /// نام وضعیت سابقه مسکن
    /// </summary>
    public string HousingHistoryStatusName { get; set; }
    
    /// <summary>
    /// لیست نخبگی
    /// </summary>
    public List<string> EliteList { get; set; } = [];
    
    /// <summary>
    /// سطح نخبگی
    /// </summary>
    public string EliteLevel { get; set; }
    
    /// <summary>
    /// بازه های نخبگی
    /// </summary>
    public List<string> ElitePeriods { get; set; } = [];
    
    /// <summary>
    /// لیست تدریس
    /// </summary>
    public List<string> TeachingList { get; set; } = [];
    
    /// <summary>
    /// سطح تدریس
    /// </summary>
    public string TeachingLevel { get; set; }
    
    /// <summary>
    /// تاریخ های تدریس
    /// </summary>
    public List<string> TeachingDates { get; set; } = [];
    
    /// <summary>
    /// لیست تبلیغ
    /// </summary>
    public List<string> PropagationList { get; set; } = [];
    
    /// <summary>
    /// سطح تبلیغ
    /// </summary>
    public string PropagationLevel { get; set; }
    
    /// <summary>
    /// تاریخ های تبلیغ
    /// </summary>
    public List<string> PropagationDates { get; set; } = [];
    
    /// <summary>
    /// لیست پژوهش
    /// </summary>
    public List<string> ResearchList { get; set; } = [];
    
    /// <summary>
    /// سطح پژوهش
    /// </summary>
    public string ResearchLevel { get; set; }
    
    /// <summary>
    /// تاریخ های پژوهش
    /// </summary>
    public List<string> ResearchDates { get; set; } = [];
    
    /// <summary>
    /// تحصیلات حوزوی
    /// </summary>
    public List<string> SeminaryEducationList { get; set; } = [];
}
