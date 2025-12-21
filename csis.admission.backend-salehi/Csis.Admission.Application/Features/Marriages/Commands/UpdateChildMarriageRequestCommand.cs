using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Marriages.Commands;

/// <summary>
/// ثبت ازدواج تکفل
/// </summary>
public sealed record UpdateChildMarriageRequestCommand : IRequest
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public long DependentId { get; init; }

    /// <inheritdoc/>
    public string MarriageDate { get; init; }
}

internal sealed class UpdateChildMarriageRequestCommandHandler(
    IRepository<DependentSummary, long> dependentInfoRepo,
    IRepository<StudentSummary, int> studentInfoRepo,
    ICurrentUserService currentUser,
    IRequestService requestService)
    : IRequestHandler<UpdateChildMarriageRequestCommand>
{
    public async Task Handle(UpdateChildMarriageRequestCommand command, CancellationToken cancellationToken) {
        _ = await Common.Utilities.SetCodm(command, currentUser);
        var student = await studentInfoRepo.GetOneAsync(x => x.Codm == command.Codm, cancellationToken: cancellationToken);

        var dependent = await dependentInfoRepo.GetOneAsync(x => x.Id == command.DependentId && x.Codm == command.Codm, cancellationToken: cancellationToken)
            ?? throw new CommandValidationException("کد مرکز خدمات سرپرست یا شناسه تکفل نادرست است.");

        if ( dependent.IsActive == false ) {
            throw new CommandValidationException("پرونده این تکفل بسته می باشد.");
        }
        if ( dependent.IsMarried ) {
            throw new CommandValidationException("این تکفل قبلا ازدواج کرده است.");
        }

        if ( dependent.Relation == DependentRelation.Child
            || dependent.Relation == DependentRelation.AdoptedChild
            || dependent.Relation == DependentRelation.Grandchild ) {

            var requestCommand = new CreateRequestCommand(command, RequestFlow.DirectRegistration);
            await requestService.Create(requestCommand, cancellationToken);

        } else {
            throw new CommandValidationException("شما مجاز به انجام این کار نیستید.");
        }
    }
}
