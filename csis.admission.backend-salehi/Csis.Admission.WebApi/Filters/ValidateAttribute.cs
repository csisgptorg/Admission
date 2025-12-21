/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Microsoft.AspNetCore.Mvc.Filters;

namespace Csis.Admission.WebApi.Filters;

/// <summary>
/// Attribute to apply <see cref="FluentValidationActionFilter"/> to controllers or action methods"/>
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public sealed class ValidateAttribute : Attribute, IFilterFactory
{
    /// <inheritdoc/>
    public bool IsReusable => false;

    /// <inheritdoc/>
    public IFilterMetadata CreateInstance(IServiceProvider serviceProvider) {
        return serviceProvider.GetRequiredService<FluentValidationActionFilter>();
    }
}
