using Csis.Admission.Application.Common.Helpers;
using Csis.Admission.Application.Features.CaseFilings.Dtos;
using Csis.FileManagement;

namespace Csis.Admission.Application.Features.CaseFilings.Queries;

/// <summary> درخواست تأیید اشتغال </summary>
public sealed record GetStudentCaseQuery : IRequest<AdmissionCaseUserDto>
{
    /// <summary>توکن</summary>
    public Guid Token { get; init; }
}
internal sealed class GetStudentCaseQueryHandler(
    IRepository<AdmissionCaseUser, Guid> caseRepository,
    ICsisFileManagementService fileManagementService,
    IMapper mapper)
    : IRequestHandler<GetStudentCaseQuery, AdmissionCaseUserDto>
{
    public async Task<AdmissionCaseUserDto> Handle(GetStudentCaseQuery request, CancellationToken cancellationToken) {

        var admissionCaseUser = await caseRepository.GetOneAsync<AdmissionCaseUserDto>(x => x.Id == request.Token, cancellationToken: cancellationToken)
                                ?? throw new CommandValidationException("شناسه نامعتبر است.");
        await FileInfoHelper.SetRequestFilesInfoAsync(admissionCaseUser, fileManagementService);

        return admissionCaseUser;
    }
}


