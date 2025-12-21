using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.TeachGrades.Commands;

/// <summary>
/// CreateTeachGradeCommand
/// </summary>
public sealed record CreateTeachGradeCommand : BaseCommandDto<CreateTeachGradeCommand, TeachGrade>, IRequest<int>
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
    public override void ReverseCustomMappings(IMappingExpression<CreateTeachGradeCommand, TeachGrade> mapping) {
        mapping.ForMember(model => model.RegisterDate, config => config.MapFrom(dto => dto.RegisterDate.StringDateToInt()));
        mapping.ForMember(model => model.ExpirationDate, config => config.MapFrom(dto => dto.ExpirationDate.StringDateToInt()));
    }
}

internal sealed class CreateTeachGradeCommandHandler : IRequestHandler<CreateTeachGradeCommand, int>
{
    private readonly IRepository<TeachGrade> _reachGradeRepo;
    public CreateTeachGradeCommandHandler(IRepository<TeachGrade> reachGradeRepo) {
        _reachGradeRepo = reachGradeRepo;
    }

    public async Task<int> Handle(CreateTeachGradeCommand request, CancellationToken cancellationToken) {
        var reachGrade = request.ToEntity();
        await _reachGradeRepo.InsertAsync(reachGrade, cancellationToken: cancellationToken);
        return reachGrade.Id;
    }
}
