using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Authorization.Services;
using Csis.CompareImageAi.Dtos;

namespace Csis.Admission.Application.Features.Students.Commands;

/// <inheritdoc/>
public sealed record UpdateStudentProfilePictureCommand(
    int Codm,
    ImageAnalysisResultDto ImageAnalysisResultDto,
    Guid FileId,
    long RequestId) : IRequest<long>;

internal sealed class UpdateStudentProfilePictureCommandHandler(
    IStudentRepository repo,
    IRepository<Request, long> requestRepository, ICsisAuthenticatedUserService authenticatedUserService)
    : IRequestHandler<UpdateStudentProfilePictureCommand, long>
{
    public async Task<long> Handle(UpdateStudentProfilePictureCommand request, CancellationToken cancellationToken) {

        var file = await repo.GetTempProfilePicture(request.FileId);
        var requestCommand = await requestRepository.GetByIdAsTrackingAsync(request.RequestId, cancellationToken: cancellationToken);

        var command = new UpdateStudentProfilePicturePrc {
            Codm = request.Codm,
            Picture = file,
            RequestId = request.RequestId,
            PersonnelId = null,
            UserId = 0,
            //TODO: اصلاح شود
            DataSource = requestCommand?.Source != null ?  DataSource.Employee : DataSource.Student,
        };
    var result = await repo.UpdateProfilePictureCommand(command);
        return result.Id;
    }
}
