using Csis.Authorization.Services;

namespace Csis.Admission.Application.Features.Employments.Commands;

/// <summary>تایید وضعیت اشتغال</summary>
public record ConfirmStudentEmploymentCommand : IRequest;

internal sealed class ConfirmStudentEmploymentCommandHandler(IRepository<StudentEmployment> repo, ICsisAuthenticatedUserService authenticatedUser)
    : IRequestHandler<ConfirmStudentEmploymentCommand>
{
    public async Task Handle(ConfirmStudentEmploymentCommand command, CancellationToken cancellationToken) {
        var codm =int.Parse(await authenticatedUser.GetStudentCodmAsync());
        var employment = await repo.GetOneAsTrackingAsync(x => x.Codm == codm, cancellationToken: cancellationToken);
        await UpdateOn(employment,authenticatedUser);
        await repo.UpdateAsync(employment, cancellationToken: cancellationToken);
    }

    private async Task UpdateOn(StudentEmployment employment,ICsisAuthenticatedUserService authenticatedUser) {
        var userId = await authenticatedUser.GetUserIdAsync();
        var delegatedUserId = await authenticatedUser.GetDelegatedUserIdAsync();
        employment.Update(userId,delegatedUserId,DateTime.Now);
    }
}
