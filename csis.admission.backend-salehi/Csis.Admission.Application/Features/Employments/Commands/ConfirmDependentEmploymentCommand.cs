using Csis.Authorization.Services;

namespace Csis.Admission.Application.Features.Employments.Commands;

/// <summary>تایید وضعیت اشتغال</summary>
public record ConfirmDependentEmploymentCommand(long DependentId) : IRequest;

internal sealed class ConfirmDependentEmploymentCommandHandler(IRepository<DependentEmployment> repo, ICsisAuthenticatedUserService authenticatedUser)
    : IRequestHandler<ConfirmDependentEmploymentCommand>
{
    public async Task Handle(ConfirmDependentEmploymentCommand command, CancellationToken cancellationToken) {
        var codm = int.Parse(await authenticatedUser.GetStudentCodmAsync());
        var employment = await repo.GetOneAsTrackingAsync(x => x.Codm == codm && x.DependentId==command.DependentId, cancellationToken: cancellationToken);
        await UpdateOn(employment, authenticatedUser);
        await repo.UpdateAsync(employment, cancellationToken: cancellationToken);
    }

    private async Task UpdateOn(DependentEmployment employment, ICsisAuthenticatedUserService authenticatedUser) {
        var userId = await authenticatedUser.GetUserIdAsync();
        var delegatedUserId = await authenticatedUser.GetDelegatedUserIdAsync();
        employment.Update(userId, delegatedUserId, DateTime.Now);
    }
}
