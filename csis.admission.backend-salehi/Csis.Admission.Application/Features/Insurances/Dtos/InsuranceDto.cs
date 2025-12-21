using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Insurances.Dtos;

/// <summary>بیمه</summary>
public sealed record StudentDependentInsuranceDto
{
    /// <inheritdoc/>
    public StudentDependentInsuranceDto(int codm, long? dependentId) {
        Codm = codm;
        DependentId = dependentId;
    }

    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public long? DependentId{ get; set; }

    /// <summary>وضعیت بیمه سلامت</summary>
    public RegistrationStatus? HealthInsuranceStatus { get; set; }

    /// <summary>شماره پرونده بیمه سلامت</summary>
    public string HealthInsuranceCaseNumber { get; set; }

    /// <summary>وضعیت بیمه تکمیلی</summary>
    public RegistrationStatus? SupInsuranceHealthStatus { get; set; }

    /// <summary>عنوان بیمه تکمیلی</summary>
    public string SupInsuranceHealthPlanTitle { get; set; }

    /// <summary>وضعیت بیمه تکمیلی</summary>
    public RegistrationStatus? SupInsuranceLifeStatus { get; set; }

    /// <summary>عنوان بیمه تکمیلی</summary>
    public string SupInsuranceLifePlanTitle { get; set; }

    /// <summary>وضعیت بیمه تامین اجتماعی</summary>
    public StudentTaminInsuranceResultDto.StatusEnum? TaminInsuranceStatus { get; set; }

    /// <inheritdoc/>
    public int? TaminInsuranceNumber { get; set; }

    /// <inheritdoc/>
    public string TaminInsuranceDescription { get; set; }
}
