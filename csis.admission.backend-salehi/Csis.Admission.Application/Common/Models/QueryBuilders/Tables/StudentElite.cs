using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;

/// <inheritdoc/>
[QueryBuilder(Label = "جامعه نخبگانی", Name = "TbNokhbeInfo", Tab = Enums.ReportBuilderTab.Student)]
public class StudentElite : IQueryBuilderTable
{
    /// <inheritdoc/>
    [QueryBuilder(Label = "نوع نخبگی", Name = "NokhbeType", RelationTable = nameof(EliteType), Source = Enums.ColumnSourceType.Api)]
    public short? EliteTypeId { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "سطح نخبگی", Name = "NokhbeLevel", RelationTable = nameof(EliteLevel), Source = Enums.ColumnSourceType.Api)]
    public short? EliteLevelId { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "تاريخ شروع", Type = typeof(DateTime))]
    public int? StartDate { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "تاريخ پایان", Type = typeof(DateTime))]
    public int? EndDate { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "مرجع شناسايی", Name = "MarjaStr")]
    public string ApprovalCenterTitle { get; set; }
}
