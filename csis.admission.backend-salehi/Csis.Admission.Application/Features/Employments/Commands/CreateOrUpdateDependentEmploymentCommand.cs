using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Employments.Commands;

/// <inheritdoc/>
public record CreateOrUpdateDependentEmploymentCommand : BaseCommandDto<CreateOrUpdateDependentEmploymentCommand, DependentEmployment>, IRequest<int>
{
 /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
 public long DependentId { get; set; }

    /// <inheritdoc/>
    public bool? IsEmployee { get; set; }

    /// <inheritdoc/>
    public string EmployeeName { get; set; }

    /// <inheritdoc/>
    public string EmployeeAddress { get; set; }

    /// <summary>‘‰«”Â œ—ŒÊ«” </summary>
    public long? RequestId { get; set; }
}

internal sealed class UpdateDependentEmploymentCommandHandler(IRepository<DependentEmployment> repo)
  : IRequestHandler<CreateOrUpdateDependentEmploymentCommand, int>
{
    public async Task<int> Handle(CreateOrUpdateDependentEmploymentCommand command, CancellationToken cancellationToken) {
        var employment = await repo.GetOneAsTrackingAsync(x => x.DependentId == command.DependentId, cancellationToken: cancellationToken);

    if ( employment is null ) {
      var newEmployment = command.ToEntity();
            await repo.InsertAsync(newEmployment, cancellationToken: cancellationToken);
            return newEmployment.Id;
        }

        await repo.UpdateAsync(command.ToEntity(employment), cancellationToken: cancellationToken);
        return employment.Id;
    }
}
