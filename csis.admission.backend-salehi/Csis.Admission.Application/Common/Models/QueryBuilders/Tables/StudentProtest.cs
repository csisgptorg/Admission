using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;

/// <inheritdoc/>
[QueryBuilder(Label = "اعتراضات", Name = "TbEteraz", Tab = Enums.ReportBuilderTab.Student)]
public class StudentProtest : IQueryBuilderTable
{
    /// <summary>فیلد مورد اعتراض</summary>
    [QueryBuilder(Label = "نام فيلد اطلاعاتي")]
    public short FieldId { get; set; }
}
