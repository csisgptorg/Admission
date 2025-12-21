/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Enums;

namespace Csis.Admission.Application.Features.ReportProfiles.Commands;

/// <summary>
/// ایجاد پروفایل گزارش
/// </summary>
/// <param name="Title">عنوان پروفایل</param>
/// <param name="Description">توضیحات</param>
/// <param name="ReportType">نوع گزارش</param>
/// <param name="ProfileType">نوع پروفایل</param>
/// <param name="Structure">ساختار گزارش</param>
public sealed record CreateReportProfileCommand(
    string Title,
    string Description,
    ReportProfileType ReportType,
    ReportProfileType ProfileType,
    ReportProfileStructure Structure) : IRequest<int>;

internal sealed class CreateReportProfileCommandHandler(IRepository<ReportProfile> repo, ICurrentUserService currentUserService) : IRequestHandler<CreateReportProfileCommand, int>
{
    public async Task<int> Handle(CreateReportProfileCommand request, CancellationToken cancellationToken) {
        if ( request.ProfileType == ReportProfileType.Private &&
            !await currentUserService.IsAuthorizedAsync(PermissionsEnum.CreatePublicReportProfile) ) {
            throw new CommandValidationException("امکان ایجاد پروفایل گزارش خصوصی برای شما وجود ندارد");
        }

        if ( request.ProfileType == ReportProfileType.Public &&
            !await currentUserService.IsAuthorizedAsync(PermissionsEnum.CreatePublicReportProfile) ) {
            throw new CommandValidationException("امکان ایجاد پروفایل گزارش عمومی برای شما وجود ندارد");
        }

        var entity = new ReportProfile {
            Title = request.Title,
            Description = request.Description,
            ReportType = request.ReportType,
            ProfileType = request.ProfileType,
            Structure = request.Structure
        };

        await repo.InsertAsync(entity, cancellationToken: cancellationToken);

        return entity.Id;
    }
}
