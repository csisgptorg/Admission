using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;
/// <summary>
/// آدرس مسجد
/// </summary>
public sealed class MosqueAddress : SoftDeletedBaseEntity
{
    // ارتباط با مسجد  
    /// <summary></summary>
    public int? MosqueId { get; set; } // شناسه مسجد مرتبط  

    /// <summary></summary>
    public Mosque Mosque { get; set; } // موجودیت مسجد مرتبط  

    /// <inheritdoc/>
    public short? ProvinceId { get; set; }

    /// <summary>شهرستان </summary>
    public short? CityId { get; set; }

    /// <summary>بخش</summary>
    public short? PortionId { get; set; }

    /// <summary>شهر</summary>
    public short? TownId { get; set; }

    /// <summary>دهستان</summary>
    public short? RuralId { get; set; }

    /// <summary>شهرک</summary>
    public string Township { get; set; }

    /// <inheritdoc/>
    public string Village { get; set; }

    /// <summary>محله</summary>
    public string District { get; set; }

    /// <summary> شهرک</summary>
    public string Dorp { get; init; }

    /// <summary>خیابان اصلی</summary>
    public string Avenue { get; set; }

    /// <summary>خیابان فرعی</summary>
    public string Street { get; set; }

    /// <summary>کوچه اصلی</summary>
    public string Alley { get; set; }

    /// <summary>کوچه فرعی</summary>
    public string Lane { get; set; }

    /// <summary>پلاک</summary>
    public string Number { get; set; }

    /// <summary>مجتمع</summary>
    public string Complex { get; set; }

    /// <summary>بلوک</summary>
    public string Block { get; set; }

    /// <summary>واحد</summary>
    public string Unit { get; set; }

    /// <inheritdoc/>
    public short? Floor { get; set; }

    /// <inheritdoc/>
    public long? ZipCode { get; set; }
}
