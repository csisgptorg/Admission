using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <inheritdoc/>
public class Address : SoftDeletedBaseEntity, IAuditable
{
    /// <inheritdoc/>
    public int Codm { get; set; }

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

    /// <inheritdoc/>
    public int? ConfirmDate { get; set; }

    /// <summary>همیشه یک</summary>
    public short ProjectCode { get; set; }

    /// <summary>همیشه یک</summary>
    public bool? Flag { get; set; }

    #region AuditLog لاگ خودکار
    /// <inheritdoc/>
    public Guid? TempId { get; set; }

    /// <inheritdoc/>
    public DataSource? AuditDataSource { get; set; }

    /// <inheritdoc/>
    public int? AuditRequestId { get; set; }

    /// <inheritdoc/>
    public int? AuditPersonId { get; set; }
    #endregion
}
