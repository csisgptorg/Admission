using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.BasicData;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.CountryDivisions.Commands;

namespace Csis.Admission.Persistence.Repositories.BasicData;

public sealed class BasicDataRepository : IBasicDataRepository
{
    private readonly AppDapperContext _dapper;

    public BasicDataRepository(AppDapperContext dapper) {
        _dapper = dapper;
    }

    public async Task<ProcedureResultDto> CreateSetTown(CreateSetTownInCountryDivisionsCommand command) {
        var result = await _dapper.ExecuteProcedureBaseSingleOrDefault<ProcedureResultDto>(ProcedureName.SetTown, command);
        return result;
    }

    public async Task<ProcedureResultDto> CreateSetRural(CreateSetRuralCountryDivisionsCommand command) {
        var result = await _dapper.ExecuteProcedureBaseSingleOrDefault<ProcedureResultDto>(ProcedureName.SetRural, command);
        return result;
    }

    public async Task<ProcedureResultDto> CreateSetPortion(CreateSetPortionCountryDivisionsCommand command) {
        var result = await _dapper.ExecuteProcedureBaseSingleOrDefault<ProcedureResultDto>(ProcedureName.SetPortion, command);
        return result;
    }
}
