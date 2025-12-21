using Csis.Admission.Application.Common.Dtos;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Csis.Admission.Application.Features.ImamJamaat.Dtos;

/// <summary>
/// آدرس مسجد
/// </summary>
public sealed record MosqueAddressWithNameDto : BaseDto<MosqueAddressWithNameDto, Mosque>
{
    /// <summary>
    /// نام رسمی مسجد بر اساس مدارک حقوقی
    /// </summary>
    public string OfficialName { get; init; }

    /// <summary>
    /// نام‌های محلی یا نام‌های دیگر رایج در بین مردم
    /// </summary>
    public string LocalNames { get; init; }

    /// <inheritdoc/>
    [JsonPropertyName("MosqueAddressId")]
    public int MosqueAddressId { get; init; }

    /// <inheritdoc/>
    [JsonPropertyName("ProvinceId")]
    public short? MosqueAddressProvinceId { get; init; }

    /// <summary>شهرستان </summary>
    [JsonPropertyName("CityId")]
    public short? MosqueAddressCityId { get; init; }

    /// <summary>بخش</summary>
    [JsonPropertyName("PortionId")]
    public short? MosqueAddressPortionId { get; init; }

    /// <summary>شهر</summary>
    [JsonPropertyName("TownId")]
    public short? MosqueAddressTownId { get; init; }

    /// <summary>دهستان</summary>
    [JsonPropertyName("RuralId")]
    public short? MosqueAddressRuralId { get; init; }

    /// <summary>شهرک</summary>
    [JsonPropertyName("Township")]
    public string MosqueAddressTownship { get; init; }

    /// <inheritdoc/> 
    [JsonPropertyName("Village")]
    public string MosqueAddressVillage { get; init; }

    /// <summary>محله</summary>
        [JsonPropertyName("District")]
    public string MosqueAddressDistrict { get; init; }

    /// <summary>خیابان اصلی</summary>
        [JsonPropertyName("Avenue")]
    public string MosqueAddressAvenue { get; init; }

    /// <summary>خیابان فرعی</summary>
        [JsonPropertyName("Street")]
    public string MosqueAddressStreet { get; init; }

    /// <summary>کوچه اصلی</summary>
    [JsonPropertyName("Alley")]
    public string MosqueAddressAlley { get; init; }

    /// <summary>کوچه فرعی</summary>
    [JsonPropertyName("Lane")]
    public string MosqueAddressLane { get; init; }

    /// <summary>پلاک</summary>
    [JsonPropertyName("Number")]
    public string MosqueAddressNumber { get; init; }

    /// <summary>مجتمع</summary>
    [JsonPropertyName("Complex")]
    public string MosqueAddressComplex { get; init; }

    /// <summary>بلوک</summary>
    [JsonPropertyName("Block")]
    public string MosqueAddressBlock { get; init; }

    /// <summary>واحد</summary>
    [JsonPropertyName("Unit")]
    public string MosqueAddressUnit { get; init; }

    /// <inheritdoc/>
    [JsonPropertyName("Floor")]
    public short? MosqueAddressFloor { get; init; }

    /// <inheritdoc/>
    [JsonPropertyName("ZipCode")]
    public long? MosqueAddressZipCode { get; init; }
}
