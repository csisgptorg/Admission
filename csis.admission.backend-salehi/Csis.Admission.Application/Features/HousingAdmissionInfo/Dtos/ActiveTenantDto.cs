namespace Csis.Admission.Application.Features.HousingAdmissionInfo.Dtos;

/// <summary>
/// اجاره نامه فعال
/// </summary>
public sealed record ActiveTenantDto
{
    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; init; }

    /// <summary>
    /// مبلغ رهن ریال
    /// </summary>
    public long? MortgageAmount { get; init; }

    /// <summary>
    /// مبلغ اجاره ریال
    /// </summary>
    public long? RentAmount { get; init; }

    /// <summary>
    /// تاریخ شروع قرارداد
    /// </summary>
    public string? StartDate { get; init; }

    /// <summary>
    /// تاریخ پایان قرارداد
    /// </summary>
    public string? EndDate { get; init; }

    /// <summary>
    /// وضعیت اجاره (فعال / غیرفعال)
    /// </summary>
    public bool IsActive { get; init; }
}
