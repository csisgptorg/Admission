namespace Csis.Admission.Application.Features.Students.Dtos;

/// <summary>
/// کارت الکترونیکی طلبه
/// </summary>
public record StudentECardDto
{
    /// <summary>
    /// کد مرکز
    /// </summary>
    public int Codm { get; init; }

    /// <summary>
    /// نام
    /// </summary>
    public string FirstName { get; init; }
    
    /// <summary>
    /// وضعیت پرونده
    /// </summary>
    public bool IsBlock { get; init; }

    /// <summary>
    /// نام خانوادگی
    /// </summary>
    public string LastName { get; init; }

    /// <summary>
    /// کد ملی
    /// </summary>
    public string NationalCode { get; init; }

    /// <summary>
    /// پایه تحصیلی
    /// </summary>
    public int Grade { get; init; }

    /// <summary>
    /// تاریخ اعتبار پرونده
    /// </summary>
    public string? CaseValidityDate { get; init; }

    /// <summary>
    /// آیا استاد است؟
    /// </summary>
    public bool IsTeacher { get; init; } 

    /// <summary>
    /// آیا مبلغ است؟
    /// </summary>
    public bool IsPreacher { get; init; }

    /// <summary>
    /// آیا پژوهشگر است؟
    /// </summary>
    public bool IsResearcher { get; init; }
}
