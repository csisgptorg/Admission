using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Authorization.Services;

namespace Csis.Admission.Application.Features.Students.Commands;

/// <summary>
/// بروزرسانی عکس پروفایل از ثبت احوال (پس از تایید ادمین)
/// </summary>
public sealed record UpdateStudentProfilePictureFromCivilRegistryCommand(
    int Codm,
    Guid NewImageFileId,
    Guid? OldImageFileId,
    long RequestId) : IRequest<long>;

internal sealed class UpdateStudentProfilePictureFromCivilRegistryCommandHandler(
    IStudentRepository repo,
    ICurrentUserService currentUserService)
    : IRequestHandler<UpdateStudentProfilePictureFromCivilRegistryCommand, long>
{
    public async Task<long> Handle(UpdateStudentProfilePictureFromCivilRegistryCommand request, CancellationToken cancellationToken) {
        var file = await repo.GetTempProfilePicture(request.NewImageFileId);

        var command = new UpdateStudentProfilePicturePrc {
            Codm = request.Codm,
            Picture = file,
            RequestId = request.RequestId,
            PersonnelId = await currentUserService.PersonnelId() ?? 0,
            UserId = await currentUserService.GetUserIdAsync() ?? 0,
            DataSource = DataSource.Employee,
        };

        var result = await repo.UpdateProfilePictureCommand(command);
        return result.Id;
    }
}
