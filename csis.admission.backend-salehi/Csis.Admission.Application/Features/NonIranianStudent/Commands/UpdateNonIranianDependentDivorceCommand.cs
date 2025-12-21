using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.DependentCaseActive.Commands;
using MediatR;

namespace Csis.Admission.Application.Features.NonIranianStudent.Commands;

/// <inheritdoc/>
public sealed record UpdateNonIranianDependentDivorceCommand : IRequest<long>
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public string DivorceDate { get; init; }

    /// <inheritdoc/>
    public long? DependentId { get; init; }
}

internal sealed class CreateNonIranianDependentDivorceCommandHandler(
    IMediator mediator,
    IStudentDependentRepository studentDependentRepository)
    : IRequestHandler<UpdateNonIranianDependentDivorceCommand,long>
{
    public async Task<long> Handle(UpdateNonIranianDependentDivorceCommand request, CancellationToken cancellationToken) {
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

