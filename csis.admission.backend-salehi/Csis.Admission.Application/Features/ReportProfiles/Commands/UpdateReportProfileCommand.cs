/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Enums;

namespace Csis.Admission.Application.Features.ReportProfiles.Commands;

/// <summary>
/// ویرایش پروفایل گزارش
/// </summary>
/// <param name="Id">شناسه رکورد موردا ویرایش</param>
/// <param name="Title">عنوان جدید</param>
/// <param name="Description">توضیحات جدید</param>
/// <param name="Structure">ساختار گزارش</param>
public sealed record UpdateReportProfileCommand(
    int Id,
    string Title,
    string Description,
    ReportProfileStructure Structure) : IRequest;

internal sealed class UpdateReportProfileCommandHandler(IRepository<ReportProfile> repo, ICurrentUserService currentUserService) : IRequestHandler<UpdateReportProfileCommand>
{
    public async Task Handle(UpdateReportProfileCommand request, CancellationToken cancellationToken) {
        var entity = await repo.GetByIdAsTrackingAsync(request.Id, cancellationToken: cancellationToken)
            ?? throw new RecordNotFoundException<ReportProfile>(request.Id);
        var userId = (await currentUserService.GetUserIdAsync()).Value;

        if ( entity.ProfileType == ReportProfileType.Private && entity.CreatedById != userId ) {
            throw new UnauthorizedActionException("Can't update private report profiles created by other users.");
        }

        if ( entity.ProfileType == ReportProfileType.Public && entity.CreatedById != userId &&
            !await currentUserService.IsAuthorizedAsync(PermissionsEnum.EditAllPublicProfiles) ) {
            throw new UnauthorizedActionException("Can't update public profiles created by other users without required permission.");
        }

        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.Structure = request.Structure;

        await repo.UpdateAsync(entity, cancellationToken: cancellationToken);
    }
}
