using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Features.Files.Dtos;

namespace Csis.Admission.Application.Features.DependentEmployments.Dtos;

/// <summary>شغل و درآمد تکفل</summary>
public sealed record DependentEmploymentDto : BaseDto<DependentEmploymentDto, DependentEmployment>
{
    /// <summary>Codm</summary>
    public int Codm { get; set; }

    /// <summary>شناسه تکفل</summary>
    public long DependentId { get; set; }

    /// <summary>تکفل</summary>
    public string Dependent { get; set; }

    /// <summary>نسبت</summary>
    public DependentRelation? Relation { get; set; }

    /// <summary>وضعیت اشتغال</summary>
    public bool? IsEmployee { get; set; }

    /// <summary>نام محل کار</summary>
    public string EmployeeName { get; set; }

    /// <summary>آدرس محل کار</summary>
    public string EmployeeAddress { get; set; }

    /// <summary>دارای بیمه پایه غیر از مرکز</summary>
    public bool? HasAnotherBaseInsurance { get; set; }

    /// <summary>نوع بیمه پایه</summary>
    public EmploymentInsuranceType? InsuranceType { get; set; }

    /// <summary>نام بیمه پایه</summary>
    public string InsurancePlaceName { get; set; }

    /// <summary>آدرس بیمه پایه</summary>
    public string InsurancePlaceAddress { get; set; }

    /// <summary>دارای بیمه تکمیلی غیر از مرکز</summary>
    public bool? HasAnotherSupInsurance { get; set; }

    /// <summary>روش شناسایی اشتغال</summary>
    public EmploymentReference? Reference { get; set; }

    /// <summary>
    /// لیست شناسه مدارک
    /// </summary>
    public List<Guid> FileIdentifiers { get; init; } = [];

    /// <summary>
    /// لیستی از مشخصات فایل های مدارک
    /// </summary>
    public List<FileModelDto> FilesInfo { get; init; } = [];

    /// <inheritdoc/>
    public override void CustomMappings(IMappingExpression<DependentEmployment, DependentEmploymentDto> mapping) {
        mapping.ForMember(dto => dto.Relation, config => config.MapFrom(model => model.Dependent.Relation));
        mapping.ForMember(dto => dto.DependentId, config => config.MapFrom(model => model.DependentId));
        mapping.ForMember(dto => dto.Dependent, config => config.MapFrom(model => model.Dependent.FullName));
        mapping.ForMember(dto => dto.FileIdentifiers, config => config.MapFrom(model => model.Request.Documents.Select(x=>x.FileId)));
    }
}
