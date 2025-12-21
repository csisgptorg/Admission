using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// موجودیت شخس
/// </summary>
public sealed class Person : SoftDeletedBaseEntity, IFilterable
{
    /// <summary>
    /// نام
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// سیادت
    /// </summary>
    public bool IsSadat { get; set; }

    /// <summary>
    /// نام خانوادگی
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// نام پدر
    /// </summary>
    public string FatherName { get; set; }

    /// <summary>
    /// نام مادر
    /// </summary>
    public string MotherName { get; set; }

    /// <summary>
    /// تاریخ تولد
    /// </summary>
    public int? BirthDate { get; set; }

    /// <summary>
    /// جنسیت
    /// </summary>
    public PersonEntityGender Gender { get; set; }

    /// <summary>
    /// مذهب
    /// </summary>
    public Religion Religion { get; set; }

    /// <summary>
    /// تابعیت
    /// </summary>
    public Citizenship Citizenship { get; set; }

    /// <summary>
    /// کد ملی
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
    //Nationality table or use tb16(shared api)

    /// <summary>
    /// شماره پاسپورت
    /// </summary>
    public string PassportNumber { get; set; }

    /// <summary>
    /// شناسه فیدا
    /// </summary>
    public string? FidaCode { get; set; }

    /// <summary>
    /// شناسه یکتا
    /// </summary>
    public string YektaCode { get; set; }

    /// <summary>
    /// تاریخ اعتبار اقامت
    /// </summary>
    public int? ResidenceExpireDate { get; set; }

    /// <summary>
    /// مرحوم است
    /// </summary>
    public bool IsDead { get; set; }

    /// <summary>
    /// تاریخ فوت
    /// </summary>
    public int? DeathDate { get; set; }

    /// <summary>
    /// علت فوت
    /// </summary>
    public DeathCause? DeathCause { get; set; }

    /// <summary>
    /// مورد تایید ثبت احوال می باشد
    /// </summary>
    public bool SabteAhvalConfirm { get; set; }

    /// <summary>
    /// تلفن همراه
    /// </summary>
    public string Mobile { get; set; }

    /// <summary>
    /// شماره حساب
    /// </summary>
    public string BankAccountNumber { get; set; }
    //multi?

    /// <summary>
    /// شماره شبا
    /// </summary>
    public string ShebaNumber { get; set; }

    /// <summary>
    /// شناسه مادر
    /// </summary>
    public int? MotherPersonId { get; set; }

    /// <summary>
    /// شناسه پدر
    /// </summary>
    public int? FatherPersonId { get; set; }

    /// <summary>
    /// شناسه تصویر شخص
    /// </summary>
    public Guid? PersonImage { get; set; }

    /// <summary>
    /// کد یکتا ساخته شده توسط سامانه
    /// </summary>
    public int UniqueCode { get; set; }

    /// <summary>
    /// نوع ایجاد شخص
    /// </summary>
    public PersonCreateType? CreateType { get; set; }

    /// <summary>
    /// پدر
    /// </summary>
    public Person FatherPerson { get; private set; }

    /// <summary>
    /// مادر
    /// </summary>
    public Person MotherPerson { get; private set; }

    /// <summary>
    /// ازدواج های خانم
    /// </summary>
    public List<Marriage> MarriageHusbandPeople { get; private set; } = [];

    /// <summary>
    /// ازدواج های آقا
    /// </summary>
    public List<Marriage> MarriageWifePeople { get; private set; } = [];

    /// <summary>
    /// اسناد
    /// </summary>
    public List<RequestDocument> Documents { get; private set; } = [];

    /// <inheritdoc/>
    public string[] GetFilterableFields() {
        return [nameof(NationalCode), nameof(BirthCertNumber), nameof(Mobile), nameof(YektaCode), nameof(UniqueCode)];
    }
}
