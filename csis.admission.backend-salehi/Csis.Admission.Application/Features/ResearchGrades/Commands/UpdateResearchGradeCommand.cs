using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.ResearchGrades.Commands;

/// <summary>
/// UpdateResearchGradeCommand
/// </summary>
public sealed record UpdateResearchGradeCommand : BaseCommandDto<UpdateResearchGradeCommand, ResearchGrade>, IRequest
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
    public override void ReverseCustomMappings(IMappingExpression<UpdateResearchGradeCommand, ResearchGrade> mapping) {
        mapping.ForMember(model => model.RegisterDate, config => config.MapFrom(dto => dto.RegisterDate.StringDateToInt()));
        mapping.ForMember(model => model.ExpirationDate, config => config.MapFrom(dto => dto.ExpirationDate.StringDateToInt()));
    }
}

internal sealed class UpdateResearchGradeCommandHandler : IRequestHandler<UpdateResearchGradeCommand>
{
    private readonly IRepository<ResearchGrade> _researchGradeRepo;
    public UpdateResearchGradeCommandHandler(IRepository<ResearchGrade> researchGradeRepo) {
        _researchGradeRepo = researchGradeRepo;
    }

    public async Task Handle(UpdateResearchGradeCommand request, CancellationToken cancellationToken) {
        var researchGrade = await _researchGradeRepo.GetByIdAsTrackingAsync(request.Id, cancellationToken: cancellationToken)
            ?? throw new RecordNotFoundException<ResearchGrade>(request.Id);

        request.ToEntity(researchGrade);
        await _researchGradeRepo.UpdateAsync(researchGrade, true,cancellationToken);
    }
}
