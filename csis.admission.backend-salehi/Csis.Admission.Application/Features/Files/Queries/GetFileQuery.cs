using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Features.Files.Dtos;
using Csis.FileManagement;

namespace Csis.Admission.Application.Features.Files.Queries;

public sealed record GetFileQuery(Guid FileIdentifier) : IRequest<FileModelDto>;

internal sealed class GetFileQueryHandler : IRequestHandler<GetFileQuery, FileModelDto>
{
    private readonly ICsisFileManagementService _csisFileManagementService;

    public GetFileQueryHandler(ICsisFileManagementService csisFileManagementService) {
        _csisFileManagementService = csisFileManagementService;
    }
    public async Task<FileModelDto> Handle(GetFileQuery request, CancellationToken cancellationToken) {
        var file = await _csisFileManagementService.DownloadLink(request.FileIdentifier, cancellationToken: cancellationToken);
        return new FileModelDto {
            Link = file.Link,
            FullName = file.FullName,
            FileType = file.Type,
            Guid = request.FileIdentifier
        };
    }
}
