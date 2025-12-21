using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// هدفمندی
/// </summary>
public class TargetedScore : SoftDeletedBaseEntity
{
    /// <summary>
    /// دسته بندی شده
    /// </summary>
    public int ParentId { get; set; }

    /// <summary>
    /// Key
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// Value
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// IsOk
    /// </summary>
    public bool? IsOk { get; set; }
}
