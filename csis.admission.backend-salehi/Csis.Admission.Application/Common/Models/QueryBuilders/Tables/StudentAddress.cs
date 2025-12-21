using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[QueryBuilder(Label = "آدرس", Name = "TbAddress",Tab =Enums.ReportBuilderTab.Student)]
public class StudentAddress : IQueryBuilderTable
{
    [QueryBuilder(Label = "استان",Name = nameof(Province), RelationTable = nameof(Province), Source = Enums.ColumnSourceType.Api)]
    public short? ProvinceId { get; set; }

    [QueryBuilder(Label = "شهرستان", Name = nameof(City),DependentColumn =nameof(ProvinceId) ,RelationTable = nameof(City), Source = Enums.ColumnSourceType.Api)]
    public short? CityId { get; set; }

    [QueryBuilder(Label = "بخش", Name =nameof(Portion),DependentColumn =nameof(CityId) ,RelationTable = nameof(Portion), Source = Enums.ColumnSourceType.Api)]
    public short? PortionId { get; set; }

    [QueryBuilder(Label = "شهر",Name =nameof(Town), DependentColumn =nameof(PortionId) ,RelationTable = nameof(Town), Source = Enums.ColumnSourceType.Api)]
    public short? TownId { get; set; }

    [QueryBuilder(Label = "دهستان",Name =nameof(Rural),DependentColumn =nameof(TownId) ,RelationTable = nameof(Rural), Source = Enums.ColumnSourceType.Api)]
    public short? RuralId { get; set; }

    [QueryBuilder(Label = "شهرک", Name = "Dorp")]
    public string Township { get; set; }

    [QueryBuilder(Label = "روستا")]
    public string Village { get; set; }

    [QueryBuilder(Label = "محله")]
    public string District { get; set; }

    [QueryBuilder(Label = "خیابان اصلی")]
    public string Avenue { get; set; }

    [QueryBuilder(Label = "خیابان فرعی")]
    public string Street { get; set; }

    [QueryBuilder(Label = "کوچه اصلی")]
    public string Alley { get; set; }

    [QueryBuilder(Label = "کوچه فرعی")]
    public string Lane { get; set; }

    [QueryBuilder(Label = "پلاک")]
    public string Number { get; set; }

    [QueryBuilder(Label = "مجتمع")]
    public string Complex { get; set; }

    [QueryBuilder(Label = "بلوک")]
    public string Block { get; set; }

    [QueryBuilder(Label = "واحد")]
    public string Unit { get; set; }

    [QueryBuilder(Label = "طبقه")]
    public short? Floor { get; set; }

    [QueryBuilder(Label = "کدپستی")]
    public long? ZipCode { get; set; }
}
