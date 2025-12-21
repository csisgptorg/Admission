using Csis.Admission.Application.Features.ImamJamaat.Dtos;
using Csis.Authorization.Services;

namespace Csis.Admission.Application.Features.ImamJamaat.Queries;

/// <summary>
/// دریافت اطلاعات مسجد بر اساس شناسه آن برای دانش‌آموزان
/// </summary>
/// <param name="MosqueId"></param>
public sealed record GetMosqueByIdStudentQuery(int MosqueId) : IRequest<MosqueByIdDto>;

internal sealed class GetMosqueByIdStudentQueryHandler : IRequestHandler<GetMosqueByIdStudentQuery, MosqueByIdDto>
{
    private readonly IRepository<Domain.Entities.ImamJamaat> _imamjamaatRepository;
    private readonly IMapper _mapper;
    private readonly ICsisAuthenticatedUserService _csisAuthenticatedUserService;

    public GetMosqueByIdStudentQueryHandler(
        IRepository<Domain.Entities.ImamJamaat> imamjamaatRepository,
        IMapper mapper,
        ICsisAuthenticatedUserService csisAuthenticatedUserService) {
        _imamjamaatRepository = imamjamaatRepository;
        _mapper = mapper;
        _csisAuthenticatedUserService = csisAuthenticatedUserService;
    }
    public async Task<MosqueByIdDto> Handle(GetMosqueByIdStudentQuery request, CancellationToken cancellationToken) {
        var codM = await _csisAuthenticatedUserService.GetStudentCodmAsync();

        var founded = await _imamjamaatRepository.GetOneAsync(x => x.CodM == int.Parse(codM) && x.MosqueId == request.MosqueId && !x.Mosque.Deleted, cancellationToken, x => x.Mosque.MosqueActivity, x => x.Mosque.MosqueAddress, x => x.ActiveSpousesInMosque);

        return founded == null
            ? throw new CommandValidationException($"Mosque with ID {request.MosqueId} not found for student with codM {codM}.")
            : new MosqueByIdDto {
                Mosque = _mapper.Map<MosqueDto>(founded.Mosque),
                ImamJamaat = _mapper.Map<List<ImamJamaatDto>>(founded.Mosque.Imams),
                MosqueActivity = _mapper.Map<MosqueActivityDto>(founded.Mosque.MosqueActivity),
                MosqueAddress = _mapper.Map<MosqueAddressDto>(founded.Mosque.MosqueAddress)
            };
    }
}
