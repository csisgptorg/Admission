using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// StudentInfo
/// </summary>
public class StudentInfo : BaseEntity
{
    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool? IsSadat { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string FatherName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int? BirthDate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Gender? Gender { get; set; }

    /// <summary>
    /// Religion
    /// </summary>
    public Religion? Religion { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Citizenship? Citizenship { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string NationalCode { get; set; }

    /// <summary>
    /// شماره شناسنامه
    /// </summary>
    public string BirthCertNumber { get; set; }

    /// <summary>
    /// سری شناسنامه
    /// </summary>
    public string BirthCertSeri { get; set; }

    /// <summary>
    /// سریال شناسنامه
    /// </summary>
    public int? BirthCertSerial { get; set; }

    /// <summary>
    /// محل صدور شناسنامه
    /// </summary>
    public string BirthCertIssuePlace { get; set; }

    /// <summary>
    /// استان محل صدور شناسنامه
    /// </summary>
    public string BirthCertIssueProvince { get; set; }

    /// <summary>
    /// توضیحات شناسنامه
    /// </summary>
    public string BirthCertDescription { get; set; }

    /// <summary>
    /// ملیت
    /// </summary>
    public short? Nationality { get; set; }

    /// <summary>
    /// ملیت
    /// </summary>
    public string NationalityTitle { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string PassportNumber { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string FidaCode { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string YektaCode { get; set; }

    /// <summary>
    /// تاریخ اعتبار اقامت
    /// </summary>
    public int? ResidenceExpireDate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool IsMarried { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int? MarriageDate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int? DivorceDate { get; set; }

    /// <summary>
    /// وضعیت تجرد
    /// </summary>
    public SingleStatus? SingleStatus { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool IsDead { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int? DeathDate { get; set; }
}
