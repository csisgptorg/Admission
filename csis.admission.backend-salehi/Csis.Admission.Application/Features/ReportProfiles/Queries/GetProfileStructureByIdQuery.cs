/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Enums;

namespace Csis.Admission.Application.Features.ReportProfiles.Queries;

/// <summary>
/// فراخوانی ساختار پروفایل گزارش
/// </summary>
/// <param name="Id">شناسه پروفایل</param>
public sealed record GetProfileStructureByIdQuery(int Id) : IRequest<ReportProfileStructure>;

internal sealed class GetProfileStructureByIdQueryHandler(IRepository<ReportProfile> repo, ICurrentUserService currentUserService) : IRequestHandler<GetProfileStructureByIdQuery, ReportProfileStructure>
{
    public async Task<ReportProfileStructure> Handle(GetProfileStructureByIdQuery request, CancellationToken cancellationToken) {
        var profile = await repo.GetByIdAsTrackingAsync(request.Id, cancellationToken: cancellationToken)
            ?? throw new RecordNotFoundException<ReportProfile>(request.Id);

        if ( profile.ProfileType == ReportProfileType.Private ) {
            var userId = (await currentUserService.GetUserIdAsync()).Value;

            if ( profile.CreatedById != userId ) {
                throw new UnauthorizedActionException("Can't access private report profile.");
            }

            return profile.Structure;
        } else if ( profile.ProfileType == ReportProfileType.Public ) {
            if ( !await currentUserService.IsAuthorizedAsync(PermissionsEnum.ViewPublicReportProfiles) ) {
                throw new UnauthorizedActionException("Can't access public report profile.");
            }

            return profile.Structure;
        }

        return null;
    }
}
