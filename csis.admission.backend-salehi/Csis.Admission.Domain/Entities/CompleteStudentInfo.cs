using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;
using Csis.Utilities.Extensions;

namespace Csis.Admission.Domain.Entities;

/// <summary></summary>
public class CompleteStudentInfo : BaseEntity
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }
    /// <summary>نام</summary>
    public string FirstName { get; set; }
    /// <summary>نام خانوادگی</summary>
    public string LastName { get; set; }
    /// <summary>کد ملی</summary>
    public string NationalCode { get; set; }
    /// <summary>مذهب</summary>
    public string Religion { get; set; }
    /// <summary>تابعیت</summary>
    public string Citizenship { get; set; }
    /// <summary>فعال/غیر فعال</summary>
    public string ActiveTitle { get; set; }
    /// <summary>شاغل</summary>
    public bool? IsEmployee { get; set; }
    /// <summary>متاهل</summary>
    public bool? IsMarried { get; set; }
    /// <summary>تعداد تکفل</summary>
    public int? DependentCount { get; set; }
    /// <summary>امتیاز کلی هدفمندی</summary>
    public float? TotalScore { get; set; }
    /// <summary>امتیاز کلی هدفمندی معیشتی</summary>
    public float? LivelihoodTotalScore { get; set; }
    /// <summary>شناسه شعبه</summary>
    public short? BranchId { get; set; }
    /// <summary>شعبه</summary>
    public string Branch { get; set; }
    /// <summary>محل صدور شناسنامه</summary>
    public string BirthCertIssuePlace { get; set; }
    /// <summary>تاریخ تولد</summary>
    public int? BirthDate { get; set; }
    /// <summary>مرحوم</summary>
    public bool IsDead { get; set; }
    /// <summary>تاریخ فوت</summary>
    public int? DeathDate { get; set; }
    /// <summary>نخبه</summary>
    public bool? IsElite { get; set; }
    /// <summary>مشهور</summary>
    public bool? IsFamous { get; set; }
    /// <summary>استاد</summary>
    public bool? IsTeacher { get; set; }
    /// <summary>امام جمعه</summary>
    public bool? EmamJomee { get; set; }
    /// <summary>مبلغ</summary>
    public bool? IsPreacher { get; set; }
    /// <summary>دهک</summary>
    public short? Dahak { get; set; }
    /// <summary>وضعیت نخبگی</summary>
    public string EliteStatus { get; set; }
    /// <summary>تراز</summary>
    public int? Taraz { get; set; }
    /// <summary>سال ورود</summary>
    public int? EnteringYear { get; set; }
    /// <summary>وضعیت تحصیل</summary>
    public string EducationStatus { get; set; }
    /// <summary>وضعیت مسکن</summary>
    public string HouseStatus { get; set; }

    /// <summary> تعداد جزِ حفظ قرآن </summary>
    public int? QoraanPart { get; set; }

    /// <summary>  تعداد سال تدریس </summary>
    public short? TeachYearCount { get; set; }
    /// <summary> سابقه تبلیغ </summary>
    public short? PreachYearCount { get; set; }

    /// <summary>تاریخ آخرین فعالیت تبلیغی </summary>
    public int? PreachLastAcitvity { get; set; }

    /// <summary>امتیاز پژوهشی</summary>
    public short? ResearchGrade { get; set; }
    /// <summary>امتیاز ایثارگری</summary>
    public float? VeteranScore { get; set; }
}
