using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.BasicData;
using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Features.CountryDivisions.Commands;

/// <summary>
/// تعریف شهر در تقسیمات کشوری
/// </summary>
public sealed class CreateSetTownInCountryDivisionsCommand :RepoCommandLogParam, IRequest<ProcedureResultDto>
{
    /// <summary>
    /// عنوان
    /// </summary>
    public string Title { get; init; }

    /// <summary>
    /// شناسه بخش
    /// </summary>
    public short PortionId { get; set; }
}

internal sealed class CreateSetTownInCountryDivisionsCommandHandler : IRequestHandler<CreateSetTownInCountryDivisionsCommand, ProcedureResultDto>
{
    private readonly IBasicDataRepository _basicDataRepository;

    public CreateSetTownInCountryDivisionsCommandHandler(IBasicDataRepository basicDataRepository) {
        _basicDataRepository = basicDataRepository;
    }

    public async Task<ProcedureResultDto> Handle(CreateSetTownInCountryDivisionsCommand request, CancellationToken cancellationToken) {
        return await _basicDataRepository.CreateSetTown(request);
    }
}
