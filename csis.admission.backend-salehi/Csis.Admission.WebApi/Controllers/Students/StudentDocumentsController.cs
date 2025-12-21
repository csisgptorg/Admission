using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Abstractions.Results;
using Csis.Admission.WebApi.Filters;
using Csis.Admission.Domain.Entities;
using Csis.Admission.Application.Features.Documents.Dtos;
using Csis.Admission.Application.Features.Documents.Queries;
using Csis.Admission.Application.Features.Documents.Commands;

namespace Csis.Admission.WebApi.Controllers;

/// <inheritdoc/>
[Route("/api/private/documents"), CsisAuthorize]
public sealed class StudentDocumentsController : ApiControllerBase
{
    /// <inheritdoc/>
    [HttpPost("search"), CsisAuthorize, DynamicSearch<RequestDocument, long>]
    public async Task<ActionResult<PaginatedResult<RequestDocumentDto>>> Search([FromBody] SearchDocumentsQuery query) {
        var result = await Mediator.Send(query);
        return PaginatedResult(result);
    }

    /// <inheritdoc/>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDocumentCommand command) {
        var result = await Mediator.Send(command);
        return NoContent();
    }
}
