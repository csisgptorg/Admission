using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[QueryBuilder(Label = "تحصيلات کلاسيک", Name = "TbClassic", Tab = Enums.ReportBuilderTab.Dependent)]
public class DependentUniversityEducation : IQueryBuilderTable
{
    [QueryBuilder(Label = "در حال تحصیل", Name = "KindTahsil")]
    public bool InStudy { get; set; }

    [QueryBuilder(Label = "ميزان تحصيلات", Name = "Degree")]
    public StudyLevel? StudyLevel { get; set; }

    [QueryBuilder(Label = "رشته", Name = "Reshte")]
    public string CourseStudy { get; set; }

    [QueryBuilder(Label = "نام دانشگاه", Name = "SchoolName")]
    public string UniversityName { get; set; }

    [QueryBuilder(Label = "تاريخ شروع مقطع", Name = "DataStart",Type =typeof(DateTime))]
    public int? StartDate { get; set; }

    [QueryBuilder(Label = "تاريخ پايان مقطع", Name = "EndDate", Type = typeof(DateTime))]
    public int? EndDate { get; set; }

    [QueryBuilder(Label = "معدل کل", Name = "Moadel")]
    public double? Average { get; set; }
}
