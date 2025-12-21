using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.UniversityEducations.Commands;

/// <summary>
/// درخواست حذف تحصیلات دانشگاهی تکفل
/// </summary>
/// <param name="Codm"></param>
/// <param name="DependentId"></param>
/// <param name="EducationId"></param>
public sealed record class DeleteDependentUniversityEducationRequestCommand(int EducationId) : IRequest;
internal sealed class DeleteDependentUniversityEducationRequestCommandHandler(
    ICurrentUserService currentUser,
    IRepository<UniversityEducation> educationRepo,
    IRequestService requestService
   )
    : IRequestHandler<DeleteDependentUniversityEducationRequestCommand>
{
    public async Task Handle(DeleteDependentUniversityEducationRequestCommand command, CancellationToken cancellationToken) {

        if ( !await currentUser.IsSenior() ) {
            throw new CommandValidationException("شما مجوز لازم برای حذف تحصیلات دانشگاهی را ندارید.");
        }

        var education = await educationRepo.GetByIdAsync(command.EducationId, cancellationToken: cancellationToken)
            ?? throw new CommandValidationException("تحصیلات دانشگاهی تکفل مورد نظر یافت نشد.");

        var requestCommand = new CreateRequestCommand(new DeleteDependentUniversityEducationCommand(education.Codm, education.DependentId.Value, command.EducationId), RequestFlow.DirectRegistration, RequestType.DeleteDependentUniversityEducation);
        await requestService.Create(requestCommand, cancellationToken);
    }
}
