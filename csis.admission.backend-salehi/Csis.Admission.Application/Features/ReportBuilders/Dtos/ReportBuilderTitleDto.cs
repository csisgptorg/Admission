using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.ReportBuilders.Dtos;

/// <summary>گزارش ساز</summary>
public sealed record ReportBuilderTitleDto : BaseDto<ReportBuilderTitleDto, ReportBuilder, long>
{
    /// <summary>عنوان</summary>
    public string Title { get; set; }
}

