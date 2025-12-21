using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// Province
/// </summary>
public class Province : BaseEntity<short>
{
    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; }
}
