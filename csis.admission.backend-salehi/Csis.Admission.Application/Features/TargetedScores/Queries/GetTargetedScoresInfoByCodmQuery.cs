using Csis.Admission.Application.Features.TargetedScores.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.TargetedScores.Queries;

/// <summary>
/// هدفمندی
/// </summary>
/// <param name="Codm"></param>
public sealed record GetTargetedScoresInfoByCodmQuery(int Codm) : IRequest<TargetedScoreDto[]>;

internal sealed class GetTargetedScoresInfoByCodmQueryHandler : IRequestHandler<GetTargetedScoresInfoByCodmQuery, TargetedScoreDto[]>
{
    private readonly IStudentRepository _repo;
    public GetTargetedScoresInfoByCodmQueryHandler(IStudentRepository repo) {
        _repo = repo;
    }

    public async Task<TargetedScoreDto[]> Handle(GetTargetedScoresInfoByCodmQuery request, CancellationToken cancellationToken) {

        var keyOrder = new List<string>
        {
            "شروط کلي",
            "امتياز تحصيل حوزوي",
            "امتياز تحصيلات دانشگاهي قبل از ورود به حوزه",
            "امتياز تدريس جاري",
            "امتياز سابقه تدريس",
            "امتياز تبليغ جاري",
            "امتياز سابقه تبليغ",
            "امتياز پژوهش جاري",
            "امتياز سابقه پژوهش",
            "امتياز ايثارگري و شايستگي ها",
            "امتياز حافظين",
            "جمع امتيازات",
            "ضرايب",
            "امتياز کل",
        };

        var result = await _repo.GetTargetedScoresByCodm(request.Codm)
            ?? throw new RecordNotFoundException<TargetedScore>(request.Codm);

        //return [.. result.OrderBy(x =>keyOrder.IndexOf(x.Key) == -1 ? int.MaxValue : keyOrder.IndexOf(x.Key))];
        return result;
    }
}
