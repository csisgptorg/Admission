using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Features.DependentDeActiveReasons.Dtos;
using Csis.Admission.Domain.Entities;

namespace Csis.Admission.Application.Features.DependentDeActiveReasons.Queries;

/// <summary>
/// دریافت همه دلیل انسداد پرونده ها
/// </summary>
public sealed record GetAllDependentDeActiveReasonsQuery : IRequest<List<DependentDeActiveReasonDto>>;

internal sealed class GetAllDependentDeActiveReasonsQueryHandler : IRequestHandler<GetAllDependentDeActiveReasonsQuery, List<DependentDeActiveReasonDto>>
{
    private readonly IRepository<DependentDeActiveReason, short> _dependentDeActiveReasonRepo;
    private readonly ILogger<GetAllDependentDeActiveReasonsQueryHandler> _logger;

    public GetAllDependentDeActiveReasonsQueryHandler(IRepository<DependentDeActiveReason, short> dependentDeActiveReasonRepo, ILogger<GetAllDependentDeActiveReasonsQueryHandler> logger) {
        _dependentDeActiveReasonRepo = dependentDeActiveReasonRepo;
        _logger = logger;
    }

    public async Task<List<DependentDeActiveReasonDto>> Handle(GetAllDependentDeActiveReasonsQuery request, CancellationToken cancellationToken) {
        var result = await _dependentDeActiveReasonRepo.GetAllAsync<DependentDeActiveReasonDto>(cancellationToken: cancellationToken);
        _logger.LogDebug("Found {count} dependentDeActiveReasons", result.Count);

        return result;
    }
}
