
using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.HousingAdmissionInfo.Dtos;

/// <summary>
/// دریافت کدمرکز طلبه با کد ملی
/// </summary>
public sealed record GetStudentCodmByNationalCodeDto
{
    /// <summary>
    /// کد ملی
    /// </summary>
    public string NationalCode { get; init; }
    /// <summary>
    /// کد مرکز طلبه
    /// </summary>
    public int Codm { get; init; }

    /// <summary>
    /// شناسه همسر
    /// </summary>
    public GetStudentDependentByNationalCodeDto? Dependent { get; init; }
}
/// <summary>
/// دریافت اطلاعات همسر با کد ملی
/// </summary>
public sealed record GetStudentDependentByNationalCodeDto
{
    /// <summary>
    /// کد ملی
    /// </summary>
    public string NationalCode { get; init; }
    /// <summary>
    /// شناسه همسر
    /// </summary>
    public long? DependentId { get; init; }
}
