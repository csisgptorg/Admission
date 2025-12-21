using Csis.Admission.Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csis.Admission.Application.Features.Settings.People.Dtos;

public sealed record SettingDto : BaseDto<SettingDto, Setting>
{
    public RegistrationType RegistrationType { get; init; }
    public WebServiceSettingTitle SettingTitle { get; init; }

    public override void CustomMappings(IMappingExpression<Setting, SettingDto> mapping) {
        base.CustomMappings(mapping);
        mapping.ForMember(x => x.RegistrationType, opt => opt.MapFrom(src => Enum.Parse<RegistrationType>(src.Value)));
        mapping.ForMember(x => x.SettingTitle, opt => opt.MapFrom(src => Enum.Parse<WebServiceSettingTitle>(src.Key)));
    }
}
