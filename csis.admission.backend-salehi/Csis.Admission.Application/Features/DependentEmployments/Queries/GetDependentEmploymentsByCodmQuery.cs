using Csis.FileManagement;
using Csis.Authorization.Services;
using Csis.Admission.Application.Features.Files.Dtos;
using Csis.Admission.Application.Features.DependentEmployments.Dtos;

namespace Csis.Admission.Application.Features.DependentEmployments.Queries;

/// <summary>دریافت اشتغال تکفل</summary>
/// <param name="Codm"></param>
public sealed record GetDependentsEmploymentByCodmQuery(int? Codm) : IRequest<List<DependentEmploymentDto>>;

internal sealed class GetDependentsEmploymentByCodmQueryHandler(
    IRepository<DependentEmployment> repo,
    ICsisFileManagementService fileService,
    ICurrentUserService currentUser
    ) : IRequestHandler<GetDependentsEmploymentByCodmQuery, List<DependentEmploymentDto>>
{
    public async Task<List<DependentEmploymentDto>> Handle(GetDependentsEmploymentByCodmQuery request, CancellationToken cancellationToken) {

        _ = await Common.Utilities.SetCodm(request, currentUser);
        var dependents = await repo.GetAllAsync<DependentEmploymentDto>(x => x.Codm == request.Codm, false, cancellationToken);
        await SetFilesInfoAsync([.. dependents]);
        return dependents;
    }

    private async Task SetFilesInfoAsync(List<DependentEmploymentDto> dependents) {
        if ( !dependents.Any() ) { return; }

        foreach ( var studentEmployment in dependents ) {
            foreach ( var fileIdentifier in studentEmployment.FileIdentifiers ) {
                var downloadLink = await fileService.DownloadLink(fileIdentifier);
                studentEmployment.FilesInfo.Add(new FileModelDto {
                    Link = downloadLink.Link,
                    FullName = downloadLink.FullName,
                    FileType = downloadLink.Type,
                    Guid = fileIdentifier,
                });
            }
        }
    }
}
