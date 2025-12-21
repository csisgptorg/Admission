/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Csis.Admission.Services;

internal sealed class IpAddressService(IServiceProvider serviceProvider) : IIpAddressService
{
    /// <inheritdoc/>
    public string GetIpAddress() {
        var contextAccessor = serviceProvider.GetService<IHttpContextAccessor>();

        if ( contextAccessor is null || contextAccessor.HttpContext is null ) {
            return string.Empty;
        }

        if ( contextAccessor.HttpContext.Request.Headers.TryGetValue("X-Forwarded-For", out var value) ) {
            return value;
        } else {
            return contextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
