using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Csis.Admission.WebApi.Filters;

/// <inheritdoc/>
public class ApiKeyHeaderOperationFilter : IOperationFilter
{
    /// <inheritdoc/>
    public void Apply(OpenApiOperation operation, OperationFilterContext context) {

        var actionAttribute = context.MethodInfo.GetCustomAttributes<ApiKeyHeaderAttribute>(true);
        var controllerAttribute = context.MethodInfo.DeclaringType.GetCustomAttributes<ApiKeyHeaderAttribute>(true);

        if(actionAttribute.Any() || controllerAttribute.Any() ) {
            operation.Parameters.Add(new OpenApiParameter {
                Name = "X-Api-Key",
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema { Type = "string" },
                Required = false
            });
        }
    }
}

/// <inheritdoc/>
public sealed class ApiKeyHeaderAttribute : Attribute { }
