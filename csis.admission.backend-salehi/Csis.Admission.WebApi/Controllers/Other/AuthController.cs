using Csis.Abstractions.Results;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Auth.Commands;
using Csis.Admission.Application.Features.Auth.Dtos;
using Csis.Authorization.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers;

/// <summary>
/// عملیات مرتبط با احراز هویت کاربران
/// </summary>
[Route("/api/public/auth")]
public sealed class AuthController : ApiControllerBase
{
    private readonly ICsisAuthorizationService _csisAuthorizationService;
    private readonly IStudentRepository _studentRepository;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="csisAuthorizationService"></param>
    /// <param name="studentRepository"></param>
    public AuthController(ICsisAuthorizationService csisAuthorizationService, IStudentRepository studentRepository) {
        _csisAuthorizationService = csisAuthorizationService;
        _studentRepository = studentRepository;
    }

    /// <summary>
    /// احراز هویت کاربران و ورود به سامانه
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<Result<LoginResultDto>>> Login([FromBody] LoginCommand command) {
        var loginResult = await Mediator.Send(command);

        if ( loginResult.Succeeded ) {
            return OkResult(loginResult);
        } else {
            return FailResult(loginResult.ErrorMessage, StatusCodes.Status401Unauthorized);
        }
    }

    /// <summary>
    /// رفرش توکن
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public async Task<ActionResult<Result<LoginResultDto>>> RefreshToken([FromBody] RefreshTokenCommand command) {
        var loginResult = await Mediator.Send(command);

        if ( loginResult.Succeeded ) {
            return OkResult(loginResult);
        } else {
            return FailResult(loginResult.ErrorMessage, StatusCodes.Status401Unauthorized);
        }
    }

    /// <summary>
    /// احراز هویت طلاب و ورود به سامانه
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("login-student")]
    [AllowAnonymous]
    public async Task<ActionResult<Result<LoginResultDto>>> LoginStudent([FromBody] LoginStudentCommand command) {
        var loginResult = await Mediator.Send(command);

        if ( loginResult.Succeeded ) {
            return OkResult(loginResult);
        } else {
            return FailResult(loginResult.ErrorMessage, StatusCodes.Status401Unauthorized);
        }
    }

    /// <summary>
    /// دریافت اطلاعات کاربر جاری
    /// </summary>
    /// <returns></returns>
    [HttpGet("user-info")]
    public async Task<IActionResult> GetUserInfo() {
        var userInfo = await _csisAuthorizationService.GetUserInfoAsync();
        if ( userInfo?.Succeeded ?? false ) {
            return Ok(userInfo);
        }

        return BadRequest();
    }

    /// <summary>
    /// خروج کاربر از سامانه
    /// </summary>
    /// <returns></returns>
    [HttpPost("logout")]
    public async Task<IActionResult> Logout() {
        try {
            await _csisAuthorizationService.LogoutAsync();
            return Ok();
        } catch {
            return BadRequest();
        }
    }

    /// <summary>
    /// همگام سازی لیست دسترسی های تعریف شده با آیدنتیتی سرور
    /// </summary>
    /// <returns></returns>
    [HttpGet("sync-permissions")]
    public async Task<IActionResult> SyncPermissions() {
        var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        if ( !configuration.GetValue<bool>("IdentityServerOptions:EnableDeveloperMode") ) {
            return NotFound();
        }

        var result = await _csisAuthorizationService.SyncAllPermissionsAsync<PermissionsEnum>();

        return Ok(result);
    }

    /// <summary>آماده سازی دیتا برای انجام تست آزمایشی</summary>
    [HttpGet("prepare-test-data")]
    public async Task<IActionResult> PrepareTestData() {

        await _studentRepository.PrepareTestData();
        return NoContent();
    }
}
