/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Features.Settings.Commands;
using Csis.Admission.Domain.Settings;

namespace Csis.Admission.Application.Features.Settings.Validators;
public sealed class SaveNotificationSettingsCommandValidator : BaseValidator<SaveNotificationSettingsCommand>
{
    public SaveNotificationSettingsCommandValidator() {
        RuleFor(x => x.NotificationSettings)
            .NotNull()
            .SetValidator(new NotificationSettingsValidator());
    }
}

public sealed class NotificationSettingsValidator : BaseValidator<NotificationSettings>
{
    public NotificationSettingsValidator() {
        RuleFor(x => x.NotificationSenderServiceBatchSize)
            .GreaterThanOrEqualTo(10)
            .LessThanOrEqualTo(500)
            .WithName("تعداد نوتیفیکیشن پردازش شده در هر اجرای بک گراند سرویس");

        RuleFor(x => x.NotificationSenderServiceIntervalInSeconds)
            .GreaterThanOrEqualTo(30)
            .LessThanOrEqualTo(1800)
            .WithName("فاصله زمانی اجرای بک گراند سرویس ارسال نوتیفیکیشن (بر حسب ثانیه)");

        RuleFor(x => x.MaxTryPerNotification)
            .GreaterThanOrEqualTo(3)
            .LessThanOrEqualTo(50)
            .WithName("حداکثر تعداد تلاش برای ارسال هر نوتیفیکیشن");
    }
}
