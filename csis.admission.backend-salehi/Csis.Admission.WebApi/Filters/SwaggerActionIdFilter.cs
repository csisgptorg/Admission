/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Utilities.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Csis.Admission.WebApi.Filters;

/// <summary>
/// نمایش شناسه یکتا برای هر اکشن
/// </summary>
internal sealed class SwaggerActionIdFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context) {
        operation.Description += $"<div>UID: {context.ApiDescription.RelativePath.ToSHA1()[24..].ToLower()}</div>";
    }
}
