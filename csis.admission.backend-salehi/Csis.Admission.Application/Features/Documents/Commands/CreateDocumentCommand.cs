using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Documents.Commands;

/// <inheritdoc/>
public sealed record CreateDocumentCommand : BaseCommandDto<CreateDocumentCommand, RequestDocument,long>, IRequest<long>
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

internal sealed class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, long>
{
    private readonly IRepository<RequestDocument,long> _repo;
    public CreateDocumentCommandHandler(IRepository<RequestDocument, long> repo) {
        _repo = repo;
    }

    public async Task<long> Handle(CreateDocumentCommand request, CancellationToken cancellationToken) {

        if ( await _repo.ExistsAsync(x => x.FileId== request.FileId, cancellationToken: cancellationToken) ) {
            throw new CommandValidationException(nameof(request.FileId), "شناسه فایل وارد شده تکراری است");
        }

        var document = request.ToEntity();
        await _repo.InsertAsync(document, true,cancellationToken);
        return document.Id;
    }
}
