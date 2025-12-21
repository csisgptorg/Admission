using Csis.Admission.Application.Features.Teaches.Dtos;

namespace Csis.Admission.Application.Features.Teaches.Queries;

/// <summary>
/// GetTeachByIdQuery
/// </summary>
/// <param name="Id"></param>
public sealed record GetTeachByIdQuery(int Id) : IRequest<TeachDto>;

internal sealed class GetTeachByIdQueryHandler : IRequestHandler<GetTeachByIdQuery, TeachDto>
{
    private readonly IRepository<Teach> _teachRepo;
    public GetTeachByIdQueryHandler(IRepository<Teach> teachRepo) {
        _teachRepo = teachRepo;
    }

    public async Task<TeachDto> Handle(GetTeachByIdQuery request, CancellationToken cancellationToken) {
        return await _teachRepo.GetByIdAsync<TeachDto>(request.Id, cancellationToken: cancellationToken)
            ?? throw new RecordNotFoundException<Teach>(request.Id);
    }
}
