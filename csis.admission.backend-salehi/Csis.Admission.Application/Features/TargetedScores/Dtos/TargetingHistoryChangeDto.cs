using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.TargetedScores.Dtos;

/// <summary>تاریخچه امتیاز هدفمندی</summary>
public record TargetingHistoryChangeDto : BaseDto<TargetingHistoryChangeDto, TargetedScoreHistory>
{
    /// <summary>امتیاز هدفمندی</summary>
    public List<Change> Changes { get; set; }
    /// <summary>مدل امتیاز هدفمندی</summary>
    public record Change(string Property, string PreviousValue, string CurrentValue);

    /// <summary>تاریخ</summary>
    public string Date { get; set; }
    /// <summary>زمان</summary>
    public TimeSpan? Time { get; set; }
    /// <summary>ورژن</summary>
    public int? Version { get; set; }
}
