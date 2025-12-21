/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Abstractions.Results;
using Csis.Admission.Application.Common.Configuration;
using Csis.Admission.Application.Features.Files.Commands;
using Csis.Admission.Application.Features.Files.Dtos;
using Csis.Admission.Application.Features.Files.Queries;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Csis.Admission.WebApi.Controllers.Other;

/// <summary>
/// مدیریت فایل‌ها
/// </summary>
[Route("/api/private/files")]
public sealed class FilesController : ApiControllerBase
{
    /// <summary>
    /// بارگذاری فایل
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("upload"), CsisAuthorize]
    public async Task<ActionResult<Result<Guid>>> Upload([FromForm] UploadFileCommand command) {
        if ( !GlobalOptions.AllowFileUpload ) {
            return NotFound();
        }

        return OkResult(await Mediator.Send(command));
    }

    [HttpGet("get-file")]
    public async Task<ActionResult<Result<FileModelDto>>> GetFile([FromQuery] Guid fileIdentifier) {
        if ( !GlobalOptions.AllowFileUpload ) {
            return NotFound();
        }
        var result = await Mediator.Send(new GetFileQuery(fileIdentifier));
        return OkResult(result);
    }
}
