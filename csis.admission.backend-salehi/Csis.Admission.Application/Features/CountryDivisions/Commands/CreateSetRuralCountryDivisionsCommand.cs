using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.BasicData;
using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Features.CountryDivisions.Commands;

/// <summary>
/// ایجاد دهستان در تقسیمات کشوری
/// </summary>
public sealed class CreateSetRuralCountryDivisionsCommand :RepoCommandLogParam, IRequest<ProcedureResultDto>
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

internal sealed class CreateSetRuralCountryDivisionsCommandHandler : IRequestHandler<CreateSetRuralCountryDivisionsCommand, ProcedureResultDto>
{
    private readonly IBasicDataRepository _basicDataRepository;

    public CreateSetRuralCountryDivisionsCommandHandler(IBasicDataRepository basicDataRepository) {
        _basicDataRepository = basicDataRepository;
    }

    public async Task<ProcedureResultDto> Handle(CreateSetRuralCountryDivisionsCommand request, CancellationToken cancellationToken) {
        return await _basicDataRepository.CreateSetRural(request);
    }
}
