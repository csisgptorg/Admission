using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.SoldierStudents.Commands;

/// <inheritdoc/>
public sealed record UpdateSoldierStudentCommand : BaseCommandDto<UpdateSoldierStudentCommand, SoldierStudent>, IRequest
{
    /// <inheritdoc/>
    public int Id { get; init; }

    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public string StartDate { get; set; }

    /// <inheritdoc/>
    public string EndDate { get; set; }

    /// <inheritdoc/>
    public string Place { get; set; }

    /// <inheritdoc/>
    public override void ReverseCustomMappings(IMappingExpression<UpdateSoldierStudentCommand, SoldierStudent> mapping) {
        mapping.ForMember(model => model.StartDate, config => config.MapFrom(dto => dto.StartDate.StringDateToInt()));
        mapping.ForMember(model => model.EndDate, config => config.MapFrom(dto => dto.EndDate.StringDateToInt()));
    }
}

internal sealed class UpdateSoldierStudentCommandHandler : IRequestHandler<UpdateSoldierStudentCommand>
{
    private readonly IRepository<SoldierStudent> _repo;
    public UpdateSoldierStudentCommandHandler(IRepository<SoldierStudent> repo) {
        _repo = repo;
    }

    public async Task Handle(UpdateSoldierStudentCommand request, CancellationToken cancellationToken) {

        var entity = await _repo.GetOneAsTrackingAsync(x=>x.Id == request.Id && x.Codm == request.Codm, false, cancellationToken) 
            ?? throw new RecordNotFoundException<SoldierStudent>(request.Id);
        var entityCodm = entity.Codm;

        request.ToEntity(entity);
        entity.Codm=entityCodm;
        await _repo.UpdateAsync(entity, true,cancellationToken);
    }
}
