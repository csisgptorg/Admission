using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Addresses.Dtos;

/// <inheritdoc/>
public sealed record AddressDto : BaseDto<AddressDto, Address>
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public short Province { get; set; }

    /// <summary>شهرستان</summary>
    public short City { get; set; }

    /// <summary>بخش</summary>
    public short Portion { get; set; }

    /// <summary>شهر</summary>
    public short Town { get; set; }

    /// <summary>دهستان</summary>
    public short Rural { get; set; }

    /// <summary>شهرک</summary>
    public string Township { get; set; }

    /// <inheritdoc/>
    public string Village { get; set; }

    /// <summary>محله</summary>
    public string District { get; set; }

    /// <summary>خیابان اصلی</summary>
    public string Avenue { get; set; }

    /// <summary>خیابان فرعی</summary>
    public string Street { get; set; }

    /// <summary>کوچه اصلی</summary>
    public string Alley { get; set; }

    /// <summary>کوچه فرعی</summary>
    public string Lane { get; set; }

    /// <summary>پلاک</summary>
    public string Number { get; set; }

    /// <summary>مجتمع</summary>
    public string Complex { get; set; }

    /// <summary>شماره بلوک</summary>
    public string Block { get; set; }

    /// <summary>واحد</summary>
    public string Unit { get; set; }

    /// <inheritdoc/>
    public short? Floor { get; set; }

    /// <inheritdoc/>
    public long? ZipCode { get; set; }

    /// <inheritdoc/>
    public string ConfirmDate { get; set; }

    /// <summary>همیشه یک</summary>
    public short ProjectCode { get; set; }

    /// <summary>همیشه یک</summary>
    public bool? Flag { get; set; }

    /// <inheritdoc/>
    public override void CustomMappings(IMappingExpression<Address, AddressDto> mapping) {
        mapping.ForMember(dto => dto.ConfirmDate, config => config.MapFrom(model => model.ConfirmDate.IntDateToString()));
        mapping.ForMember(x => x.Province, config => config.MapFrom(model => model.ProvinceId));
        mapping.ForMember(x => x.City, config => config.MapFrom(model => model.CityId));
        mapping.ForMember(x => x.Portion, config => config.MapFrom(model => model.PortionId));
        mapping.ForMember(x => x.Town, config => config.MapFrom(model => model.TownId));
        mapping.ForMember(x => x.Rural, config => config.MapFrom(model => model.RuralId));

    }
}
