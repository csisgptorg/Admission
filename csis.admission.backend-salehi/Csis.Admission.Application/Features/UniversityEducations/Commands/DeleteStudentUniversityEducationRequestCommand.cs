using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.UniversityEducations.Commands;

/// <summary>
/// درخواست حذف تحصیلات دانشگاهی
/// </summary>
/// <param name="Codm"></param>
/// <param name="EducationId"></param>
public sealed record class DeleteStudentUniversityEducationRequestCommand(int EducationId) : IRequest;
internal sealed class DeleteStudentUniversityEducationRequestCommandHandler(
    ICurrentUserService currentUser,
    IRepository<UniversityEducation> educationRepository,
    IRequestService requestService
   )
    : IRequestHandler<DeleteStudentUniversityEducationRequestCommand>
{
    public async Task Handle(DeleteStudentUniversityEducationRequestCommand command, CancellationToken cancellationToken) {

        if ( !await currentUser.IsSenior() ) {
            throw new CommandValidationException("شما مجوز لازم برای حذف تحصیلات دانشگاهی را ندارید.");
        }

        var education = await educationRepository.GetByIdAsync(command.EducationId, cancellationToken: cancellationToken)
            ?? throw new CommandValidationException("تحصیلات دانشگاهی مورد نظر یافت نشد.");

        var requestCommand = new CreateRequestCommand(new DeleteStudentUniversityEducationCommand(education.Codm, command.EducationId), RequestFlow.DirectRegistration, RequestType.DeleteStudentUniversityEducation);
        await requestService.Create(requestCommand, cancellationToken);
    }
}
