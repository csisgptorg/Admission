using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Features.Settings.People.Dtos;

namespace Csis.Admission.Application.Features.Settings.People.Queries;

/// <summary>
/// دریافت نوع ثبت استعلام
/// </summary>
/// <param name="SettingTitle"></param>
public sealed record GetSettingsQuery() : IRequest<List<SettingDto>>;
internal sealed class GetSettingsQueryHandler(IRepository<Setting> settingRepository) : IRequestHandler<GetSettingsQuery, List<SettingDto>>
{
    public async Task<List<SettingDto>> Handle(GetSettingsQuery request, CancellationToken cancellationToken) {
        var setting = await settingRepository.GetAllAsync<SettingDto>(x=>x.Description == "People");
        if ( setting == null ) {
            throw new CommandValidationException("تنظیمات سیستم یافت نشد");
        }
        return setting;
    }
}
