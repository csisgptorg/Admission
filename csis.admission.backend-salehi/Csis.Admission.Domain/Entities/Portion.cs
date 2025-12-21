using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <inheritdoc/>
public class Portion : BaseEntity<short>
{
    /// <summary>شهر</summary>
    public short? CityId { get; set; }

    /// <summary>عنوان</summary>
    public string Title { get; set; }
}
