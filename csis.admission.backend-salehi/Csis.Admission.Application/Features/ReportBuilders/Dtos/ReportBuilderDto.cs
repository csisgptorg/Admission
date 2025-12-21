using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.ReportBuilders.Dtos;

/// <summary>گزارش ساز</summary>
public sealed record ReportBuilderDto : BaseDto<ReportBuilderDto, ReportBuilder,long>
{
    /// <summary>عنوان</summary>
    public string Title { get; set; }

    /// <summary>جداول</summary>
    public string Tables { get; set; }

    /// <summary>فیلترها</summary>
    public string Filter { get; set; }
}
