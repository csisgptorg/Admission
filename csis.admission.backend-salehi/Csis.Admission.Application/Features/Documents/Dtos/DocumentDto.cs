using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Documents.Dtos;

/// <inheritdoc/>
public sealed record RequestDocumentDto : BaseDto<RequestDocumentDto, RequestDocument, long>
{
    /// <inheritdoc/>
    public Guid FileId { get; set; }

    /// <inheritdoc/>
    public TableName Table { get; set; }

    /// <inheritdoc/>
    public long TableRecordId { get; set; }

    /// <inheritdoc/>
    public FileType Type { get; set; }
}
