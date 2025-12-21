using Csis.Admission.Application.Features.Insurances.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.Insurances.Queries;

/// <inheritdoc/>
public sealed record GetStudentDependentInsurancesByCodmQuery(int Codm, long? DependentId) : IRequest<StudentDependentInsuranceDto>;

internal sealed class GetStudentDependentInsurancesByCodmHandler : IRequestHandler<GetStudentDependentInsurancesByCodmQuery, StudentDependentInsuranceDto>
{
    private readonly IStudentRepository _studentRepository;
    private readonly ICsisSupInsuranceService _supInsuranceService;
    private readonly ILogger<GetStudentDependentInsurancesByCodmHandler> _logger;
    private readonly ICsisHealthInsuranceService _healthInsuranceService;
    public GetStudentDependentInsurancesByCodmHandler(ICsisHealthInsuranceService healthInsuranceService, ICsisSupInsuranceService supInsuranceService,
        ILogger<GetStudentDependentInsurancesByCodmHandler> logger, IStudentRepository studentRepository) {
        _logger = logger;
        _supInsuranceService = supInsuranceService;
        _healthInsuranceService = healthInsuranceService;
        _studentRepository = studentRepository;
    }

    public async Task<StudentDependentInsuranceDto> Handle(GetStudentDependentInsurancesByCodmQuery request, CancellationToken cancellationToken) {

        var result = new StudentDependentInsuranceDto(request.Codm, request.DependentId);

        try {
            var caseState = await _healthInsuranceService.CaseState(request.Codm, request.DependentId, cancellationToken);
            result.HealthInsuranceStatus = caseState.Status;
            result.HealthInsuranceCaseNumber = caseState.CaseNumber;
        } catch ( Exception exception ) {
            var message = "اطلاعات بیمه سلامت در دسترس نمی باشد.";
            result.HealthInsuranceCaseNumber = message;
            _logger.LogError(exception, message);
        }

        try {
            var healthStatus = await _supInsuranceService.GetHealthStatus(request.Codm, request.DependentId, cancellationToken);
            result.SupInsuranceHealthStatus = healthStatus.Status;
            result.SupInsuranceHealthPlanTitle = healthStatus.PlanTitle;
        } catch ( Exception exception ) {
            var message = "اطلاعات بیمه تکمیلی در دسترس نمی باشد.";
            result.HealthInsuranceCaseNumber = message;
            _logger.LogError(exception, message);
        }

        try {
            var lifeStatus = await _supInsuranceService.GetLifeStatus(request.Codm, request.DependentId, cancellationToken);
            result.SupInsuranceLifeStatus = lifeStatus?.Status;
            result.SupInsuranceLifePlanTitle= lifeStatus?.PlanTitle;
        } catch ( Exception exception ) {
            var message = "اطلاعات بیمه عمر در دسترس نمی باشد.";
            _logger.LogError(exception, message);
        }

        try {
            var taminInsurance = await _studentRepository.GetTaminInsuranceByCodm(request.Codm);
            result.TaminInsuranceStatus = taminInsurance?.Status;
            result.TaminInsuranceNumber = taminInsurance?.TaminNumber;
            result.TaminInsuranceDescription = taminInsurance?.Description;
        } catch ( Exception exception ) {
            var message = "اطلاعات بیمه تامین اجتماعی در دسترس نمی باشد.";
            result.TaminInsuranceDescription = message;
            _logger.LogError(exception, message);
        }

        return result;
    }
}
