using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Common.Interfaces;

/// <inheritdoc/>
public interface IHttpRequestService
{
    /// <inheritdoc/>
    Task<HttpRequestResult<TApiResult>> SendAsync<TApiResult>(HttpRequestSectionOptions sectionOption,HttpRequestMessage request, 
        CancellationToken cancellationToken, AuthenticationHeaderValue authenticationHeader = null, [CallerMemberName] string callMember = "");
}
