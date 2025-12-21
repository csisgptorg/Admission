using Csis.Authorization.Services;

namespace Csis.Admission.Application.Features.CaseFilings.Commands;

/// <summary>
/// مرحله آخر ایجاد کاربر 
/// </summary>
/// <param name="Codm"></param>
/// <param name="Password"></param>
public sealed record CreateAdmissionCaseStepCreateUserCommand(int Codm, string Password) : IRequest<int>;

internal sealed class CreateAdmissionCaseStepCreateUserCommandHandler(ICsisUsersService csisUsersService, IRepository<StudentSummary> repository,ILogger<CreateAdmissionCaseStepCreateUserCommandHandler> logger)
    : IRequestHandler<CreateAdmissionCaseStepCreateUserCommand, int>
{
    public async Task<int> Handle(CreateAdmissionCaseStepCreateUserCommand request,
        CancellationToken cancellationToken) {

        if (! await repository.ExistsAsync(x => x.Codm == request.Codm, cancellationToken: cancellationToken) ) {
            throw new CommandValidationException("طلبه ای با این کد مرکز یافت نشد");
        }

        var resultIds = await csisUsersService.CreateStudentUserAsync(request.Codm, request.Password);

        if ( !resultIds.Succeeded ) {
            throw new CommandValidationException(resultIds.ErrorMessage);
        }

        logger.LogInformation( $"User created for student with Codm: {request.Codm}, UserId: {resultIds.ToJson()}" );

        return request.Codm;
    }
}
