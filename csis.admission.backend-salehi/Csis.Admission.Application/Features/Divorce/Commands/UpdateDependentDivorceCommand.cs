using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.DependentCaseActive.Commands;
using Csis.Admission.Domain.Entities;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Application.Features.Divorce.Commands;

/// <inheritdoc/>
public sealed record UpdateDependentDivorceCommand : IRequest<long>
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public string DivorceDate { get; init; }

    /// <inheritdoc/>
    public long? DependentId { get; init; }

    /// <inheritdoc/>
    public string DependentNationalCode { get; init; }

    /// <inheritdoc/>
    public string DependentBirthDate { get; init; }
}

internal sealed class CreateDependentDivorceCommandHandler(
    IMediator mediator,
    IStudentDependentRepository studentDependentRepository)
    : IRequestHandler<UpdateDependentDivorceCommand, long>
{
    public async Task<long> Handle(UpdateDependentDivorceCommand request, CancellationToken cancellationToken) {
        var dependentDivorce = new SetDependentDivorceModel {
            Codm = request.Codm,
            DependentId = request.DependentId.Value,
            DivorceDate = request.DivorceDate.StringDateToInt().Value
        };
        var result = await studentDependentRepository.UpdateDependentChildDivorceAsync(dependentDivorce);

        // ایجاد درخواست برای ثبت اتوماتیک باز شدن پرونده تکفل
        await mediator.Send(new AutomaticOpenDependentCaseRequestCommand(request.Codm, request.DependentId.Value), cancellationToken);

        return result.Id;
    }

}
