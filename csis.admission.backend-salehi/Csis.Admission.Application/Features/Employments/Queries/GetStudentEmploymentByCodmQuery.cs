using Csis.FileManagement;
using Csis.Admission.Application.Features.Files.Dtos;
using Csis.Admission.Application.Features.Employments.Dtos;

namespace Csis.Admission.Application.Features.Employments.Queries;

/// <summary>دریافت اشتغال طلبه</summary>
/// <param name="Codm"></param>
public sealed record GetStudentEmploymentByCodmQuery(int? Codm) : IRequest<StudentEmploymentDto>;

internal sealed class GetEmploymentsByCodmQueryHandler(IRepository<StudentEmployment> employmentRepo, ICurrentUserService currentUser, ICsisFileManagementService fileService
    ) : IRequestHandler<GetStudentEmploymentByCodmQuery, StudentEmploymentDto>
{
    public async Task<StudentEmploymentDto> Handle(GetStudentEmploymentByCodmQuery query, CancellationToken cancellationToken) {
        _ = await Common.Utilities.SetCodm(query, currentUser);
        var result = await employmentRepo.GetOneAsync<StudentEmploymentDto>(x => x.Codm == query.Codm, cancellationToken: cancellationToken);
        await SetFilesInfoAsync(result);
        return result;
    }

    private async Task SetFilesInfoAsync(StudentEmploymentDto studentEmployment) {
        if ( studentEmployment is not null && studentEmployment.FileIdentifiers.Count != 0 ) {
            foreach ( var fileIdentifier in studentEmployment.FileIdentifiers ) {
                DownloadLink downloadLink = null;
                try { downloadLink = await fileService.DownloadLink(fileIdentifier); } catch ( Exception ) { }

                studentEmployment.FilesInfo.Add(new FileModelDto {
                    Link = downloadLink?.Link,
                    FullName = downloadLink?.FullName,
                    FileType = downloadLink?.Type,
                    Guid = fileIdentifier,
                });
            }
        }
    }
}
