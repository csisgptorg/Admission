using Csis.Utilities;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Common.Models;

/// <inheritdoc/>
public record GetIdentityInfoByYektaCodeRequest(string YektaCode);

/// <inheritdoc/>
public class GetIdentityInfoByYektaCodeResponse
{
    /// <summary>دریافت اطلاعات شناسنامه ای</summary>
    public NonIranianBirthCertInfo BirthCertInfo() {
        var gender = (Gender) int.Parse(SexId);
        var gregorianBirthDate =BirthDatePersianDate.IntDateToGregorianStingDate();
        var age = Utilities.CalculateAge(BirthDatePersianDate, DeathDatePersianDate);
        return new NonIranianBirthCertInfo(YektaCode, FirstName, LastName,FatherName, BirthDatePersianDate.IntDateToString(),
            gregorianBirthDate.ToString(),age.Value, (Nationality)short.Parse(NationalityId), gender, DeathDate.HasValue(), DeathDate);
    }

    /// <summary>نام</summary>
    public string FirstName { get; set; }

    /// <summary>نام خانوادگی</summary>
    public string LastName { get; set; }

    /// <summary>نام پدر</summary>
    public string FatherName { get; set; }

    /// <summary>شناسه جنسیت</summary>
    public string SexId { get; set; }

    /// <summary>جنسیت</summary>
    public string Sex { get; set; }

    /// <summary>شناسه ملیت</summary>
    public string NationalityId { get; set; }

    /// <summary>ملیت</summary>
    public string Nationality { get; set; }

    /// <summary>تاریخ تولد (میلادی)</summary>
    public string BirthDate { get; set; }

    /// <summary>تاریخ تولد (شمسی، به‌صورت عددی)</summary>
    public int BirthDatePersianDate {
        get {
            if ( !string.IsNullOrWhiteSpace(BirthDate) ) {

                var isPersianDate = BirthDate.StartsWith("13") || BirthDate.StartsWith("14");
                if ( isPersianDate ) {
                    return BirthDate.StringDateToInt().Value;
                }
            }

            var date = DateTime.Parse(BirthDate);
            return date.ToPersianDateTime().ToString().StringDateToInt().Value;
        }
    }

    /// <summary>تاریخ فوت</summary>
    public string DeathDate { get; set; }

    /// <summary>تاریخ تولد (شمسی، به‌صورت عددی)</summary>
    public int? DeathDatePersianDate {
        get {

            if ( !string.IsNullOrWhiteSpace(DeathDate) ) {

                if ( PersianDateTime.IsValid(DeathDate) ) {
                    return DeathDate.StringDateToInt().Value;
                }

                var date = DateTime.Parse(DeathDate);
                return date.ToPersianDateTime().ToString().StringDateToInt().Value;
            }

            return null;
        }
    }

    /// <summary>شناسه وضعیت تأهل</summary>
    public string MarriageStatusId { get; set; }

    /// <summary>وضعیت تأهل</summary>
    public string MarriageStatus { get; set; }

    /// <summary>تاریخ ازدواج</summary>
    public string MarriageDate { get; set; }

    /// <summary>کد ناجا</summary>
    public string NajaBId { get; set; }

    /// <summary>کد فیدا</summary>
    public string FidaCode { get; set; }

    /// <summary>کد یکتا</summary>
    public string YektaCode { get; set; }
}

/// <summary>اطلاعات شناسنامه غیر ایرانی</summary>
public record NonIranianBirthCertInfo(string YektaCode, string FirstName, string LastName,string FatherName, 
    string BirthDate, string GregorianBirthDate,int Age, Nationality Nationality, Gender Gender, bool IsDead, string DeathDate);
