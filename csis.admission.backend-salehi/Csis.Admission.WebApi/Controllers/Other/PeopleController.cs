using Csis.Abstractions.Results;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.People.Commands;
using Csis.Admission.Application.Features.People.Dtos;
using Csis.Admission.Application.Features.People.Queries;
using Csis.Admission.Domain.Entities;
using Csis.Admission.WebApi.Filters;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Other;

/// <summary>
/// مدیریت موجودیت شخص
/// </summary>
[Route("/api/private/people")]
public sealed class PeopleController : ApiControllerBase
{
    /// <summary>
    /// جستجوی موجودیت شخص
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost("search"), CsisAuthorize(PermissionsEnum.SearchPeople)]
    [DynamicSearch<Person>]
    public async Task<ActionResult<PaginatedResult<PersonDto>>> Search([FromBody] SearchPeopleQuery query) {
        var result = await Mediator.Send(query);
        return PaginatedResult(result);
    }

    /// <summary>
    /// دریافت موجودیت شخص با شناسه
    /// </summary>
    /// <returns></returns>
    [HttpGet("{queryParam}"), CsisAuthorize(PermissionsEnum.ViewPerson)]
    public async Task<ActionResult<Result<PersonDto>>> GetById([FromRoute] string queryParam) {
        return OkResult(await Mediator.Send(new GetPersonByIdQuery(queryParam)));
    }

    /// <summary>
    /// ایجاد موجودیت شخص جدید - ایرانی از طریق استعلام
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("iranian-auto"), CsisAuthorize(PermissionsEnum.CreatePerson)]
    public async Task<ActionResult<Result<int>>> CreateIranianPersonByInquiry([FromBody] CreateIranianPersonByInquiryCommand command) {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { queryParam = result }, Result<int>.Success(result));
    }

    /// <summary>
    /// ایجاد موجودیت شخص جدید - ایرانی به صورت دستی
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("iranian-manual"), CsisAuthorize(PermissionsEnum.CreatePerson)]
    public async Task<ActionResult<Result<int>>> CreateIranianPersonManually([FromBody] CreateIranianPersonManuallyCommand command) {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { queryParam = result }, Result<int>.Success(result));
    }

    /// <summary>
    /// ایجاد موجودیت شخص جدید - غیرایرانی از طریق استعلام
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("non-iranian-auto"), CsisAuthorize(PermissionsEnum.CreatePerson)]
    public async Task<ActionResult<Result<int>>> CreateNonIranianPersonByInquiry([FromBody] CreateNonIranianPersonByInquiryCommand command) {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { queryParam = result }, Result<int>.Success(result));
    }

    /// <summary>
    /// ایجاد موجودیت شخص جدید - غیرایرانی به صورت دستی
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("non-iranian-manual"), CsisAuthorize(PermissionsEnum.CreatePerson)]
    public async Task<ActionResult<Result<int>>> CreateNonIranianPersonManually([FromBody] CreateNonIranianPersonManuallyCommand command) {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { queryParam = result }, Result<int>.Success(result));
    }

    /// <summary>
    /// ویرایش موجودیت شخص
    /// </summary>
    /// <param name="id">شناسه موجودیت شخص</param>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("{id:min(1)}"), CsisAuthorize(PermissionsEnum.UpdatePerson)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePersonCommand command) {
        if ( id != command.Id ) {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// حذف موجودیت شخص
    /// </summary>
    /// <param name="id">شناسه موجودیت شخص</param>
    /// <returns></returns>
    [HttpDelete("{id:min(1)}"), CsisAuthorize(PermissionsEnum.DeletePerson)]
    public async Task<IActionResult> Delete([FromRoute] int id) {
        await Mediator.Send(new DeletePersonCommand(id));
        return NoContent();
    }

    /// <summary>
    /// ثبت نسبت خانوادگی برای شخص (والدین و فرزندان)
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("assign-relation"), CsisAuthorize(PermissionsEnum.PersonAssignRelation)]
    public async Task<ActionResult<Result<PersonDto>>> AssignRelation([FromBody] AssignParentChildRelationCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// ثبت نسبت همسر برای شخص
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("assign-spouse"), CsisAuthorize(PermissionsEnum.PersonAssignSpouse)]
    public async Task<ActionResult<Result<PersonDto>>> AssignSpouse([FromBody] AssignSpousalRelationCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// ثبت نسبت همسر برای شخص غیرایرانی
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("assign-non-iranian-spouse"), CsisAuthorize(PermissionsEnum.PersonAssignSpouse)]
    public async Task<ActionResult<Result<PersonDto>>> AssignNonIranianSpouse([FromBody] AssignNonIranianSpousalRelationCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// ثبت نسبت والدین برای شخص غیرایرانی
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("assign-non-iranian-parent-child"), CsisAuthorize(PermissionsEnum.PersonAssignRelation)]
    public async Task<ActionResult<Result<PersonDto>>> AssignNonIranianParentChild([FromBody] AssignNonIranianParentChildRelationCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// دریافت موجودیت شخص با کد ملی
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("by-national-code"), CsisAuthorize(PermissionsEnum.GetPersonByNationalCode)]
    public async Task<ActionResult<Result<GetIdentityInfoByNationalCodeResponse>>> GetByNationalCode([FromBody] ValidateIranianPersonIdentityCommand command) {

        var foundedResult = await Mediator.Send(new GetPersonByNationalCodeQuery(command.NationalCode, command.BirthDate));
        if ( foundedResult != null ) {
            return OkResult(foundedResult);
        }

        foundedResult = await Mediator.Send(new ValidateIranianPersonIdentityCommand(command.NationalCode, command.BirthDate));
        return OkResult(foundedResult);
    }

    /// <summary>
    /// اعتبارسنجی هویت غیرایرانی (شناسه یکتا) قبل از ایجاد/بروزرسانی
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("validate-non-iranian-identity"), CsisAuthorize(PermissionsEnum.ValidateNonIranianPersonIdentity)]
    public async Task<ActionResult<Result<ValidateNonIranianYektaCodeResponse>>> ValidateNonIranianIdentity([FromBody] ValidateNonIranianPersonIdentityCommand command) {
        var result = await Mediator.Send(new GetPersonByYektaCodeQuery(command.YektaCode));
        if ( result != null ) {
            return OkResult(result);
        }
        result = await Mediator.Send(command);
        return OkResult(result);
    }

    /// <summary>
    /// اعتبارسنجی شماره همراه شخص
    /// </summary>
    /// <param name="command">درخواست اعتبارسنجی شماره همراه</param>
    /// <returns>نتیجه اعتبارسنجی</returns>
    [HttpPost("validate-mobile"), CsisAuthorize(PermissionsEnum.ValidatePersonMobileNumber)]
    public async Task<ActionResult<Result<bool>>> ValidateMobile([FromBody] ValidatePersonMobileCommand command) {
        var result = await Mediator.Send(command);
        return OkResult(result);
    }

    /// <summary>
    /// به‌روزرسانی تصویر شخص
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("update-person-image"), CsisAuthorize(PermissionsEnum.UpdatePersonImage)]
    public async Task<ActionResult<Result>> UpdatePersonImage([FromBody] UpdatePersonImageCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// اعتبار سنجی مالکیت شبا شخص
    /// </summary>
    [HttpPost("validate-sheba-ownership"), CsisAuthorize(PermissionsEnum.ValidateShebaOwnership)]
    public async Task<ActionResult<Result<ValidateShebaOwnershipResponse>>> ValidateShebaOwnership([FromBody] ValidateShebaOwnershipCommand command) {
        var result = await Mediator.Send(command);
        return OkResult(result);
    }
}
