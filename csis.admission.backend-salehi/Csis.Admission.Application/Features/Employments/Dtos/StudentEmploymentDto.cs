using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Features.Files.Dtos;
using System.Text.Json.Serialization;

namespace Csis.Admission.Application.Features.Employments.Dtos;

/// <summary>
/// شغل و درآمد
/// </summary>
public sealed record StudentEmploymentDto : BaseDto<StudentEmploymentDto, StudentEmployment>
{
    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// HasIncome
    /// </summary>
    public bool? HasIncome { get; set; }

    /// <summary>
    /// IsEmployee
    /// </summary>
    public bool? IsEmployee { get; set; }

    /// <summary>
    /// EmployeeName
    /// </summary>
    public string EmployeeName { get; set; }

    /// <summary>
    /// EmployeeAddress
    /// </summary>
    public string EmployeeAddress { get; set; }

    /// <summary>
    /// HasSufficientIncome
    /// </summary>
    public bool? HasSufficientIncome { get; set; }

    /// <summary>
    /// HasAnotherBaseInsurance
    /// </summary>
    public bool? HasAnotherBaseInsurance { get; set; }

    /// <summary>
    /// InsurancePlaceName
    /// </summary>
    public string InsurancePlaceName { get; set; }

    /// <summary>
    /// InsurancePlaceAddress
    /// </summary>
    public string InsurancePlaceAddress { get; set; }

    /// <summary>
    /// HasAnotherSupInsurance
    /// </summary>
    public bool? HasAnotherSupInsurance { get; set; }

    /// <summary>
    /// IsEmployeeInHowze
    /// </summary>
    public bool? IsEmployeeInHowze { get; set; }

    /// <summary>
    /// HowzeTypeId
    /// </summary>
    public EmploymentHowzeType? HowzeTypeId { get; set; }

    /// <summary>
    /// IsRetried
    /// </summary>
    public bool? IsRetried { get; set; }

    /// <summary>دهک درآمدی فرد</summary>
    public short? Decile { get; set; }

    /// <summary>
    /// InsuranceType
    /// </summary>
    public EmploymentInsuranceType? InsuranceType { get; set; }

    /// <inheritdoc/>
    public EmploymentReference Reference { get; set; }

    /// <summary>
    /// لیست شناسه مدارک
    /// </summary>
    [JsonIgnore]
    public List<Guid> FileIdentifiers { get; init; } = [];

    /// <summary>
    /// لیستی از مشخصات فایل های مدارک
    /// </summary>
    public List<FileModelDto> FilesInfo { get; init; } = [];

    /// <inheritdoc/>
    public override void CustomMappings(IMappingExpression<StudentEmployment, StudentEmploymentDto> mapping) {
        mapping.ForMember(dto => dto.InsuranceType, config => config.MapFrom(model => model.InsuranceTypeId));
        mapping.ForMember(dto => dto.FileIdentifiers, config => config.MapFrom(model => model.Request.Documents.Select(f => f.FileId)));
    }
}
