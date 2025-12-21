using Csis.Admission.Application.Features.CompleteStudentInfos.Dtos;

namespace Csis.Admission.Application.Features.CompleteStudentInfos.Queries;

/// <summary>دریافت اطلاعات کامل طلبه</summary>
public sealed record GetCompleteStudentInfoByCodmQuery(int Codm) : IRequest<CompleteStudentInfoDto>;

internal sealed class GetStudentAdmissionAuditLogsByCodmQueryHandler(IRepository<CompleteStudentInfo> repo) : IRequestHandler<GetCompleteStudentInfoByCodmQuery, CompleteStudentInfoDto>
{
    public async Task<CompleteStudentInfoDto> Handle(GetCompleteStudentInfoByCodmQuery request, CancellationToken cancellationToken) {
        return await repo.GetOneAsync<CompleteStudentInfoDto>(x => x.Codm == request.Codm, cancellationToken: cancellationToken)
            ?? throw new CommandValidationException("طلبه ای با این کد مرکز وجود ندارد.");
    }
}
