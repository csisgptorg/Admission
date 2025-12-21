using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// کلاس معرف موجودیت اقلیت شیعه
/// </summary>
public sealed class ShiaMinitory : BaseEntity
{
    /// <summary>
    /// کد مرکز
    /// </summary>
    public int Codm { get; set; }
}
