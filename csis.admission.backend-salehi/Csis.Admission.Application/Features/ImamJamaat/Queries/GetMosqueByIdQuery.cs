using Csis.Admission.Application.Features.ImamJamaat.Dtos;

namespace Csis.Admission.Application.Features.ImamJamaat.Queries;

public sealed record GetMosqueByIdQuery(int MosqueId) : IRequest<MosqueByIdDto>;

internal sealed class GetMosqueByIdQueryHandler(
    IRepository<Domain.Entities.ImamJamaat> imamjamaatRepository,
    IMapper mapper)
    : IRequestHandler<GetMosqueByIdQuery, MosqueByIdDto>
{
    public async Task<MosqueByIdDto> Handle(GetMosqueByIdQuery request, CancellationToken cancellationToken) {
        var founded = await imamjamaatRepository.GetOneAsync(x => x.MosqueId == request.MosqueId && !x.Mosque.Deleted, cancellationToken, x => x.Mosque.MosqueActivity, x => x.Mosque.MosqueAddress, x => x.ActiveSpousesInMosque);

        return founded == null
            ? throw new CommandValidationException($"Mosque with ID {request.MosqueId} not found.")
            : new MosqueByIdDto {
                Mosque = mapper.Map<MosqueDto>(founded.Mosque),
                ImamJamaat = mapper.Map<List<ImamJamaatDto>>(founded.Mosque.Imams),
                MosqueActivity = mapper.Map<MosqueActivityDto>(founded.Mosque.MosqueActivity),
                MosqueAddress = mapper.Map<MosqueAddressDto>(founded.Mosque.MosqueAddress)
            };
    }
}

