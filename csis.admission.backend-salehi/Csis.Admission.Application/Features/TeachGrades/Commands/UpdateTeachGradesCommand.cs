using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.TeachGrades.Commands;

/// <summary>
/// UpdateTeachGradeCommand
/// </summary>
public sealed record UpdateTeachGradeCommand : BaseCommandDto<UpdateTeachGradeCommand, TeachGrade>, IRequest
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// مرکز حوزوی
    /// </summary>
    public ApprovalCenter ApprovalCenter { get; set; }

    /// <summary>
    /// رتبه
    /// </summary>
    public short Grade { get; set; }

    /// <summary>
    /// تاریخ ثبت
    /// </summary>
    public string RegisterDate { get; set; }

    /// <summary>
    /// تاریخ اعتبار
    /// </summary>
    public string ExpirationDate { get; set; }

    /// <summary>
    /// CustomMappings
    /// </summary>
    /// <param name="mapping"></param>
    public override void ReverseCustomMappings(IMappingExpression<UpdateTeachGradeCommand, TeachGrade> mapping) {
        mapping.ForMember(model => model.RegisterDate, config => config.MapFrom(dto => dto.RegisterDate.StringDateToInt()));
        mapping.ForMember(model => model.ExpirationDate, config => config.MapFrom(dto => dto.ExpirationDate.StringDateToInt()));
    }
}

internal sealed class UpdateTeachGradeCommandHandler : IRequestHandler<UpdateTeachGradeCommand>
{
    private readonly IRepository<TeachGrade> _reachGradeRepo;
    public UpdateTeachGradeCommandHandler(IRepository<TeachGrade> reachGradeRepo) {
        _reachGradeRepo = reachGradeRepo;
    }

    public async Task Handle(UpdateTeachGradeCommand request, CancellationToken cancellationToken) {
        var reachGrade = await _reachGradeRepo.GetByIdAsTrackingAsync(request.Id, cancellationToken: cancellationToken)
            ?? throw new RecordNotFoundException<TeachGrade>(request.Id);

        request.ToEntity(reachGrade);
        await _reachGradeRepo.UpdateAsync(reachGrade, true,cancellationToken);
    }
}
