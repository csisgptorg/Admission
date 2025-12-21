using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

/// <inheritdoc/>
[QueryBuilder(Label = "تبليغ", Name = "TbTabligh",Tab =Enums.ReportBuilderTab.Student)]
public class StudentPreach : IQueryBuilderTable
{

    [QueryBuilder(Label = "کشور", Name = nameof(Country), RelationTable = nameof(Country), Source = Enums.ColumnSourceType.Api)]
    public int? CountryId { get; set; }

    [QueryBuilder(Label = "استان", Name = nameof(Province), RelationTable = nameof(Province),DependentColumn =nameof(CountryId), Source = Enums.ColumnSourceType.Api)]
    public short? ProvinceId { get; set; }

    [QueryBuilder(Label = "شهر به صورت متني", Name = "CityTitle")]
    public string City { get; set; }

    [QueryBuilder(Label = "تاریخ شروع", Name = "Year",Type =typeof(DateTime))]
    public int? StartDate { get; set;}

    [QueryBuilder(Label = "تاریخ پایان", Name = "YearTo", Type = typeof(DateTime))]
    public int? EndDate { get; set; }

    [QueryBuilder(Label = "نوع", Name = "Kind")]
    public PreachKind? Kind { get; set; }

    [QueryBuilder(Label = "محل صدور مدرک", Name = "Hokm1")]
    public PreachApprovalCenter? ApprovalCenter { get; set; }
}
