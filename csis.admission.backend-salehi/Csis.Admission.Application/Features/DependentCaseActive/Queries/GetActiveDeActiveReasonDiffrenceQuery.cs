using Csis.Admission.Application.Features.DependentCaseActive.Commands;
using static Csis.Admission.Application.Common.Utilities;

namespace Csis.Admission.Application.Features.DependentCaseActive.Queries;

/// <summary>
/// محاسبه علت فعال یا غیر فعال بودن تکفل
/// </summary>
/// <param name="Codm"></param>
/// <param name="DependentId"></param>
public sealed record GetActiveDeActiveReasonDiffrenceQuery(int Codm, long DependentId) : IRequest<List<PropertyDifference>>;

internal sealed class GetActiveDeActiveReasonDiffrenceQueryHandler(
    IRepository<DependentSummary, long> dependentSummaryRepository,
    IMediator mediator

    ) : IRequestHandler<GetActiveDeActiveReasonDiffrenceQuery, List<PropertyDifference>>
{

    public async Task<List<PropertyDifference>> Handle(GetActiveDeActiveReasonDiffrenceQuery request, CancellationToken cancellationToken) {
        var dependentSummary = await dependentSummaryRepository.GetOneAsync(x => x.Codm == request.Codm && x.Id == request.DependentId, cancellationToken: cancellationToken);
        var newReason = await GetNewReason(request.Codm, request.DependentId);

        var previousReason = new DependentActiveDeactiveReason {
            IsActive = dependentSummary.IsActive,
            ActiveReason = (short?) dependentSummary.ActiveReason,
            DeActiveReason = (short?) dependentSummary.DeActiveReason,
            DeActiveReasonOnExpire = dependentSummary.DeActiveReasonOnExpire,
            ExpireDate = dependentSummary.DateExpire
        };

        var differences = CompareObjects(previousReason, newReason);

        return differences;
    }

    private async Task<DependentActiveDeactiveReason> GetNewReason(int codM, long dependentId) {
        var command = new UpdateDependentCaseActiveEmployeeRequestCommand { Codm = codM, DependentId = dependentId, Confirmed = false };
        var result = await mediator.Send(command);
        return result;
    }
}
