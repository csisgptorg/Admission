using Csis.Notification;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Csis.Admission.Application.Features.Notifications.Commands;
using Csis.Abstractions.Results;

namespace Csis.Admission.WebApi.Controllers.Students;

/// <summary>پیام رسان</summary>
[Route("/api/private/notification"), Tags("StudentNotification")]
public sealed class StudentNotificationController(ICsisNotificationAdvancedService notificationService) : ApiControllerBase
{
    /// <summary>ارسال پیام</summary>
    [HttpPost, CsisAuthorize]
    public async Task<ActionResult<Result<long>>> SendMessage([FromBody] SendNotificationCommand command) {
        return OkResult(await Mediator.Send(command));
    }

    /// <summary>لیست</summary>
    [HttpPost("search"), CsisAuthorize]
    public async Task<Notification.PaginatedResult<RecipientDto>> Search([FromBody] SearchRecipientsQuery query) {
        return await notificationService.SearchRecipients(query,CancellationToken.None);
    }
}
