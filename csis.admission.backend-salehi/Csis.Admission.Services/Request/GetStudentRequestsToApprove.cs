using Csis.Admission.Domain.Enums;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Interfaces;

namespace Csis.Admission.Services;

/// <inheritdoc/>
internal sealed partial class RequestService : IRequestService
{
    public async Task<RequestsToApproveDto[]> GetStudentRequestsToApprove(CancellationToken cancellationToken) {
        var codm = int.Parse(await authenticatedUser.GetStudentCodmAsync());
        var requests= await repo.GetAllAsync(x => x.ApprovalStatus == ApprovalStatus.Pending && x.Approvers.Any(y=>y.ApproverCodm== codm),
        x=>x.Approvers, false, cancellationToken);

        var noNeedApprovedRequestIds = requests.Where(x =>x.Approvers.Any(y=>y.Status!=ApprovalStatus.Pending && y.ApproverCodm==codm)).Select(x => x.Id).ToArray();
        return requests.Where(x=>!noNeedApprovedRequestIds.Contains(x.Id)).Select(mapper.Map<RequestsToApproveDto>).ToArray();
    }
}

/// <inheritdoc/>
internal sealed partial class RequestService : IRequestService
{
    public async Task<RequestsToApproveDto[]> GetStudentRequests(CancellationToken cancellationToken) {
        var codm = int.Parse(await authenticatedUser.GetStudentCodmAsync());
        var requests = await repo.GetAllAsync<RequestsToApproveDto>(x=>x.Codm==codm, false, cancellationToken);
        return requests.ToArray();
    }
}
