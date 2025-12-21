using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.Application.Common.Configuration;
using Csis.Admission.Application.Features.Files.Commands;

namespace Csis.Admission.WebApi.Controllers.Other;

/// <inheritdoc/>
[Route("/api/public/files"),Tags("Files")]
public sealed class FilesControllerPublic : ApiControllerBase
{
    /// <inheritdoc/>
    [HttpPost("upload"), CsisAuthorizeStudent]
    public async Task<ActionResult<Result<Guid>>> Upload([FromForm] UploadFileCommand command) {
        if ( !GlobalOptions.AllowFileUpload ) {
            return NotFound();
        }

        return OkResult(await Mediator.Send(command));
    }

    /// <inheritdoc/>
    [HttpPost("case-filing/upload")]
    public async Task<ActionResult<Result<Guid>>> CaseFiling([FromForm] CreateCaseFillingFileUploadCommand command) {
        if ( !GlobalOptions.AllowFileUpload ) {
            return NotFound();
        }

        return OkResult(await Mediator.Send(command));
    }
}
