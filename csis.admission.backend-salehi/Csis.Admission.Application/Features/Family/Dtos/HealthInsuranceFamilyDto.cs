using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Family.Dtos;

public sealed record HealthInsuranceFamilyDto //: BaseDto<HealthInsuranceFamilyDto,DependentSummary,long>
{
    /// <summary>
    /// کد مرکز
    /// </summary>
    public int Codm { get; init; }
    /// <summary>
    /// آی دی تکفل
    /// </summary>
    public long? DependentId { get; init; }
    /// <summary>
    /// نسبت با فرد اصلی
    /// </summary>
    public DependentRelation? Relation { get; init; }
    /// <summary>
    /// جنسیت
    /// </summary>
    public Gender Gender { get; init; }

    /// <summary>
    /// کد ملی
    /// </summary>
    public string NationalCode { get; init; }
    /// <summary>
    /// کد یکتا
    /// </summary>
    public string YektaCode { get; init; }
    /// <summary>
    /// نام
    /// </summary>
    public string FirstName { get; init; }
    /// <summary>
    /// نام خانوادگی
    /// </summary>
    public string LastName { get; init; }
    /// <summary>
    /// نام پ
    /// </summary>
    public string FatherName { get; init; }
    /// <summary>
    /// پرونده فعال
    /// </summary>
    public bool IsActive { get; init; }






}
