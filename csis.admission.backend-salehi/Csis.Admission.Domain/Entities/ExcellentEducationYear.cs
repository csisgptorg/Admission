using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>سال تحصیلی ممتازی</summary>
public class ExcellentEducationYear : BaseEntity<short>
{
    /// <summary>عنوان</summary>
    public string Title { get; set; }
}
