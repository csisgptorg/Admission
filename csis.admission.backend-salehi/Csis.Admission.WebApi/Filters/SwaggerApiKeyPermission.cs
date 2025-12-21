/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Csis.Admission.WebApi.Filters;

/// <summary>
/// نمایش دسترسی و فیلد کلید دسترسی
/// </summary>
internal sealed class SwaggerApiKeyPermission : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context) {
        var actionAttributes = context.MethodInfo.GetCustomAttributes<CsisAuthorizeApiKeyAttribute>(true);
        var controllerAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes<CsisAuthorizeApiKeyAttribute>(true);
        var allAttributes = controllerAttributes.Concat(actionAttributes).ToArray();

        if ( allAttributes.Length > 0 ) {
            var permissions = new List<string>();
            var headers = new HashSet<string>();

            foreach ( var attribute in allAttributes ) {
                var permission = attribute.GetPermissionCode();
                if ( permission.HasValue ) {
                    permissions.Add(permission.Value.ToString());
                }

                headers.Add(attribute.GetHeaderName());
            }

            if ( permissions.Count > 0 ) {
                operation.Description += $"<div>Required API KEY permissions: {string.Join(", ", permissions)}</div>";
            } else {
                operation.Description += "<div>Requires API KEY</div>";
            }

            operation.Parameters ??= [];

            foreach ( var header in headers ) {
                operation.Parameters.Add(new OpenApiParameter {
                    Name = header,
                    In = ParameterLocation.Header,
                    Description = "Enter api key",
                    Required = true,
                    Schema = new OpenApiSchema {
                        Type = "string"
                    }
                });
            }
        }
    }
}
