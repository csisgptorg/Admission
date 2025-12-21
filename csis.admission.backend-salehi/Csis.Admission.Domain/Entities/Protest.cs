using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// اعتراض
/// </summary>
public class Protest : SoftDeletedBaseEntity<long>, IAuditable
{
    /// <summary>
    /// کد مرکز
    /// </summary>
    public int Codm { get; set; }

    /// <summary>شناسه فیلد مورد اعتراض</summary>
    public ProtestFormTitle FieldId { get; set; }

    /// <summary>فیلد مورد اعتراض</summary>
    [NotMapped]
    public string FieldTitle { get; set; }

    /// <summary>مقدار فیلد مورد اعتراض</summary>
    public string FieldValue { get; set; }

    /// <summary>میتواند اعترض کند</summary>
    [NotMapped]
    public bool ProtestPossibility { get; set; }

    /// <summary>
    /// این فیلد مربوط به سه نوع اعتراض است (BeingLandlord, HousingBuySellHistory, PersonalHousingHistory)
    /// </summary>
    public bool? HasHousingHistory { get; set; }

    /// <summary> شرح مورد اعتراض </summary>
    public string FieldDescription { get; set; }

    /// <summary>
    /// کد رهگیری سامانه سخا
    /// </summary>
    public long? RequestId { get; set; }

    /// <summary>
    /// درخواست مرتبط با اعتراض
    /// </summary>
    public Request Request { get; set; }

    /// <inheritdoc/>
    public static RequestType GetRequestType(ProtestFormTitle protestFileId) {
        var type = nameof(Protest) + protestFileId.ToString();
        return Enum.Parse<RequestType>(type);
    }

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
