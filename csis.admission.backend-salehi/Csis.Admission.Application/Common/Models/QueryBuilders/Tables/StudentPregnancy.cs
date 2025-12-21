using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member


[QueryBuilder(Label = "بارداری", Name = "Pregnancy", Tab = Enums.ReportBuilderTab.Student)]
public class StudentPregnancy : IQueryBuilderTable
{
    [QueryBuilder(Label = "تاريخ شروع", Type = typeof(DateTime))]
    public int StartDate { get; set; }

    [QueryBuilder(Label = "تاريخ پایان", Type = typeof(DateTime))]
    public int EndDate { get; set; }
}
