
namespace Csis.Admission;

internal sealed class CsisAdmissionService : ICsisAdmissionService
{
    private readonly AdmissionHttpRequestService _httpRequestService;
    private const string BasePath = "/api/private/client-application-requests";
    public CsisAdmissionService(AdmissionHttpRequestService httpRequestService) => _httpRequestService = httpRequestService;

    public async Task<string> GetStudentProfileImageAsync(int codm, CancellationToken cancellation) {
        var httpRequest = _httpRequestService.CreateRequest(HttpMethod.Get, BasePath + $"/students/{codm}/profile-image");
        var profileImage = await _httpRequestService.SendAsync<string>(httpRequest, cancellation);
        return profileImage;
    }

    public async Task<StudentCaseDtoResult> GetStudentCaseAsync(int codm, CancellationToken cancellation) {
        var httpRequest = _httpRequestService.CreateRequest(HttpMethod.Get, BasePath + $"/students/{codm}/case");
        var @case = await _httpRequestService.SendAsync<StudentCaseDtoResult>(httpRequest, cancellation);
        return @case;
    }

    public async Task<StudentTotalReportResult> GetStudentTotalReportAsync(int codm, CancellationToken cancellation) {
        var httpRequest = _httpRequestService.CreateRequest(HttpMethod.Get, BasePath + $"/total-report/{codm}");
        var studentTotalReport = await _httpRequestService.SendAsync<StudentTotalReportResult>(httpRequest, cancellation);
        return studentTotalReport;
    }

    public async Task<StudentElectronicIdCardResult> GetStudentElectronicIdCardAsync(int codm, CancellationToken cancellation) {
        var httpRequest = _httpRequestService.CreateRequest(HttpMethod.Get, BasePath + $"/students/electronic-card/{codm}");
        var studentElectronicIdCard = await _httpRequestService.SendAsync<StudentElectronicIdCardResult>(httpRequest, cancellation);
        return studentElectronicIdCard;
    }

    public async Task<StudentDataForPayRunResult> GetDataForPayRunByCodmAsync(int codm, CancellationToken cancellationToken) {
        var httpRequest = _httpRequestService.CreateRequest(HttpMethod.Get, BasePath + $"/students/data-for-pay-run?codm={codm}");
        var studentDataForPayRun = await _httpRequestService.SendAsync<StudentDataForPayRunResult>(httpRequest, cancellationToken);
        return studentDataForPayRun;
    }

    public async Task<List<StudentDataForPayRunResult>> GetDataForPayRunByCodmListAsync(List<int> codmList, CancellationToken cancellationToken) {
        var httpRequest = _httpRequestService.CreateRequest(HttpMethod.Post, BasePath + $"/students/data-for-pay-run-list", new { codmList });
        var studentDataForPayRunList = await _httpRequestService.SendAsync<List<StudentDataForPayRunResult>>(httpRequest, cancellationToken);
        return studentDataForPayRunList;
    }

    public async Task<List<StudentDataForPayRunResult>> GetDataForPayRunByStartEndCodmAsync(int startCodm, int endCodm, CancellationToken cancellationToken) {
        var httpRequest = _httpRequestService.CreateRequest(HttpMethod.Post, BasePath + $"/students/data-for-pay-run-start-end-codm", new { startCodm, endCodm });
        var studentDataForPayRunList = await _httpRequestService.SendAsync<List<StudentDataForPayRunResult>>(httpRequest, cancellationToken);
        return studentDataForPayRunList;
    }

    public async Task<int> GetMaxCodmForPayRunAsync(CancellationToken cancellationToken) {
        var httpRequest = _httpRequestService.CreateRequest(HttpMethod.Get, BasePath + $"/students/max-codm-for-pay-run");
        var maxCodm = await _httpRequestService.SendAsync<int>(httpRequest, cancellationToken);
        return maxCodm;
    }

    public async Task<CityResult[]> GetCitiesByProvinceIdAsync(short? ProvinceId, CancellationToken cancellationToken) {
        var httpRequest = _httpRequestService.CreateRequest(HttpMethod.Get, BasePath + $"/students/cities?ProvinceId={ProvinceId}");
        var cities = await _httpRequestService.SendAsync<CityResult[]>(httpRequest, cancellationToken);
        return cities;
    }

    public async Task<List<ProvinceResult>> GetProvincesAsync(CancellationToken cancellationToken) {
        var httpRequest = _httpRequestService.CreateRequest(HttpMethod.Get, BasePath + $"/students/provinces");
        var provinces = await _httpRequestService.SendAsync<List<ProvinceResult>>(httpRequest, cancellationToken);
        return provinces;
    }

