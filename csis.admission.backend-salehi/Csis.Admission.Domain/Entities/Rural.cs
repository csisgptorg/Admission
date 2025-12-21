using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary></summary>
public class Rural : BaseEntity<short>
{
    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// ProvinceId
    /// </summary>
    public short PortionId { get; set; }

    /// <summary>
    /// Province
    /// </summary>
    public Portion Portion { get; set; }
}
