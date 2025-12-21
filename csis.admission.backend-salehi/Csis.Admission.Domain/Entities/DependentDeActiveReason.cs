using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// کلاس معرف موجودیت دلیل انسداد پرونده
/// </summary>
public sealed class DependentDeActiveReason : BaseEntity<short>
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
