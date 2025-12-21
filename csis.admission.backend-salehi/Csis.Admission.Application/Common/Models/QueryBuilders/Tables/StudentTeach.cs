using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[QueryBuilder(Label = "تدريس", Name = "TbTadris", Tab = Enums.ReportBuilderTab.Student)]
public class StudentTeach : IQueryBuilderTable
{
    [QueryBuilder(Label = "استان", Name = "Ostan", RelationTable = nameof(Province), Source = Enums.ColumnSourceType.Api)]
    public short? ProvinceId { get; set; }

    [QueryBuilder(Label = "شهرستان", Name = nameof(City), RelationTable = nameof(City),DependentColumn =nameof(ProvinceId), Source = Enums.ColumnSourceType.Api)]
    public short? CityId { get; set; }

    [QueryBuilder(Label = "سال تحصيلی", Name = "SaleTahsili", RelationTable = nameof(EducationYear), Source = Enums.ColumnSourceType.Api)]
    public int? EducationYearId { get; set; }

    [QueryBuilder(Label = "مقطع تدريس", Name = "MaghtaeTadris")]
    public TeachEducationLevel? EducationLevel { get; set; }

    [QueryBuilder(Label = "درس", Name = "Dars")]
    public string Lesson { get; set; }

    [QueryBuilder(Label = "مدرسه", Name = "Madrese", RelationTable = nameof(School), Source = Enums.ColumnSourceType.Api)]
    public short? SchoolId { get; set; }

    [QueryBuilder(Label = "تدريس در هفته", Name = "WeekSession")]
    public short? WeekSession { get; set; }

    [QueryBuilder(Label = "مرکز حوزوي", Name = "MarkazeHouzavi")]
    public ApprovalCenter? ApprovalCenter { get; set; }
}
