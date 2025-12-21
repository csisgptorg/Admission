using Csis.Admission.Application.Common.Interfaces.Repositories;

namespace Csis.Admission.Application.Features.Settings.People.Commands;

/// <summary>تغییر نوع ثبت شماره </summary>
/// <param name="RegistrationType"> نوع استعلام</param>
/// <param name="SettingTitle"> عنوان تنظیمات وب سرویس.</param>
public sealed record CreateOrUpdateSettingsCommand(RegistrationType RegistrationType, WebServiceSettingTitle SettingTitle) : IRequest;
internal sealed class CreateOrUpdateSettingsCommandHandler(ISettingRepository settingRepository) : IRequestHandler<CreateOrUpdateSettingsCommand>
{
    public async Task Handle(CreateOrUpdateSettingsCommand request, CancellationToken cancellationToken) {

        var setting = await settingRepository.GetByKeyAsync(request.SettingTitle.GetEnumDisplayName());

        if ( setting == null ) {
            var newSetting = new Setting {
                Key = request.SettingTitle.GetEnumDisplayName(),
                Value = ((int) request.RegistrationType).ToString(),
                Version = 1,
                Description = "People"
            };

            await settingRepository.InsertAsync(newSetting);
        } else {
            setting.Value = ((int) request.RegistrationType).ToString();
            setting.Version++;
            await settingRepository.UpdateAsync(setting);
        }
    }
}