    public async Task<bool> ValidateBranchesByProvinceIdAsync(short ProvinceId, short BranchId, CancellationToken cancellationToken) {
        var httpRequest = _httpRequestService.CreateRequest(HttpMethod.Get, BasePath + $"/students/validate-branches-by-province-id?ProvinceId={ProvinceId}&BranchId={BranchId}");
        var isValid = await _httpRequestService.SendAsync<bool>(httpRequest, cancellationToken);
        return isValid;
    }

    public async Task<HousingActivitiesInfoResult> GetHousingActivitiesInfoAsync(int codm, CancellationToken cancellationToken) {
        var httpRequest = _httpRequestService.CreateRequest(HttpMethod.Get, BasePath + $"/activities/{codm}");
        var housingActivitiesInfo = await _httpRequestService.SendAsync<HousingActivitiesInfoResult>(httpRequest, cancellationToken);
        return housingActivitiesInfo;
    }

    public async Task<HousingBasicInfoResult> GetHousingBasicInfoByCodmAsync(int codm, CancellationToken cancellationToken) {
        var httpRequest = _httpRequestService.CreateRequest(HttpMethod.Get, BasePath + $"/basic/{codm}");
        var housingBasicInfo = await _httpRequestService.SendAsync<HousingBasicInfoResult>(httpRequest, cancellationToken);
        return housingBasicInfo;
    }

    public async Task<HousingStatusInfoResult> GetHousingStatusInfoByCodmAsync(int codm, CancellationToken cancellationToken) {
        var httpRequest = _httpRequestService.CreateRequest(HttpMethod.Get, BasePath + $"/status/{codm}");
        var housingStatusInfo = await _httpRequestService.SendAsync<HousingStatusInfoResult>(httpRequest, cancellationToken);
        return housingStatusInfo;
    }

    public Task<BranchResult> GetProvinceByBranchIdAsync(short branchId, CancellationToken cancellationToken) {
        var httpRequest = _httpRequestService.CreateRequest(HttpMethod.Get, BasePath + $"/students/provinces-by-branch-id?branchId={branchId}");
        return _httpRequestService.SendAsync<BranchResult>(httpRequest, cancellationToken);
    }

    public Task<Dictionary<int, short?>> GetDecileByCodmListAsync(List<int> codmList, CancellationToken cancellationToken) {
        var httpRequest = _httpRequestService.CreateRequest(HttpMethod.Post, BasePath + $"/students/deciles", codmList);
        return _httpRequestService.SendAsync<Dictionary<int, short?>>(httpRequest, cancellationToken);
    }

    public Task<StudentWithDependentsResult> GetStudentWithDependentsAsync(int codm, CancellationToken cancellationToken) {
        var httpRequest = _httpRequestService.CreateRequest(HttpMethod.Get, BasePath + $"/students/with-dependents?codm={codm}");
        return _httpRequestService.SendAsync<StudentWithDependentsResult>(httpRequest, cancellationToken);
    }

    public Task<List<BranchResult>> GetAllBranchesAsync(CancellationToken cancellationToken) {
        var httpRequest = _httpRequestService.CreateRequest(HttpMethod.Get, BasePath + $"/students/branches");
        return _httpRequestService.SendAsync<List<BranchResult>>(httpRequest, cancellationToken);
    }

    public Task<List<StudentWithDependentInfoForDowryServiceResult>> GetStudentInfoForDowryServiceAsync(List<int?> codms, CancellationToken cancellationToken) {
        var httpRequest = _httpRequestService.CreateRequest(HttpMethod.Post, BasePath + $"/students/dowry-info", codms);
        return _httpRequestService.SendAsync<List<StudentWithDependentInfoForDowryServiceResult>>(httpRequest, cancellationToken);
    }

    public Task<List<ActiveTenantRentContractResult>> GetActiveTenantRentContractsByCodmAsync(int codm, CancellationToken cancellationToken) {
        var httpRequest = _httpRequestService.CreateRequest(HttpMethod.Get, BasePath + $"/students/active-tenants?codm={codm}");
        return _httpRequestService.SendAsync<List<ActiveTenantRentContractResult>>(httpRequest, cancellationToken);
    }

    public Task<List<GetStudentCodmByNationalCodeResult>> GetStudentsCodmByNationalCodeAsync(List<string> nationalCodes, CancellationToken cancellationToken) {
        var httpRequest = _httpRequestService.CreateRequest(HttpMethod.Post, BasePath + $"/students/bulk-codm-by-national-code", nationalCodes);
        return _httpRequestService.SendAsync<List<GetStudentCodmByNationalCodeResult>>(httpRequest, cancellationToken);
    }
}
