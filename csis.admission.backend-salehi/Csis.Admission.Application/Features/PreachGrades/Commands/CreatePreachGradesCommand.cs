using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.PreachGrades.Commands;

/// <summary>
/// CreatePreachGradeCommand
/// </summary>
public sealed record CreatePreachGradeCommand : BaseCommandDto<CreatePreachGradeCommand, PreachGrade>, IRequest<int>
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
    public override void ReverseCustomMappings(IMappingExpression<CreatePreachGradeCommand, PreachGrade> mapping) {
        mapping.ForMember(model => model.RegisterDate, config => config.MapFrom(dto => dto.RegisterDate.StringDateToInt()));
        mapping.ForMember(model => model.ExpirationDate, config => config.MapFrom(dto => dto.ExpirationDate.StringDateToInt()));
    }
}

internal sealed class CreatePreachGradeCommandHandler : IRequestHandler<CreatePreachGradeCommand, int>
{
    private readonly IRepository<PreachGrade> _preachGradeRepo;
    public CreatePreachGradeCommandHandler(IRepository<PreachGrade> preachGradeRepo) {
        _preachGradeRepo = preachGradeRepo;
    }

    public async Task<int> Handle(CreatePreachGradeCommand request, CancellationToken cancellationToken) {
        var preachGrade = request.ToEntity();
        await _preachGradeRepo.InsertAsync(preachGrade, cancellationToken: cancellationToken);
        return preachGrade.Id;
    }
}
