using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Domain.Entities;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Application.Features.People.Dtos;

/// <summary>
/// مدل نمایشی خلاصه موجودیت شخص
/// </summary>
public sealed record PersonAbstractDto : BaseDto<PersonAbstractDto, Person>
{
    /// <summary>
    /// نام پدر
    /// </summary>
    public string FatherName { get; init; }

    /// <summary>
    /// شناسه فیدا
    /// </summary>
    public string FidaCode { get; init; }

    /// <summary>
    /// نام
    /// </summary>
    public string FirstName { get; init; }

    /// <summary>
    /// نام خانوادگی
    /// </summary>
    public string LastName { get; init; }

    /// <summary>
    /// تلفن همراه
    /// </summary>
    public string Mobile { get; init; }

    /// <summary>
    /// کد ملی
    /// </summary>
    public string NationalCode { get; init; }

    /// <summary>
    /// نام مستعار
    /// </summary>
    public string NickName { get; init; }

    /// <summary>
    /// شناسه یکتا
    /// </summary>
    public string YektaCode { get; init; }

    /// <summary>
    /// ملیت
    /// </summary>
    public short Nationality { get; init; }

    /// <summary>
    /// تاریخ تولد
    /// </summary>
    public int BirthDate { get; init; }

    /// <summary>
    /// مرحوم است
    /// </summary>
    public bool IsDead { get; init; }

    /// <summary>
    /// عائله مند است
    /// </summary>
    public bool IsHouseholder { get; init; }

    /// <summary>
    /// متاهل است
    /// </summary>
    public bool IsMarried { get; init; }

    /// <summary>
    /// سیادت
    /// </summary>
    public bool IsSadat { get; init; }

    /// <summary>
    /// تاریخ فوت
    /// </summary>
    public DateTime? DeathDate { get; init; }

    /// <summary>
    /// جنسیت
    /// </summary>
    public Gender Gender { get; init; }

    /// <summary>
    /// مذهب
    /// </summary>
    public Religion Religion { get; init; }

    /// <summary>
    /// وضعیت تجرد
    /// </summary>
    public SingleStatus? SingleStatus { get; init; }

    /// <summary>
    /// تابعیت
    /// </summary>
    public Citizenship Citizenship { get; init; }

    /// <summary>
    /// نوع ایجاد شخص
    /// </summary>
    public PersonCreateType? CreateType { get; init; }
}
