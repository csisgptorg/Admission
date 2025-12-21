using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.Preaches.Commands;
using Csis.Admission.Application.Features.ResearchGrades.Dtos;

namespace Csis.Admission.Application.Features.ResearchGrades.Commands;

/// <summary>
/// CreateResearchGradeCommand
/// </summary>
public sealed record CreateResearchGradeCommand : BaseCommandDto<CreateResearchGradeCommand, ResearchGrade>, IRequest<int>
{
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
    public override void ReverseCustomMappings(IMappingExpression<CreateResearchGradeCommand, ResearchGrade> mapping) {
        mapping.ForMember(model => model.RegisterDate, config => config.MapFrom(dto => dto.RegisterDate.StringDateToInt()));
        mapping.ForMember(model => model.ExpirationDate, config => config.MapFrom(dto => dto.ExpirationDate.StringDateToInt()));
    }
}

internal sealed class CreateResearchGradeCommandHandler : IRequestHandler<CreateResearchGradeCommand, int>
{
    private readonly IRepository<ResearchGrade> _researchGradeRepo;
    public CreateResearchGradeCommandHandler(IRepository<ResearchGrade> researchGradeRepo) {
        _researchGradeRepo = researchGradeRepo;
    }

    public async Task<int> Handle(CreateResearchGradeCommand request, CancellationToken cancellationToken) {
        var researchGrade = request.ToEntity();
        await _researchGradeRepo.InsertAsync(researchGrade, cancellationToken: cancellationToken);
        return researchGrade.Id;
    }
}
