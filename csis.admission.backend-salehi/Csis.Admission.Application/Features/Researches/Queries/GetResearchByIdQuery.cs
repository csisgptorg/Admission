using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Features.Researches.Dtos;
using Csis.Admission.Domain.Entities;

namespace Csis.Admission.Application.Features.Researches.Queries;

/// <summary>
/// دریافت پژوهش با شناسه
/// </summary>
/// <param name="Id">شناسه پژوهش</param>
public sealed record GetResearchByIdQuery(int Id) : IRequest<ResearchDto>;

internal sealed class GetResearchByIdQueryHandler : IRequestHandler<GetResearchByIdQuery, ResearchDto>
{
    private readonly IRepository<Research> _researchRepo;

    public GetResearchByIdQueryHandler(IRepository<Research> researchRepo) {
        _researchRepo = researchRepo;
    }

    public async Task<ResearchDto> Handle(GetResearchByIdQuery request, CancellationToken cancellationToken) {
        return await _researchRepo.GetByIdAsync<ResearchDto>(request.Id, cancellationToken: cancellationToken)
            ?? throw new RecordNotFoundException<Research>(request.Id);
    }
}
