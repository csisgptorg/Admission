using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Features.CountryDivisions.Commands;

namespace Csis.Admission.Application.Common.Interfaces.Repositories.BasicData;

/// <summary>
/// سرویس دیتا پایه
/// </summary>
public interface IBasicDataRepository
{
    /// <summary>
    /// ایجاد دیتاپایه شهر در تقسیمات کشوری
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    Task<ProcedureResultDto> CreateSetTown(CreateSetTownInCountryDivisionsCommand command);

    /// <summary>
    /// ایجاد دهستان شهر در تقسیمات کشوری
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    Task<ProcedureResultDto> CreateSetRural(CreateSetRuralCountryDivisionsCommand command);

    /// <summary>
    /// ایجاد بخش شهر در تقسیمات کشوری
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    Task<ProcedureResultDto> CreateSetPortion(CreateSetPortionCountryDivisionsCommand command);
}
