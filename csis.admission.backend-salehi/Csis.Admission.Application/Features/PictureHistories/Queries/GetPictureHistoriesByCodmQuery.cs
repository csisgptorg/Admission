using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Features.PictureHistories.Dtos;

namespace Csis.Admission.Application.Features.PictureHistories.Queries;

/// <summary>
/// GetPictureHistoriesByCodmQuery
/// </summary>
/// <param name="Codm"></param>
public sealed record GetPictureHistoriesByCodmQuery(int Codm) : IRequest<PictureHistoryDto[]>;

internal sealed class GetPictureHistoryByCodmQueryHandler : IRequestHandler<GetPictureHistoriesByCodmQuery, PictureHistoryDto[]>
{
    private readonly IStudentRepository _repo;
    public GetPictureHistoryByCodmQueryHandler(IStudentRepository repo) {
        _repo = repo;
    }

    public async Task<PictureHistoryDto[]> Handle(GetPictureHistoriesByCodmQuery request, CancellationToken cancellationToken) {
        var result = await _repo.GetPictureHistoriesByCodm(request.Codm);
        return [..result.OrderByDescending(x=> x.Id)];
    }
}
