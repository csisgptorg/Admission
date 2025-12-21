using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Features.Files.Dtos;
using System.Text.Json.Serialization;

namespace Csis.Admission.Application.Features.Houses.Dtos;

/// <summary>
/// مسکن
/// </summary>
public sealed record HouseDto : BaseDto<HouseDto, House>
{
    /// <summary>
    /// کد مرکز
    /// </summary>
    public int Codm { get; init; }

    /// <summary>
    /// وضعیت سکونت (شخصی، حمایتی، اجاره‌ای/رهنی)
    /// </summary>
    public HouseStatus HouseStatus { get; init; }

    /// <summary>
    /// جزئیات وضعیت سکونت (سازمانی، پدری، منزل همسر، سایر)
    /// </summary>
    public HouseStatusItem? HouseStatusItem { get; init; }

    /// <summary>
    /// توضیح جزئیات وضعیت سکونت (وقتی سایر انتخاب شود)
    /// </summary>
    public string? HouseStatusItemDesc { get; init; }

    /// <summary>
    /// آیا دارای خانه شخصی می‌باشید؟
    /// </summary>
    public bool? HasHouse { get; init; }

    /// <summary>
    /// آیا دارای زمین شخصی می‌باشید؟
    /// </summary>
    public bool? HasLand { get; init; }

    /// <summary>
    /// آیا در حجره یا خوابگاه نیز سکونت دارید؟
    /// </summary>
    public bool? LiveInCell { get; init; }

    /// <summary>
    /// مسکن اجاره ای
    /// </summary>
    public TenantDto Tenant { get; init; }

    /// <summary>
    /// لیست شناسه مدارک
    /// </summary>
    [JsonIgnore]
    public List<Guid> FileIdentifiers { get; init; } = [];

    /// <summary>
    /// لیستی از مشخصات فایل های مدارک
    /// </summary>
    public List<FileModelDto> FilesInfo { get; init; } = [];

    public override void CustomMappings(IMappingExpression<House, HouseDto> mapping) {
        base.CustomMappings(mapping);
        mapping.ForMember(x => x.FileIdentifiers, opt => opt.MapFrom(x => x.Request.Documents.Select(r => r.FileId)));
    }
}
