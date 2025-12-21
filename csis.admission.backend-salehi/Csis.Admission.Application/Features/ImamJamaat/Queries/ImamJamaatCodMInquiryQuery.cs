using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.Students.Commands;
using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Authorization.Services;

namespace Csis.Admission.Application.Features.ImamJamaat.Queries;
/// <summary>
/// استعلام اطلاعات طلبه با کد مرکز
/// </summary>
/// <param name="Codm"></param>
public sealed record ImamJamaatCodMInquiryQuery(int Codm) : IRequest<StudentDto>;

internal sealed class CodMInquiryQueryHandler : IRequestHandler<ImamJamaatCodMInquiryQuery, StudentDto>
{
    private readonly IStudentRepository _studentRepository;
    private readonly ILogger<CodMInquiryQueryHandler> _logger;
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    private readonly IMediator _mediator;

    public CodMInquiryQueryHandler(
        IStudentRepository studentRepository,
        ILogger<CodMInquiryQueryHandler> logger,
        ICsisAuthenticatedUserService authenticatedUserService,
        IMediator mediator) {
        _studentRepository = studentRepository;
        _logger = logger;
        _authenticatedUserService = authenticatedUserService;
        _mediator = mediator;
    }
    public async Task<StudentDto> Handle(ImamJamaatCodMInquiryQuery request, CancellationToken cancellationToken) {
        var student = await _studentRepository.GetByCodm(request.Codm) ?? throw new CommandValidationException($"طلبه ای با کد '{request.Codm}' یافت نشد.");
        await _mediator.Send(new CreateEmployeeViewStudentLogCommand(request.Codm), cancellationToken);

        var accessFemaleIdentityInfo = await _authenticatedUserService.IsAuthorizedToAsync(PermissionsEnum.FemaleInfoIdentity);
        if ( student.Gender == Gender.Female && accessFemaleIdentityInfo != true ) {
            throw new UnauthorizedActionException(PermissionsEnum.FemaleInfoIdentity.GetDescription());
        }

        return student;
    }
}
