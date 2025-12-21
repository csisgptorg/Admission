using Csis.Admission.Domain.Enums;
using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>مستندات درخواست</summary>
public sealed class RequestDocument : SoftDeletedBaseEntity<long>, IFilterable
{
    /// <inheritdoc/>
    public RequestDocument(Guid fileIdentifier, DocumentType? type) {
        FileId = fileIdentifier;
        Type = type;
    }

    /// <inheritdoc/>
    public RequestDocument() {}

    /// <summary>شناسه فایل</summary>
    public Guid FileId { get; set; }

    /// <summary>نوع</summary>
    public DocumentType? Type { get; set; }

    /// <summary>شناسه درخواست</summary>
    public long RequestId { get; set; }

    /// <summary>درخواست</summary>
    public Request Request { get; set; }

    /// <inheritdoc/>
    public string[] GetFilterableFields() {
        return [nameof(FileId), nameof(RequestId), nameof(Type)];
    }
}
