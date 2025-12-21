/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.WebApi.Controllers;

/// <summary>
/// Base api controller
/// </summary>
[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    private IMediator _mediator;

    /// <summary>
    /// Get a mediator instance
    /// </summary>
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

    /// <summary>
    /// Return HTTP Ok with success <see cref="Result"/> structure
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    protected virtual OkObjectResult OkResult(object value) {
        return Ok(value?.ToResult() ?? Result<object>.Success(null));
    }

    /// <summary>
    /// Return HTTP Ok with success <see cref="Abstractions.Results.PaginatedResult{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    protected virtual OkObjectResult PaginatedResult<T>(IPagedList<T> list) where T : class {
        return Ok(list?.ToPaginatedResult());
    }

    /// <summary>
    /// Return HTTP Ok with fail <see cref="Result"/> structure
    /// </summary>
    /// <param name="message">Error message</param>
    /// <returns></returns>
    protected virtual OkObjectResult FailResult(string message = null) {
        return Ok(Result.Fail(message));
    }

    /// <summary>
    /// Return json with fail <see cref="Result"/> structure and specified status code
    /// </summary>
    /// <param name="message">Error message</param>
    /// <param name="statusCode">HTTP status code</param>
    /// <returns></returns>
    protected virtual JsonResult FailResult(string message = null, int statusCode = 200) {
        return new JsonResult(Result.Fail(message, statusCode)) { StatusCode = statusCode };
    }
}
