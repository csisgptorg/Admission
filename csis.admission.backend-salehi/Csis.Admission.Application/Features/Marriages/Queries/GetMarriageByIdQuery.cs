using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Features.Marriages.Dtos;

namespace Csis.Admission.Application.Features.Marriages.Queries;

/// <summary>
/// دریافت موجودیت ازدواج با شناسه
/// </summary>
/// <param name="Id">شناسه موجودیت ازدواج</param>
public sealed record GetMarriageByIdQuery(int Id) : IRequest<MarriageDto>;

internal sealed class GetMarriageByIdQueryHandler : IRequestHandler<GetMarriageByIdQuery, MarriageDto>
{
    private readonly IPersonMarriageRepository _marriageRepo;
    private readonly ILogger<GetMarriageByIdQueryHandler> _logger;

    public GetMarriageByIdQueryHandler(IPersonMarriageRepository marriageRepo, ILogger<GetMarriageByIdQueryHandler> logger) {
        _marriageRepo = marriageRepo;
        _logger = logger;
    }

    public async Task<MarriageDto> Handle(GetMarriageByIdQuery request, CancellationToken cancellationToken) {
        _logger.LogDebug("Getting marriage with id {id}", request.Id);

        return await _marriageRepo.GetByIdAsync<MarriageDto>(request.Id, cancellationToken: cancellationToken)
            ?? throw new RecordNotFoundException<Marriage>(request.Id);
    }
}
