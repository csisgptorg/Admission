using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Features.DependentActiveReasons.Dtos;
using Csis.Admission.Domain.Entities;

namespace Csis.Admission.Application.Features.DependentActiveReasons.Queries;

/// <summary>
/// دریافت همه دلیل رفع انسداد پرونده ها
/// </summary>
public sealed record GetAllDependentActiveReasonsQuery : IRequest<List<DependentActiveReasonDto>>;

internal sealed class GetAllDependentActiveReasonsQueryHandler : IRequestHandler<GetAllDependentActiveReasonsQuery, List<DependentActiveReasonDto>>
{
    private readonly IRepository<DependentActiveReason, short> _dependentActiveReasonRepo;
    private readonly ILogger<GetAllDependentActiveReasonsQueryHandler> _logger;

    public GetAllDependentActiveReasonsQueryHandler(IRepository<DependentActiveReason, short> dependentActiveReasonRepo, ILogger<GetAllDependentActiveReasonsQueryHandler> logger) {
        _dependentActiveReasonRepo = dependentActiveReasonRepo;
        _logger = logger;
    }

    public async Task<List<DependentActiveReasonDto>> Handle(GetAllDependentActiveReasonsQuery request, CancellationToken cancellationToken) {
        var result = await _dependentActiveReasonRepo.GetAllAsync<DependentActiveReasonDto>(cancellationToken: cancellationToken);
        _logger.LogDebug("Found {count} dependentActiveReasons", result.Count);

        return result;
    }
}
