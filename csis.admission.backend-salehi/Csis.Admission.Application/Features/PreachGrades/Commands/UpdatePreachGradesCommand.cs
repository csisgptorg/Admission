using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.PreachGrades.Commands;

/// <summary>
/// UpdatePreachGradeCommand
/// </summary>
public sealed record UpdatePreachGradeCommand : BaseCommandDto<UpdatePreachGradeCommand, PreachGrade>, IRequest
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
    public override void ReverseCustomMappings(IMappingExpression<UpdatePreachGradeCommand, PreachGrade> mapping) {
        mapping.ForMember(model => model.RegisterDate, config => config.MapFrom(dto => dto.RegisterDate.StringDateToInt()));
        mapping.ForMember(model => model.ExpirationDate, config => config.MapFrom(dto => dto.ExpirationDate.StringDateToInt()));
    }
}

internal sealed class UpdatePreachGradeCommandHandler : IRequestHandler<UpdatePreachGradeCommand>
{
    private readonly IRepository<PreachGrade> _preachGradeRepo;
    public UpdatePreachGradeCommandHandler(IRepository<PreachGrade> preachGradeRepo) {
        _preachGradeRepo = preachGradeRepo;
    }

    public async Task Handle(UpdatePreachGradeCommand request, CancellationToken cancellationToken) {
        var preachGrade = await _preachGradeRepo.GetByIdAsTrackingAsync(request.Id, cancellationToken: cancellationToken)
            ?? throw new RecordNotFoundException<PreachGrade>(request.Id);

        request.ToEntity(preachGrade);
        await _preachGradeRepo.UpdateAsync(preachGrade, true,cancellationToken);
    }
}
