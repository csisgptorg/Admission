namespace Csis.Admission.Application.Features.Students.Dtos;

/// <summary>
/// تراز , هدفمندی , معیشت طلبه
/// </summary>
public sealed record StudentTotalReportDto
{
    /// <summary>
    /// تراز طلبه
    /// </summary>
    public int Taraz { get; init; }
    /// <summary>
    /// هدفمندی طلبه
    /// </summary>
    public float TotalScore { get; init; }

    /// <summary>
    /// معیشت طلبه
    /// </summary>
    public float LivelihoodTotalScore { get; init; }

    /// <summary>
    /// حداکثر تراز
    /// </summary>
    public int MaxTaraz { get; init; }

    /// <summary>
    /// حداکثر هدفمندی
    /// </summary>
    public float MaxTotalScore { get; init; }

    /// <summary>
    /// حداکثر معیشت
    /// </summary>
    public float MaxLivelihoodTotalScore { get; init; }
}
