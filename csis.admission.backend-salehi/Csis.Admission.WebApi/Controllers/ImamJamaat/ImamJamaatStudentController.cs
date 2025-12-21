#region Usings
using Csis.Abstractions.Results;
using Csis.Admission.Application.Features.Cities.Dtos;
using Csis.Admission.Application.Features.Cities.Queries;
using Csis.Admission.Application.Features.ImamJamaat.Commands;
using Csis.Admission.Application.Features.ImamJamaat.Dtos;
using Csis.Admission.Application.Features.ImamJamaat.Queries;
using Csis.Admission.Application.Features.Portions.Dtos;
using Csis.Admission.Application.Features.Portions.Queries;
using Csis.Admission.Application.Features.Provinces.Dtos;
using Csis.Admission.Application.Features.Provinces.Queries;
using Csis.Admission.Application.Features.Rurals.Dtos;
using Csis.Admission.Application.Features.Rurals.Queries;
using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Admission.Application.Features.Students.Queries;
using Csis.Admission.Application.Features.Towns.Dtos;
using Csis.Admission.Application.Features.Towns.Queries;
using Csis.Admission.WebApi.Filters;
using Csis.Paging;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace Csis.Admission.WebApi.Controllers.ImamJamaat;

[Route("api/public/imamjamaat-student")]
public class ImamJamaatStudentController : ApiControllerBase
{
    ///// <summary>
    ///// گرفتن لیست مساجد با قابلیت جستجو و فیلتر
    ///// </summary>
    ///// <param name="query"></param>
    ///// <returns></returns>
    //[HttpPost("mosque-list")]
    //[DynamicSearch<Domain.Entities.ImamJamaat>]
    //public async Task<ActionResult<Result<IPagedList<MosqueListDto>>>> Search([FromBody] GetMosqueListStudentQuery query) {
    //    return PaginatedResult(await Mediator.Send(query));
    //}

    ///// <summary>
    ///// گرفتن اطلاعات مسجد بر اساس شناسه
    ///// </summary>
    ///// <param name="mosqueId"></param>
    ///// <returns></returns>
    //[HttpGet("mosque/{mosqueId:min(1)}")]
    //public async Task<ActionResult<Result<MosqueByIdDto>>> GetMosqueById([FromRoute] int mosqueId) {
    //    return OkResult(await Mediator.Send(new GetMosqueByIdStudentQuery(mosqueId)));
    //}

    ///// <summary>
    ///// ایجاد مسجد با جزئیات
    ///// </summary>
    ///// <param name="command"></param>
    ///// <returns></returns>
    //[HttpPost("mosque")]
    //public async Task<ActionResult<Result<int>>> CreateStudent([FromBody] CreateMosqueWithDetailsStudentCommand command) {
    //    var result = await Mediator.Send(command);
    //    return CreatedAtAction(nameof(GetMosqueById), new { mosqueId = result }, Result<int>.Success(result ));
    //}

    ///// <summary>
    ///// دریافت آدرس بر اساس کد پستی
    ///// </summary>
    ///// <param name="postalcode"></param>
    ///// <returns></returns>
    //[HttpGet("mosque/postal-code/{postalcode:long}")]
    //public async Task<ActionResult<Result<MosqueAddressFromExternalServiceDto>>> GetAddressByPostalCode([FromRoute] long postalcode) {
    //    return OkResult(await Mediator.Send(new GetAddressByPostalCodeQuery(postalcode)));
    //}

    ///// <summary>لیست شهرها</summary>
    //[HttpGet("cities")]
    //public async Task<ActionResult<Result<CityDto[]>>> GetCities([FromQuery] short? ProvinceId) {
    //    return OkResult(await Mediator.Send(new GetCitiesQuery(ProvinceId)));
    //}

    ///// <summary>دریافت لیست استانها</summary>
    //[HttpGet("provinces")]
    //public async Task<ActionResult<Result<List<ProvinceDto>>>> GetProvinces() {
    //    return OkResult(await Mediator.Send(new GetProvincesQuery()));
    //}

    ///// <summary>لیست شهرستانها</summary>
    //[HttpGet("towns")]
    //public async Task<ActionResult<Result<TownDto[]>>> GetTowns([FromQuery] short? PortionId) {
    //    return OkResult(await Mediator.Send(new GetTownsQuery(PortionId)));
    //}
    ///// <summary>لیست روستاها</summary>
    //[HttpGet("rurals")]
    //public async Task<ActionResult<Result<RuralDto[]>>> GetRurals([FromQuery] short? PortionId) {
    //    return OkResult(await Mediator.Send(new GetRuralsQuery(PortionId)));
    //}

    ///// <summary>لیست استان ها</summary>
    //[HttpGet("portions")]
    //public async Task<ActionResult<Result<PortionDto[]>>> GetPortions([FromQuery] short? CityId) {
    //    return OkResult(await Mediator.Send(new GetPortionsQuery(CityId)));
    //}

    ///// <summary>
    ///// اطلاعات همسر طلبه بر اساس کد مرکز
    ///// </summary>
    ///// <param name="codM"></param>
    ///// <returns></returns>
    //[HttpGet("spouse/{codM}")]
    //public async Task<ActionResult<Result<StudentSpouseDto[]>>> GetSpouse([FromRoute] int codM) {
    //    return OkResult(await Mediator.Send(new GetStudentSpouseByStudentCodmQuery(codM)));
    //}

    ///// <summary>
    ///// بررسی وجود سابقه منبری برای طلبه
    ///// </summary>
    ///// <param name="codM"></param>
    ///// <returns></returns>
    //[HttpGet("has-preach-history")]
    //public async Task<ActionResult<Result<bool>>> HasPreachHistory([FromQuery] int codM) {
    //    return OkResult(await Mediator.Send(new GetStudentPossibilityToCreateMosqueQuery(codM)));
    //}

    ///// <summary>
    ///// گرفتن لیست مساجد بر اساس آدرس
    ///// </summary>
    ///// <returns></returns>
    //[HttpPost("mosque-by-address")]
    //public async Task<ActionResult<Result<List<MosqueAddressDto>>>> GetMosqueByAddress([FromBody] GetMosqueByAddressCommand command) {
    //    return OkResult(await Mediator.Send(command));
    //}
}
