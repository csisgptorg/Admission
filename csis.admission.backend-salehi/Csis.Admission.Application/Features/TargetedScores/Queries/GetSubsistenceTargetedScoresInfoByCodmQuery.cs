using Csis.Admission.Application.Features.TargetedScores.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.TargetedScores.Queries;

/// <summary>امتیاز هدفمندی معیشتی</summary>
public sealed record GetSubsistenceTargetedScoresInfoByCodmQuery(int Codm) : IRequest<TargetedScoreDto[]>;

internal sealed class GetSubsistenceTargetedScoresInfoByCodmQueryHandler : IRequestHandler<GetSubsistenceTargetedScoresInfoByCodmQuery, TargetedScoreDto[]>
{
    private readonly IStudentRepository _repo;
    public GetSubsistenceTargetedScoresInfoByCodmQueryHandler(IStudentRepository repo) {
        _repo = repo;
    }

    public async Task<TargetedScoreDto[]> Handle(GetSubsistenceTargetedScoresInfoByCodmQuery request, CancellationToken cancellationToken) {

        var keyOrder = new List<string>
        {
            "عائله مندي",
            "دهک معيشتي",
            "وضعيت سکونت",
            "تحصيل در سطح يک"
        };

        var result = await _repo.GetSubsistenceTargetedScoresByCodm(request.Codm)
            ?? throw new RecordNotFoundException<TargetedScore>(request.Codm);

        //return [.. result.OrderBy(x =>keyOrder.IndexOf(x.Key) == -1 ? int.MaxValue : keyOrder.IndexOf(x.Key))];
        return result;
    }
}
