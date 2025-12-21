using Microsoft.AspNetCore.Http;
using Csis.Authorization.Services;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Interfaces.Repositories;

namespace Csis.Admission.Application.Features.StudentMobiles.Commands;

/// <summary>بروز رسانی موبایل تکفل</summary>
public sealed record UpdateDependentMobileCommand(int? Codm, long DependentId, string Mobile, string Otp) : IRequest<long>;

internal sealed class UpdateDependentMobileCommandHandler(
    IStudentMobileRepository repo,
    IHttpContextAccessor contextAccessor,
    ICsisAuthenticatedUserService authenticatedUser
    ) : IRequestHandler<UpdateDependentMobileCommand,long>
{
    public async Task<long> Handle(UpdateDependentMobileCommand command, CancellationToken cancellationToken) {
        var repoCommand = new UpdateDependentMobileRepoCommand(command.DependentId, command.Codm.Value, command.Mobile);
        await Common.Utilities.SetLogParam(repoCommand, authenticatedUser, contextAccessor);
       var result = await repo.UpdateDependent(repoCommand);
        return result.Id;
    }
}
