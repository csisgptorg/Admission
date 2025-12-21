namespace Csis.Admission.Application.Common.Models;

/// <summary>
/// مدل نمایشی وضعیت پرونده جاری
/// </summary>
/// <param name="Codm">کد مرکز</param>
/// <param name="TakafolId">شناسه تکفل</param>
/// <param name="Status">وضعیت بیمه</param>
/// <param name="StatusTitle">عنوان وضعیت بیمه</param>
/// <param name="CaseNumber">شماره دفترچه</param>
/// <param name="PlanTitle">طرح بیمه</param>
public sealed record CurrentHealthInsuranceCaseStateDto(string Codm, int? TakafolId, RegistrationStatus? Status, string StatusTitle, string CaseNumber, string PlanTitle);
