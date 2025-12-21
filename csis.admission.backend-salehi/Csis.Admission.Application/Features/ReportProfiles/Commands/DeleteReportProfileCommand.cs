/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Enums;

namespace Csis.Admission.Application.Features.ReportProfiles.Commands;

/// <summary>
/// حذف پروفایل گزارش
/// </summary>
/// <param name="Id">شناسه پروفایل</param>
public sealed record DeleteReportProfileCommand(int Id) : IRequest;

internal sealed class DeleteReportProfileCommandHandler(IRepository<ReportProfile> repo, ICurrentUserService currentUserService) : IRequestHandler<DeleteReportProfileCommand>
{
    public async Task Handle(DeleteReportProfileCommand request, CancellationToken cancellationToken) {
        var entity = await repo.GetByIdAsTrackingAsync(request.Id, cancellationToken: cancellationToken)
            ?? throw new RecordNotFoundException<ReportProfile>(request.Id);
        var userId = (await currentUserService.GetUserIdAsync()).Value;

        if ( entity.ProfileType == ReportProfileType.Private && entity.CreatedById != userId ) {
            throw new UnauthorizedActionException("Can't delete private report profiles created by other users.");
        }

        if ( entity.ProfileType == ReportProfileType.Public && entity.CreatedById != userId &&
            !await currentUserService.IsAuthorizedAsync(PermissionsEnum.EditAllPublicProfiles) ) {
            throw new UnauthorizedActionException("Can't delete public profiles created by other users without required permission.");
        }

        await repo.DeleteAsync(entity, cancellationToken: cancellationToken);
    }
}
