using System.ComponentModel.DataAnnotations;

namespace Csis.Admission.Application.Common.Models;

/// <inheritdoc/>
public record CsisSupInsuranceGetStatusResponse(string Codm, int? TakafolId, string Status, string PlanTitle);

/// <summary>
/// مدل نمایشی وضعیت پرونده جاری
/// </summary>
/// <param name="Codm">کد مرکز</param>
/// <param name="TakafolId">شناسه تکفل</param>
/// <param name="Status">وضعیت بیمه</param>
/// <param name="StatusTitle">عنوان وضعیت بیمه</param>
/// <param name="CaseNumber">شماره دفترچه</param>
/// <param name="PlanTitle">طرح بیمه</param>
public sealed record CurrentSupInsuranceCaseStateDto(string Codm, int? TakafolId, RegistrationStatus? Status, string StatusTitle, string CaseNumber, string PlanTitle);


/// <summary>
/// وضعیت ثبت نام
/// </summary>
public enum RegistrationStatus
{
    /// <summary>
    /// پیش ثبت نام
    /// </summary>
    [Display(Name = "پیش ثبت نام")]
    PreRegistered = 0,

    /// <summary>
    /// فعال تایید شده - فایل تاییدیه از شرکت بیمه دریافت شده است
    /// </summary>
    [Display(Name = "فعال تایید شده")]
    ActiveConfirmed = 1,

    /// <summary>
    /// فعال تایید نشده - فایل به شرکت بیمه ارسال شده اما هنوز تایید نشده
    /// </summary>
    [Display(Name = "فعال تایید نشده")]
    ActiveNotConfirmed = 2,

    /// <summary>
    /// غیرفعال تایید شده - اتمام یا انصراف توسط شرکت بیمه نهایی شده است
    /// </summary>
    [Display(Name = "غیرفعال تایید شده")]
    DeActiveConfirmed = 3,

    /// <summary>
    /// غیرفعال تایید نشده - اتمام یا انصراف به شرکت بیمه اعلام شده اما هنوز به تایید نرسیده است
    /// </summary>
    [Display(Name = "غیرفعال تایید نشده")]
    DeActiveNotConfirmed = 4,

    /// <summary>
    /// انصراف توسط کاربر - کاربر از بیمه تا پایان قرارداد جاری اعلام انصراف نموده است
    /// </summary>
    [Display(Name = "انصراف توسط کاربر")]
    CancelledByUser = 5
}
