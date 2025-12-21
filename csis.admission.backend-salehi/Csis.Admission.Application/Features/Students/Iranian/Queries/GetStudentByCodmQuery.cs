using Csis.Authorization.Services;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Admission.Application.Features.Students.Commands;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.Students.Queries;

/// <inheritdoc/>
public sealed record GetStudentByCodmQuery(int Codm) : IRequest<StudentDto>;

internal sealed class GetStudentByCodmQueryHandler : IRequestHandler<GetStudentByCodmQuery, StudentDto>
{
    private readonly IMediator _mediator;
    private readonly IStudentRepository _studentRepo;
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    public GetStudentByCodmQueryHandler(IStudentRepository studentRepo, ICsisAuthenticatedUserService authenticatedUserService, IMediator mediator) {
        _studentRepo = studentRepo;
        _authenticatedUserService = authenticatedUserService;
        _mediator = mediator;
    }

    public async Task<StudentDto> Handle(GetStudentByCodmQuery request, CancellationToken cancellationToken) {

        var student = await _studentRepo.GetByCodm(request.Codm) ?? throw new CommandValidationException($"طلبه ای با کد '{request.Codm}' یافت نشد.");
        await _mediator.Send(new CreateEmployeeViewStudentLogCommand(request.Codm));

        var accessFemaleIdentityInfo = await _authenticatedUserService.IsAuthorizedToAsync(PermissionsEnum.FemaleInfoIdentity);
        if ( student.Gender == Gender.Female && accessFemaleIdentityInfo != true){
            throw new UnauthorizedActionException(PermissionsEnum.FemaleInfoIdentity.GetDescription());
        }

        return student;
    }
}
