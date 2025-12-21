using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[QueryBuilder(Label = "حافظين", Name = "TbHafezin", Tab = Enums.ReportBuilderTab.Dependent)]
public class DependentMemorizer : IQueryBuilderTable
{
    [QueryBuilder(Label = "نوع", Name = "Kind")]
    public MemorizationType? Kind { get; set; }

    [QueryBuilder(Label = "تعداد جزء/خطبه/حديث", Name = "JozCount")]
    public int? JozCount { get; set; }

    [QueryBuilder(Label = "محل صدور حکم", Name = "MarkazeHouzavi")]
    public ApprovalCenter? ApprovalCenter { get; set; }
}
