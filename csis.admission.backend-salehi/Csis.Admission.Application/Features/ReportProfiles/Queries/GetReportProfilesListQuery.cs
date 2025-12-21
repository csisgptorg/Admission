/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.ReportProfiles.Dtos;

namespace Csis.Admission.Application.Features.ReportProfiles.Queries;

/// <summary>
/// دریافت لیست پروفایل های گزارش ذخیره شده
/// </summary>
/// <param name="ReportType">نوع گزارش</param>
public sealed record GetReportProfilesListQuery(ReportProfileType ReportType) : IRequest<List<ReportProfileDto>>;

internal sealed class GetReportProfilesListQueryHandler(
    IRepository<ReportProfile> repo,
    ICurrentUserService currentUserService,
    IPersonInfoService personInfoService) : IRequestHandler<GetReportProfilesListQuery, List<ReportProfileDto>>
{
    public async Task<List<ReportProfileDto>> Handle(GetReportProfilesListQuery request, CancellationToken cancellationToken) {
        var userId = (await currentUserService.GetUserIdAsync()).Value;
        var canViewPublics = await currentUserService.IsAuthorizedAsync(PermissionsEnum.ViewPublicReportProfiles);

        List<ReportProfileDto> profiles = [];

        profiles.AddRange(await repo.GetAllAsync<ReportProfileDto>(x => x.ReportType == request.ReportType &&
            x.ProfileType == ReportProfileType.Private &&
            x.CreatedById == userId, cancellationToken: cancellationToken));

        if ( canViewPublics ) {
            profiles.AddRange(await repo.GetAllAsync<ReportProfileDto>(x => x.ReportType == request.ReportType &&
                x.ProfileType == ReportProfileType.Public, cancellationToken: cancellationToken));
        }

        return await personInfoService.FillUserInfoAsync(profiles, [(x => x.CreatedById, x => x.CreatedBy)]);
    }
}
