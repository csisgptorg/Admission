using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.BasicData;
using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Features.CountryDivisions.Commands;

/// <summary>
/// ایجاد بخش در تقسیمات کشوری
/// </summary>
public sealed class CreateSetPortionCountryDivisionsCommand : RepoCommandLogParam, IRequest<ProcedureResultDto>
{
    /// <summary>
    /// عنوان
    /// </summary>
    public string Title { get; init; }

    /// <summary>
    /// شناسه شهر
    /// </summary>
    public short CityId { get; set; }
}

internal sealed class CreateSetPortionCountryDivisionsCommandHandler : IRequestHandler<CreateSetPortionCountryDivisionsCommand, ProcedureResultDto>
{
    private readonly IBasicDataRepository _basicDataRepository;

    public CreateSetPortionCountryDivisionsCommandHandler(IBasicDataRepository basicDataRepository) {
        _basicDataRepository = basicDataRepository;
    }

    public async Task<ProcedureResultDto> Handle(CreateSetPortionCountryDivisionsCommand request, CancellationToken cancellationToken) {
        return await _basicDataRepository.CreateSetPortion(request);
    }
}
