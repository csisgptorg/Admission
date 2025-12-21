/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Domain.Common;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Csis.Admission.WebApi.Filters;

/// <summary>
/// نمایش لیست فیلدهای قابل فیلتر در اکشن‌های جستجو
/// </summary>
internal sealed class SwaggerFilterableFields : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context) {
        var myAttributes = context.MethodInfo.GetCustomAttributes(true)
            .Where(attr => attr.GetType().IsGenericType &&
                (attr.GetType().GetGenericTypeDefinition() == typeof(DynamicSearchAttribute<>) ||
                    attr.GetType().GetGenericTypeDefinition() == typeof(DynamicSearchAttribute<,>)));

        foreach ( var attribute in myAttributes ) {
            var entityType = attribute.GetType().GetGenericArguments()[0];
            var entityInstance = Activator.CreateInstance(entityType) as IFilterable;

            var fields = entityInstance.GetFilterableFields();

            operation.Description += $"<div>Filterable fields: {string.Join(", ", fields.Select(x => x.ToLower()))}</div>";
        }
    }
}
