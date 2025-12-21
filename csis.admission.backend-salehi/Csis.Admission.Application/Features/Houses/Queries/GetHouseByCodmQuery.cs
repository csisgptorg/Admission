using Csis.FileManagement;
using Csis.Admission.Application.Features.Files.Dtos;
using Csis.Admission.Application.Features.Houses.Dtos;

namespace Csis.Admission.Application.Features.Houses.Queries;

/// <summary>
/// GetHouseByCodmQuery
/// </summary>
/// <param name="Codm"></param>
public sealed record GetHouseByCodmQuery(int? Codm) : IRequest<HouseDto>;

internal sealed class GetHouseByCodmQueryHandler(
    IRepository<House> houseRepo,
    IRepository<Tenant> tenantRepo,
    ICurrentUserService currentUser,
    IRepository<RequestDocument, long> documentRepo,
    ICsisFileManagementService fileManagementService)
    : IRequestHandler<GetHouseByCodmQuery, HouseDto>
{
    public async Task<HouseDto> Handle(GetHouseByCodmQuery command, CancellationToken cancellationToken)
    {
        _ = await Common.Utilities.SetCodm(command, currentUser);
        var house = await houseRepo.GetOneAsync<HouseDto>(x => x.Codm == command.Codm, cancellationToken: cancellationToken);

        if (house != null)
        {
            // دریافت Tenant از طریق Codm
            var tenant = await tenantRepo.GetOneAsync<TenantDto>(x => x.Codm == command.Codm, cancellationToken: cancellationToken);
            house = house with { Tenant = tenant };
            
            await SetFilesInfoAsync(house);
        }

        return house;
    }

    private async Task SetFilesInfoAsync(HouseDto house)
    {
        if (house != null && house.FileIdentifiers.Any())
        {
            var files = (await documentRepo.GetAllAsync(x => house.FileIdentifiers.Contains(x.FileId))).DistinctBy(x => x.FileId).ToList() ?? [];

            foreach (var file in files)
            {
                try
                {
                    var downloadLink = await fileManagementService.DownloadLink(file.FileId);
                    if (downloadLink != null)
                    {
                        house.FilesInfo.Add(new FileModelDto
                        {
                            Link = downloadLink.Link,
                            FullName = downloadLink.FullName,
                            FileType = file.Type != null ? (FileTypeEnum)file.Type : 0,
                            Guid = file.FileId,
                        });
                    }
                }
                catch (Exception) { }
            }
        }
    }
}
