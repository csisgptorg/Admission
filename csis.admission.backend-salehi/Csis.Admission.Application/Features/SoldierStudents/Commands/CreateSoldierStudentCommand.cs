using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.SoldierStudents.Commands;

/// <inheritdoc/>
public sealed record CreateSoldierStudentCommand : BaseCommandDto<CreateSoldierStudentCommand, SoldierStudent>, IRequest<int>
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public string StartDate { get; set; }

    /// <inheritdoc/>
    public string EndDate { get; set; }

    /// <inheritdoc/>
    public string Place { get; set; }

    /// <inheritdoc/>
    public override void ReverseCustomMappings(IMappingExpression<CreateSoldierStudentCommand, SoldierStudent> mapping) {
        mapping.ForMember(model => model.StartDate, config => config.MapFrom(dto => dto.StartDate.StringDateToInt()));
        mapping.ForMember(model => model.EndDate, config => config.MapFrom(dto => dto.EndDate.StringDateToInt()));
    }
}

internal sealed class CreateSoldierStudentCommandHandler : IRequestHandler<CreateSoldierStudentCommand, int>
{
    private readonly IRepository<SoldierStudent> _repo;
    public CreateSoldierStudentCommandHandler(IRepository<SoldierStudent> repo) {
        _repo = repo;
    }

    public async Task<int> Handle(CreateSoldierStudentCommand request, CancellationToken cancellationToken) {
        var entity = request.ToEntity();
        await _repo.InsertAsync(entity, cancellationToken: cancellationToken);
        return entity.Id;
    }
}
