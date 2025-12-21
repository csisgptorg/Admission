using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;

/// <inheritdoc/>
[QueryBuilder(Label = "فعاليت های فرهنگی و اجتماعی", Name = "TbFarhangi", Tab = Enums.ReportBuilderTab.Student)]
public class StudentCulturalActivity : IQueryBuilderTable
{
    /// <inheritdoc/>
    [QueryBuilder(Label = "نوع فعاليت", Name = "KindManage")]
    public CulturalKind Kind { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "سال فعالیت")]
    public int Year { get; set; }
}
