using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Features.Houses.Dtos;

namespace Csis.Admission.Application.Features.Houses.Commands;

/// <summary>
/// ایجاد یا ویرایش مسکن طلبه
/// </summary>
public sealed record CreateOrUpdateHouseCommand : BaseCommandDto<CreateOrUpdateHouseCommand, House>, IRequest<int>
{
    /// <summary>
    /// کد مرکز
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// وضعیت سکونت (شخصی، حمایتی، اجاره‌ای/رهنی)
    /// </summary>
    public HouseStatus HouseStatus { get; init; }

    /// <summary>
    /// جزئیات وضعیت سکونت (سازمانی، پدری، منزل همسر، سایر)
    /// </summary>
    public HouseStatusItem? HouseStatusItem { get; init; }

    /// <summary>
    /// (توضیح جزئیات وضعیت سکونت (وقتی سایر انتخاب شود
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
    public TenantDto? Tenant { get; init; }

    /// <inheritdoc/>
    public long? RequestId { get; init; }
}

internal sealed class CreateOrUpdateHouseCommandHandler(
    IRepository<House> houseRepo,
    IRepository<Tenant> tenantRepo)
    : IRequestHandler<CreateOrUpdateHouseCommand, int>
{
    public async Task<int> Handle(CreateOrUpdateHouseCommand request, CancellationToken cancellationToken)
    {
        var house = await houseRepo.GetOneAsTrackingAsync(x => x.Codm == request.Codm, cancellationToken: cancellationToken);

        int houseId;
        
        if (house is null)
        {
            var newHouse = request.ToEntity();
            await houseRepo.InsertAsync(newHouse, cancellationToken: cancellationToken);
            houseId = newHouse.Id;
        }
        else
        {
            await houseRepo.UpdateAsync(request.ToEntity(house), cancellationToken: cancellationToken);
            houseId = house.Id;
        }

        // مدیریت Tenant از طریق Codm
        if (request.Tenant != null)
        {
            var existingTenant = await tenantRepo.GetOneAsTrackingAsync(x => x.Codm == request.Codm, cancellationToken: cancellationToken);
            
            var tenantEntity = request.Tenant.ToEntity();
            tenantEntity.Codm = request.Codm;

            if (existingTenant is null)
            {
                await tenantRepo.InsertAsync(tenantEntity, cancellationToken: cancellationToken);
            }
            else
            {
                await tenantRepo.UpdateAsync(request.Tenant.ToEntity(existingTenant), cancellationToken: cancellationToken);
            }
        }

        return houseId;
    }
}
