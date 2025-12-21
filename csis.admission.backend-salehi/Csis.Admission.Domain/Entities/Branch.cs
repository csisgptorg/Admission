namespace Csis.Admission.Domain.Entities;

/// <summary>شعبه</summary>
public class Branch : BaseEntity<short>
{
    /// <summary>عنوان</summary>
    public string Title { get; set; }

    /// <summary>کد استان</summary>
    public int ProvinceId { get; set; }

    /// <summary>نمایندگی دارد</summary>
    public bool HasAgency { get; set; }
}
