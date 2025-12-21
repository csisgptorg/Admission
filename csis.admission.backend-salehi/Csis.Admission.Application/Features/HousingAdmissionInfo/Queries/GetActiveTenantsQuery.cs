using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.HousingAdmissionInfo.Dtos;

namespace Csis.Admission.Application.Features.HousingAdmissionInfo.Queries;

/// <summary>
/// دریافت اجاره نامه های فعال
/// </summary>
/// <param name="Codm">کد مرکز (اختیاری - اگر نباشد از کاربر جاری گرفته می‌شود)</param>
public sealed record GetActiveTenantsQuery(int? Codm) : IRequest<List<ActiveTenantDto>>;

internal sealed class GetActiveTenantsQueryHandler(
    IRepository<Tenant> tenantRepo,
    IDateTimeService dateTimeService,
    ICurrentUserService currentUser)
    : IRequestHandler<GetActiveTenantsQuery, List<ActiveTenantDto>>
{
    public async Task<List<ActiveTenantDto>> Handle(GetActiveTenantsQuery command, CancellationToken cancellationToken) {

        var currentDate = dateTimeService.NowPersian.ToDateOnly().ToPersianInteger();

        var tenants = await tenantRepo.GetAllAsync(x => x.Codm == command.Codm && x.EndDate.HasValue && x.EndDate.Value > currentDate, cancellationToken: cancellationToken);

        var result = tenants.Select(t => new ActiveTenantDto {
            Codm = t.Codm,
            MortgageAmount = t.MortgageAmount,
            RentAmount = t.RentAmount,
            StartDate = t.StartDate.IntDateToString(),
            EndDate = t.EndDate.IntDateToString(),
            IsActive = t.EndDate.HasValue && t.EndDate.Value > currentDate
        }).ToList();

        return result;
    }
}
