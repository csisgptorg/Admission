using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.Divorce.Commands;

/// <inheritdoc/>
public sealed record UpdateWifeDivorceCommand : IRequest<long>
{
    /// <inheritdoc/>
    public int Codm { get; set; }
    /// <inheritdoc/>
    public string DivorceDate { get; init; }

    /// <inheritdoc/>
    public long? DependentId { get; init; }
}

internal sealed class UpdateWifeDivorceCommandHandler(
    IStudentDependentRepository studentDependentRepository)
    : IRequestHandler<UpdateWifeDivorceCommand, long>
{
    public async Task<long> Handle(UpdateWifeDivorceCommand request, CancellationToken cancellationToken) {
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
