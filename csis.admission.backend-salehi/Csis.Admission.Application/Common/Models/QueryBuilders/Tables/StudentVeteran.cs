using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;

/// <inheritdoc/>
[QueryBuilder(Label = "ايثارگری", Name = "Esargari", Tab = Enums.ReportBuilderTab.Student)]
public class StudentVeteran : IQueryBuilderTable
{
    /// <inheritdoc/>
    [QueryBuilder(Label = "تعداد روز دفاع از حرم", Name = "ModafeHaramTotalDay")]
    public int? HaramDefenceDays { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "تعداد روز دفاع مقدس", Name = "DefaMoqadasTotalDay")]
    public int? HolyDefenseDays { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "تعداد روز زندان قبل از انقلاب", Name = "ZendanTotalDay")]
    public int? JailDays { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "تعداد روز تبعید قبل از انقلاب", Name = "TabeedTotalDay")]
    public int? ExileDays { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "درصد جانبازی", Name = "JanbaziDarsad")]
    public short? VeteranPercent { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "نسبت با شهيد", Name = "NesbatBaShahid")]
    public DependentRelation? RelationWithMartyr { get; set; }

}
