using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.ImamJamaat.Dtos.Commands;
public sealed record MosqueAddressCommandDto : BaseCommandDto<MosqueAddressCommandDto, Address>
{
    /// <inheritdoc/>
    public short? ProvinceId { get; init; }

    /// <summary>شهرستان </summary>
    public short? CityId { get; init; }

    /// <summary>بخش</summary>
    public short? PortionId { get; init; }

    /// <summary>شهر</summary>
    public short? TownId { get; init; }

    /// <summary>دهستان</summary>
    public short? RuralId { get; init; }

    /// <summary>شهرک</summary>
    public string Township { get; init; }

    /// <inheritdoc/>
    public string Village { get; init; }

    /// <summary>محله</summary>
    public string District { get; init; }
    /// <summary>شهرک</summary>
    public string Dorp { get; init; }

    /// <summary>خیابان اصلی</summary>
    public string Avenue { get; init; }

    /// <summary>خیابان فرعی</summary>
    public string Street { get; init; }

    /// <summary>کوچه اصلی</summary>
    public string Alley { get; init; }

    /// <summary>کوچه فرعی</summary>
    public string Lane { get; init; }

    /// <summary>پلاک</summary>
    public string Number { get; init; }

    /// <summary>مجتمع</summary>
    public string Complex { get; init; }

    /// <summary>بلوک</summary>
    public string Block { get; init; }

    /// <summary>واحد</summary>
    public string Unit { get; init; }

    /// <inheritdoc/>
    public short? Floor { get; init; }

    /// <inheritdoc/>
    public long? ZipCode { get; init; }

    /// <summary>همیشه هفت</summary>
    public short ProjectCode { get; set; } = 7;

}
