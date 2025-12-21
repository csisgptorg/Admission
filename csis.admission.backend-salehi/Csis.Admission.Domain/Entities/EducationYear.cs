using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>سال تحصیلی</summary>
public class EducationYear : BaseEntity<short>
{
    /// <summary>عنوان</summary>
    public string Title { get; set; }
}
