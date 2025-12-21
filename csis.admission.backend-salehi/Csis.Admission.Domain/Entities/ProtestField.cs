using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// فیلد مورد اعتراض
/// </summary>
public class ProtestField : BaseEntity<short>
{
    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; }
}
