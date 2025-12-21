using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// City
/// </summary>
public class City : BaseEntity<short>
{
    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// ProvinceId
    /// </summary>
    public short ProvinceId { get; set; }

    /// <summary>
    /// Province
    /// </summary>
    public Province Province { get; set; }
}
