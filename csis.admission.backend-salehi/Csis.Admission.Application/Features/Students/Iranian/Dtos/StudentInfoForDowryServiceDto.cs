namespace Csis.Admission.Application.Features.Students.Dtos;

/// <summary>
/// اطلاعات طلبه براي جهيزيه
/// </summary>
public sealed record InfoForDowryServiceDto
{
    /// <summary>
    /// كد مرکز طلبه
    /// </summary>
    public int Codm { get; init; }

    /// <summary>
    /// شناسه فرد تحت تکفل (در صورت وجود)
    /// </summary>
    public long? DependentId { get; init; }
    /// <summary>
    /// نام
    /// </summary>
    public string FirstName { get; init; }
    /// <summary>
    /// نام خانوادگی
    /// </summary>
    public string LastName { get; init; }
    /// <summary>
    /// دهک
    /// </summary>
    public short? Decile { get; init; }
    /// <summary>
    /// سرپرست خانوار است؟
    /// </summary>
    public bool IsHeadOfHousehold { get; init; }
    /// <summary>
    /// متاهل است؟
    /// </summary>
    public bool IsMarried { get; init; }
    /// <summary>
    /// تاریخ ازدواج
    /// </summary>
    public string? MarriageDate { get; init; }
    /// <summary>
    /// زندگی در مناطق اقلیت شیعه
    /// </summary>
    public bool IsLivingInShiaMinorityArea { get; init; }

    /// <summary>
    /// زندگی در مناطق  محروم
    /// </summary>
    public bool? IsLivingInPoorArea { get; init; }
}

/// <summary>
/// دریافت لیست تکفل ها + خود طلبه برای استفاده در نرم افزار جهیزیه
/// </summary>
/// <param name="Student"></param>
/// <param name="Dependents"></param>
public sealed record StudentWithDependentInfoForDowryServiceDto(InfoForDowryServiceDto Student, List<InfoForDowryServiceDto> Dependents);
