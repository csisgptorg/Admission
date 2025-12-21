using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[QueryBuilder(Label = "مسکن", Name = "tbMaskan",Tab =Enums.ReportBuilderTab.Student)]
public class StudentHouse : IQueryBuilderTable
{
    [QueryBuilder(Label = "وضعيت سکونت")]
    public HouseStatus HouseStatus { get; set; }
}
