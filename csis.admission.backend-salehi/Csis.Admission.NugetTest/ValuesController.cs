using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.NugetTest;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    private readonly ICsisAdmissionService _service;
    public ValuesController(ICsisAdmissionService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Test() {

        //var profileImage = await _service.GetStudentProfileImage(82000,default);
        //var @case = await _service.GetStudentCase(82000,default);
        //var totalReport = await _service.GetStudentTotalReport(82000, default);
        //var eCard = await _service.GetStudentElectronicIdCard(82000, cancellation: CancellationToken.None);
        //var GetDataForPayRunByCodm = await _service.GetDataForPayRunByCodm(82000, cancellation: CancellationToken.None);
        //var GetDataForPayRunByCodmList = await _service.GetDataForPayRunByCodmList([12000, 82000, 3680, 201, 65189], cancellation: CancellationToken.None);
        //var GetDataForPayRunByStartEndCodm = await _service.GetDataForPayRunByStartEndCodm(12000, 82000, cancellation: CancellationToken.None);
        //var maxCodm = await _service.GetMaxCodmForPayRun(cancellationToken: CancellationToken.None);

        //var validation = await _service.ValidateBranchesByProvinceId(1, 1, cancellationToken: CancellationToken.None);
        //var validationQom = await _service.ValidateBranchesByProvinceId(18, 0, cancellationToken: CancellationToken.None);

        //var towns = await _service.GetCitiesByProvinceId(1, cancellationToken: CancellationToken.None);
        //var provinces = await _service.GetProvinces(cancellationToken: CancellationToken.None);
        //var basic = await _service.GetHousingBasicInfoByCodmAsync(82000,cancellationToken: CancellationToken.None);
        //var activities = await _service.GetHousingActivitiesInfoAsync(82000, cancellationToken: CancellationToken.None);
        //var status = await _service.GetHousingStatusInfoByCodmAsync(82000,cancellationToken: CancellationToken.None);
        //var dep = await _service.GetStudentWithDependents(102093, cancellationToken: CancellationToken.None);
        //var dep2 = await _service.GetAllBranches(cancellationToken: CancellationToken.None);
        //var dowry = await _service.GetStudentInfoForDowryServiceAsync([482555, 82000, 65189, 201], cancellationToken: CancellationToken.None);
        //var tenants = await _service.GetActiveTenantRentContractsByCodmAsync(82000, cancellationToken: CancellationToken.None);
        var nationalCodes = await _service.GetStudentsCodmByNationalCodeAsync(new List<string> { "1092335205", "1092335204", "0372285708", "0382820932", "0371305543", "0383621895" }, cancellationToken: CancellationToken.None);
        return Ok(new { nationalCodes });
    }
}
