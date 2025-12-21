using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.TargetedScores.Dtos;

namespace Csis.Admission.Application.Features.TargetedScores.Queries;

/// <summary>دریافت لیست تاریخچه امتیاز هدف مندی</summary>
public sealed record GetTargetingHistoryChangesByCodmQuery(int Codm) : IRequest<List<TargetingHistoryChangeDto>>;

internal sealed class GetTargetingHistoryChangesByCodmQueryHandler(IRepository<TargetedScoreHistory> repo)
    : IRequestHandler<GetTargetingHistoryChangesByCodmQuery, List<TargetingHistoryChangeDto>>
{
    public async Task<List<TargetingHistoryChangeDto>> Handle(GetTargetingHistoryChangesByCodmQuery request, CancellationToken cancellationToken) {

        var result = new List<TargetingHistoryChangeDto>();
        var histories = await repo.GetAllAsync(x => x.Codm == request.Codm, cancellationToken: cancellationToken);
        if ( !histories.Any() ) {
            return result;
        }

        histories = histories.OrderBy(x => x.Id).ToList();
        for ( var i = 0; i < histories.Count; i++ ) {
            var history = new TargetingHistoryChangeDto {
                Changes = i == 0 ? CompareTargetedScores(null, histories[i].TargetedScore)
                    : CompareTargetedScores(histories[i - 1].TargetedScore, histories[i].TargetedScore),
                Date = histories[i].Date.IntDateToString(),
                Time = histories[i].Time,
                Version = histories[i].Version ?? 1
            };
            result.Add(history);
        }

        return result;
    }

    private static List<TargetingHistoryChangeDto.Change> CompareTargetedScores
        (TargetedScoreHistory.TargetedScoreModel old, TargetedScoreHistory.TargetedScoreModel @new) {

        var changes = new List<TargetingHistoryChangeDto.Change>();
        if ( old is null && @new is null || @new is null ) {
            return changes;
        }

        var properties = typeof(TargetedScoreHistory.TargetedScoreModel).GetProperties();
        foreach ( var prop in properties ) {

            var oldValue = old is not null ? prop.GetValue(old) : null;
            var newValue = prop.GetValue(@new);

            if ( prop.PropertyType == typeof(string) ) {
                oldValue = oldValue?.ToString().Replace("ي", "ی").Replace("ك", "ک").Trim();
                newValue = newValue?.ToString().Replace("ي", "ی").Replace("ك", "ک").Trim();
            }

            if ( Equals(oldValue, newValue) ) {
                continue;
            }

            changes.Add(new TargetingHistoryChangeDto.Change(prop.GetDisplayName(), oldValue?.ToString(), newValue?.ToString()));
        }

        return changes;
    }
}
