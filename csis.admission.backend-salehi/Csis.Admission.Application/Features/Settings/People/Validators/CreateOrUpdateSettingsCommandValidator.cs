using Csis.Admission.Application.Features.Settings.People.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.Settings.People.Validators;

// validation
internal sealed class CreateOrUpdateSettingsCommandValidator : AbstractValidator<CreateOrUpdateSettingsCommand>
{
    public CreateOrUpdateSettingsCommandValidator()
    {
        RuleFor(x => x.RegistrationType)
            .IsInEnum()
            .WithMessage("نوع استعلام معتبر نمی باشد.");
        RuleFor(x => x.SettingTitle)
            .IsInEnum()
            .WithMessage("عنوان تنظیمات وب سرویس معتبر نمی باشد.");
    }
}
