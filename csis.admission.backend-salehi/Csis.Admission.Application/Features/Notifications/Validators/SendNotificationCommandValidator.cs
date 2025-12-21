using FluentValidation;
using Csis.Admission.Application.Features.Notifications.Commands;

namespace Csis.Admission.Application.Features.Notifications.Validators;

/// <summary>اعتبارسنجی</summary>
public sealed class SendNotificationCommandValidator : BaseValidator<SendNotificationCommand>
{
    public SendNotificationCommandValidator() {

        RuleFor(x => x.Message)
            .MinimumLength(20).WithMessage("پیام نمی‌تواند کمتر از ۲۰ کاراکتر باشد.")
            .MaximumLength(134).WithMessage("پیام نمی‌تواند بیش از ۱۳۴ کاراکتر باشد.");

        RuleFor(x => x.Codm).GreaterThan(0).WithMessage("کد مرکز خدمات نامعتبر است.");
    }
}
