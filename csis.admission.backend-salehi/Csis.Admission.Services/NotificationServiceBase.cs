/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Domain.Enums;
using Csis.Utilities.Extensions;
using System.Text.RegularExpressions;

namespace Csis.Admission.Services;
internal sealed partial class NotificationService(INotificationRepository notificationRepo) : INotificationService
{
    public string ProcessTemplate(string template, Dictionary<string, string> parameters) {
        return Placeholder().Replace(template, match => {
            var placeholderName = match.Groups[1].Value;
            var found = parameters.TryGetValue(placeholderName, out var parameter);
            if ( !found ) {
                return null;
            }

            return parameter;
        });
    }

    public async Task<int> SendToStudentAsync(int codm, string message, List<int> deliveryChannels, NotificationType type, NotificationPriority priority = NotificationPriority.Normal, DateTime? scheduleDate = null, CancellationToken cancellationToken = default) {
        if ( !message.HasValue() ) {
            throw new ArgumentException($"'{nameof(message)}' cannot be null or whitespace.", nameof(message));
        }

        if ( codm <= 0 ) {
            throw new ArgumentException($"'{nameof(codm)}' must be greater than zero.", nameof(codm));
        }

        return await SendNotification(
            type,
            priority,
            codm: codm,
            message: message,
            deliveryChannels: deliveryChannels,
            scheduleDate: scheduleDate,
            cancellationToken: cancellationToken
        );
    }

    public async Task<int> SendToStudentTemplateAsync(int codm, string template, List<int> deliveryChannels, NotificationType type, Dictionary<string, string> parameters, NotificationPriority priority = NotificationPriority.Normal, DateTime? scheduleDate = null, CancellationToken cancellationToken = default) {
        if ( !template.HasValue() ) {
            throw new ArgumentException($"'{nameof(template)}' cannot be null or whitespace.", nameof(template));
        }

        if ( codm <= 0 ) {
            throw new ArgumentException($"'{nameof(codm)}' must be greater than zero.", nameof(codm));
        }

        return await SendNotification(
            type,
            priority,
            codm: codm,
            message: ProcessTemplate(template, parameters),
            template: template,
            deliveryChannels: deliveryChannels,
            scheduleDate: scheduleDate,
            cancellationToken: cancellationToken
        );
    }

    public async Task<int> SendToEmployeeAsync(int personnelId, string message, List<int> deliveryChannels, NotificationType type, NotificationPriority priority = NotificationPriority.Normal, DateTime? scheduleDate = null, CancellationToken cancellationToken = default) {
        if ( !message.HasValue() ) {
            throw new ArgumentException($"'{nameof(message)}' cannot be null or whitespace.", nameof(message));
        }

        if ( personnelId <= 0 ) {
            throw new ArgumentException($"'{nameof(personnelId)}' must be greater than zero.", nameof(personnelId));
        }

        return await SendNotification(
            type,
            priority,
            personnelId: personnelId,
            message: message,
            deliveryChannels: deliveryChannels,
            scheduleDate: scheduleDate,
            cancellationToken: cancellationToken
        );
    }

    public async Task<int> SendToEmployeeTemplateAsync(int personnelId, string template, List<int> deliveryChannels, NotificationType type, Dictionary<string, string> parameters, NotificationPriority priority = NotificationPriority.Normal, DateTime? scheduleDate = null, CancellationToken cancellationToken = default) {
        if ( !template.HasValue() ) {
            throw new ArgumentException($"'{nameof(template)}' cannot be null or whitespace.", nameof(template));
        }

        if ( personnelId <= 0 ) {
            throw new ArgumentException($"'{nameof(personnelId)}' must be greater than zero.", nameof(personnelId));
        }

        return await SendNotification(
            type,
            priority,
            personnelId: personnelId,
            message: ProcessTemplate(template, parameters),
            template: template,
            deliveryChannels: deliveryChannels,
            scheduleDate: scheduleDate,
            cancellationToken: cancellationToken
        );
    }

    private async Task<int> SendNotification(
        NotificationType type,
        NotificationPriority priority,
        int? codm = null,
        int? personnelId = null,
        string template = null,
        string message = null,
        List<int> deliveryChannels = null,
        DateTime? scheduleDate = null,
        CancellationToken cancellationToken = default) {
        if ( deliveryChannels is null || deliveryChannels.Count == 0 ) {
            throw new ArgumentException("Provide at least one delivery channel", nameof(deliveryChannels));
        }

        var entity = new Domain.Entities.Notification {
            Codm = codm,
            PersonnelId = personnelId,
            DeliveryChannels = deliveryChannels,
            Type = type,
            Priority = priority,
            Template = template,
            Message = message,
            ScheduleDate = scheduleDate
        };
        await notificationRepo.InsertAsync(entity, cancellationToken: cancellationToken);

        return entity.Id;
    }

    [GeneratedRegex(@"\{\{(.*?)\}\}")]
    private partial Regex Placeholder();

    public bool ValidateTemplate(string template) {
        return Placeholder().Matches(template).All(x => GetValidParamNames().Contains(x.Groups[1].Value));
    }
}
