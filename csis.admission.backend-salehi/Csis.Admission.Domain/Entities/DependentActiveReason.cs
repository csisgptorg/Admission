using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// دلیل رفع انسداد پرونده
/// </summary>
public sealed class DependentActiveReason : BaseEntity<short>
{
    /// <summary>
    /// عنوان
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// نوع
    /// </summary>
    public string Type { get; set; }
}
