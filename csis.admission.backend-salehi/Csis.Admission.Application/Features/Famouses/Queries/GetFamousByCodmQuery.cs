using Csis.Admission.Application.Features.Famouses.Dtos;

namespace Csis.Admission.Application.Features.Famouses.Queries;

/// <summary>
/// دریافت مشهور با شناسه
/// </summary>
public sealed record GetFamousByCodmQuery(int Codm) : IRequest<List<StudentFamousDto>>;

internal sealed class GetFamousByCodmQueryHandler(IRepository<Famous> famousRepo)
    : IRequestHandler<GetFamousByCodmQuery, List<StudentFamousDto>>
{
    public async Task<List<StudentFamousDto>> Handle(GetFamousByCodmQuery request, CancellationToken cancellationToken) {
        return await famousRepo.GetAllAsync<StudentFamousDto>(x => x.Codm == request.Codm, cancellationToken: cancellationToken)
               ?? throw new CommandValidationException($"طلبه مشهور با شناسه {request.Codm} یافت نشد.");
    }
}
