using Csis.Admission.Application.Features.Preaches.Dtos;

namespace Csis.Admission.Application.Features.Preaches.Queries;

/// <summary>
/// GetPreachByIdQuery
/// </summary>
/// <param name="Id"></param>
public sealed record GetPreachByIdQuery(int Id) : IRequest<PreachDto>;

internal sealed class GetPreachByIdQueryHandler : IRequestHandler<GetPreachByIdQuery, PreachDto>
{
    private readonly IRepository<Preach> _preachRepo;
    public GetPreachByIdQueryHandler(IRepository<Preach> preachRepo) {
        _preachRepo = preachRepo;
    }

    public async Task<PreachDto> Handle(GetPreachByIdQuery request, CancellationToken cancellationToken) {
        return await _preachRepo.GetByIdAsync<PreachDto>(request.Id, cancellationToken: cancellationToken)
            ?? throw new RecordNotFoundException<Preach>(request.Id);
    }
}
