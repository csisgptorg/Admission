using Csis.Notification;

namespace Csis.Admission.Application.Features.Notifications.Commands;

/// <summary>ارسال پیام</summary>
public sealed record SendNotificationCommand(string Message, int Codm) : IRequest<long>;

internal sealed class SendNotificationCommandHandler(ICsisNotificationService notificationService)
    : IRequestHandler<SendNotificationCommand, long>
{
    public async Task<long> Handle(SendNotificationCommand command, CancellationToken cancellation) {
        var notifCommand = new SendMessageToStudent(command.Message, [command.Codm], [DeliveryChannelEnum.Sms]);
        return await notificationService.SendMessageToStudent(notifCommand, cancellation);
    }
}
