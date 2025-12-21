using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;
using System.ComponentModel;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;

/// <inheritdoc/>
[QueryBuilder(Label = "تاریخچه امتیاز هدفمندی", Name = "TbHadafmandiEmtiaz",Tab =Enums.ReportBuilderTab.Student)]
public class StudentTargetedScoreHistory : IQueryBuilderTable
{
#pragma warning disable VSSpell001 // Spell Check
#pragma warning disable IDE0049 // Simplify Names
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [QueryBuilder(Label = "امتياز جاري تدريس")]
    public Single EmtiazTadrisCurrent { get; set; }

    [QueryBuilder(Label = "امتياز سابقه تدريس")]
    public Single EmtiazTadrisHistory { get; set; }

    [QueryBuilder(Label = "امتياز جاري تبليغ")]
    public Single EmtiazTablighCurrent { get; set; }

    [QueryBuilder(Label = "امتياز سابقه تبليغ")]
    public Single EmtiazTablighHistory { get; set; }

    [QueryBuilder(Label = "امتياز جاري پژوهش")]
    public Single EmtiazResearchCurrent { get; set; }

    [QueryBuilder(Label = "امتياز سابقه پژوهش")]
    public Single EmtiazResearchHistory { get; set; }

    [QueryBuilder(Label = "امتياز تحصيل")]
    public Single EmtiazTahsil { get; set; }

    [QueryBuilder(Label = "امتياز کل")]
    public Single EmtiazKol { get; set; }
}
