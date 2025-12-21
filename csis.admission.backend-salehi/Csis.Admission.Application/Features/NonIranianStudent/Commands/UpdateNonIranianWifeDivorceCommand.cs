using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.NonIranianStudent.Commands;

/// <inheritdoc/>
public sealed record UpdateNonIranianWifeDivorceCommand : IRequest<long>
{
    /// <inheritdoc/>
    public int Codm { get; set; }
    /// <inheritdoc/>
    public string DivorceDate { get; init; }

    /// <inheritdoc/>
    public long? DependentId { get; init; }
}

internal sealed class UpdateNonIranianWifeDivorceCommandHandler(
    IStudentDependentRepository studentDependentRepository)
    : IRequestHandler<UpdateNonIranianWifeDivorceCommand, long>
{
    public async Task<long> Handle(UpdateNonIranianWifeDivorceCommand request, CancellationToken cancellationToken) {
        var dependentDivorce = new SetDependentDivorceModel {
            Codm = request.Codm,
            DependentId = request.DependentId.Value,
            DivorceDate = request.DivorceDate.StringDateToInt().Value,
            DataSource = DataSource.Student
        };
        var result = await studentDependentRepository.UpdateDependentSpouseDivorceAsync(dependentDivorce);
        return result.Id;
    }
}

