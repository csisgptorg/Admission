using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>خدمات مرکز</summary>
public class CsisService : BaseEntity
{
    /// <summary>عنوان</summary>
    public string Title { get; set; }

    /// <summary>خدمات مسدود طلبه</summary>
    public ICollection<StudentBlockService> StudentBlockServices { get; set; }

    /// <summary>خدمات مسدود تکفل</summary>
    public ICollection<DependentBlockService> DependentBlockServices { get; set; }
}
