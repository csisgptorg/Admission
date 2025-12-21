using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>گزارش ساز</summary>
public class ReportBuilder : BaseEntity<long>
{
    /// <summary>عنوان</summary>
    public string Title { get; set; }

    /// <summary>جداول</summary>
    public string Tables { get; set; }

    /// <summary>فیلتر</summary>
    public string Filter { get; set; }
}
