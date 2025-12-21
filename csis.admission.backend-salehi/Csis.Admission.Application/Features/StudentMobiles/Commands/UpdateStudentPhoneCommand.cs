using Microsoft.AspNetCore.Http;
using Csis.Authorization.Services;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Interfaces.Repositories;

namespace Csis.Admission.Application.Features.StudentMobiles.Commands;

/// <summary>بروز رسانی موبایل طلبه</summary>
public sealed record UpdateStudentPhoneCommand(int? Codm, string? Mobile, string? PreCodeTel, string? Tel, string? Otp) : IRequest<long>;

internal sealed class UpdateStudentPhoneCommandHandler(
    IStudentMobileRepository repo,
    IHttpContextAccessor contextAccessor,
    ICsisAuthenticatedUserService authenticatedUser
    ) : IRequestHandler<UpdateStudentPhoneCommand, long>
{
    public async Task<long> Handle(UpdateStudentPhoneCommand command, CancellationToken cancellationToken) {
        var repoCommand = new UpdateStudentPhoneRepoCommand(command.Codm.Value, command.Mobile, command.PreCodeTel, command.Tel);
        await Common.Utilities.SetLogParam(repoCommand, authenticatedUser, contextAccessor);
        var result = await repo.Update(repoCommand);

        return result.Id;
    }
}
