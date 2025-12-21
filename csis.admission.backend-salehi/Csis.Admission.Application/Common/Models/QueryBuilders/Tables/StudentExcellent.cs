using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

/// <inheritdoc/>
[QueryBuilder(Label = "ممتازين", Name = "Momtazin", Tab = Enums.ReportBuilderTab.Student)]
public class StudentExcellent : IQueryBuilderTable
{
    [QueryBuilder(Label = "سال تحصیلی", Name = "SalMomtaz",RelationTable =nameof(ExcellentEducationYear), Source = Enums.ColumnSourceType.Api)]
    public short? ExcellentEducationYearId { get; set; }

    [QueryBuilder(Label = "مقطع", Name = "Maghta",RelationTable =nameof(ExcellentEducationLevel), Source = Enums.ColumnSourceType.Api)]
    public short? ExcellentEducationLevelId { get; set; }

    [QueryBuilder(Label = "معدل", Name = "Moadel")]
    public double? Average { get; set; }
}
