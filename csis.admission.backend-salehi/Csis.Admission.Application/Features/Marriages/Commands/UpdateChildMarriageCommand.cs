using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.Marriages.Commands;

/// <summary>
/// ثبت ازدواج تکفل
/// </summary>
public sealed record UpdateChildMarriageCommand : IRequest<long>
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public long DependentId { get; init; }

    /// <inheritdoc/>
    public string MarriageDate { get; init; }
}

internal sealed class UpdateChildMarriageCommandHandler(
    IStudentDependentRepository studentDependentRepository,
    ICurrentUserService currentUserService)
    : IRequestHandler<UpdateChildMarriageCommand, long>
{
    public async Task<long> Handle(UpdateChildMarriageCommand command, CancellationToken cancellationToken) {

        var marriageRequest = new UpdateDependentMarriagePrcRequest {
            Codm = command.Codm,
            DependentId = command.DependentId,
            MarriageDate = command.MarriageDate.StringDateToInt().Value,
            UserId = 1,
            DataSource = await currentUserService.GetUserIdAsync() != null ? DataSource.Employee : DataSource.Student
        };

        var result = await studentDependentRepository.UpdateDependentChildMarriageAsync(marriageRequest);
        return result.Id;
    }
}
