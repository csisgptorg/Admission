using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.NonIranianStudent.Commands;

/// <inheritdoc/>
public sealed record UpdateNonIranianDependentMarriageCommand : IRequest<long>
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public string MarriageDate { get; init; }

    /// <inheritdoc/>
    public long? DependentId { get; init; }
}

internal sealed class CreateNonIranianDependentMarriageCommandHandler(
    IStudentDependentRepository studentDependentRepository)
    : IRequestHandler<UpdateNonIranianDependentMarriageCommand, long>
{
    public async Task<long> Handle(UpdateNonIranianDependentMarriageCommand request, CancellationToken cancellationToken) {
        var dependentDivorce = new UpdateDependentMarriagePrcRequest() {
            Codm = request.Codm,
            DependentId = request.DependentId.Value,
            MarriageDate = request.MarriageDate.StringDateToInt().Value
        };
        var result = await studentDependentRepository.UpdateDependentChildMarriageAsync(dependentDivorce);

        return result.Id;
    }

}
