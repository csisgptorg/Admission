using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>مستندات درخواست</summary>
public sealed class CaseFillingRequestDocument : SoftDeletedBaseEntity<long>, IFilterable
{
    /// <inheritdoc/>
    public CaseFillingRequestDocument(Guid fileIdentifier, DocumentType? type) {
        FileId = fileIdentifier;
        Type = type;
    }

    /// <inheritdoc/>
    public CaseFillingRequestDocument() {}

    /// <summary>شناسه فایل</summary>
    public Guid FileId { get; set; }

    /// <summary>نوع</summary>
    public DocumentType? Type { get; set; }

    /// <summary>شناسه درخواست</summary>
    public long RequestId { get; set; }

    /// <summary>درخواست</summary>
    public CaseFillingRequest Request { get; set; }

    /// <inheritdoc/>
    public string[] GetFilterableFields() {
        return [nameof(FileId), nameof(RequestId), nameof(Type)];
    }
}
