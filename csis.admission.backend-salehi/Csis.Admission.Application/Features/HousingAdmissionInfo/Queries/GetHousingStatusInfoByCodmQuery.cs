using Csis.Admission.Application.Features.HousingAdmissionInfo.Dtos;

namespace Csis.Admission.Application.Features.HousingAdmissionInfo.Queries;

/// <summary>
/// دریافت اطلاعات مسکن و اعتراضات برای سامانه مسکن
/// </summary>
/// <param name="Codm">کد مرکز</param>
public sealed record GetHousingStatusInfoByCodmQuery(int Codm) : IRequest<HousingStatusInfoDto>;
internal sealed class GetHousingStatusInfoByCodmQueryHandler(
    IRepository<House> houseRepo,
    IRepository<Protest, long> protestRepo)
    : IRequestHandler<GetHousingStatusInfoByCodmQuery, HousingStatusInfoDto>
{
    public async Task<HousingStatusInfoDto> Handle(GetHousingStatusInfoByCodmQuery request, CancellationToken cancellationToken) {
        // دریافت اطلاعات مسکن
        var house = await houseRepo.GetOneAsync(
            x => x.Codm == request.Codm,
            cancellationToken: cancellationToken);

        // دریافت تمام اعتراضات
        var protests = await protestRepo.GetAllAsync(
            x => x.Codm == request.Codm,
            cancellationToken: cancellationToken);

        // استخراج اعتراضات بر اساس نوع
        var formJProtest = protests.FirstOrDefault(p => p.FieldId == ProtestFormTitle.FormJim);
        var documentRegistrationProtest = protests.FirstOrDefault(p => p.FieldId == ProtestFormTitle.OwnershipHistory);
        var housingBankProtest = protests.FirstOrDefault(p => p.FieldId == ProtestFormTitle.HousingLoanHistory);
        var propertyRentProtest = protests.FirstOrDefault(p => p.FieldId == ProtestFormTitle.BeingLandlord);
        var buyAndSellProtest = protests.FirstOrDefault(p => p.FieldId == ProtestFormTitle.HousingBuySellHistory);
        var personalHousingProtest = protests.FirstOrDefault(p => p.FieldId == ProtestFormTitle.PersonalHousingHistory);

        return new HousingStatusInfoDto {
            // وضعیت مسکن
            HousingStatus = house != null ? house.HouseStatus : null,

            // فرم جیم
            FormJ = formJProtest != null ? ProtestFormTitle.FormJim : ProtestFormTitle.None,

            // ثبت اسناد
            DocumentRegistration = documentRegistrationProtest != null ? ProtestFormTitle.OwnershipHistory : ProtestFormTitle.None,

            // بانک مسکن
            HousingBank = housingBankProtest != null ? ProtestFormTitle.HousingLoanHistory : ProtestFormTitle.None,

            // وضعیت تایید اعتراض بانک مسکن
            HousingBankObjectionStatus = housingBankProtest?.RequestId.HasValue == true ? ProtestFormTitle.HousingLoanHistory : ProtestFormTitle.None,

            // وضعیت اجاره ملک
            PropertyRentStatus = propertyRentProtest != null ? ProtestFormTitle.BeingLandlord : ProtestFormTitle.None,

            // وضعیت اعتراض اجاره
            RentObjectionStatus = propertyRentProtest?.RequestId.HasValue == true ? ProtestFormTitle.BeingLandlord : ProtestFormTitle.None,

            // وضعیت خرید و فروش
            BuyAndSellStatus = buyAndSellProtest != null ? ProtestFormTitle.HousingBuySellHistory : ProtestFormTitle.None,

            // وضعیت لاگ صاحب منزل در پذیرش
            HomeOwnerLogStatus = personalHousingProtest != null ? ProtestFormTitle.PersonalHousingHistory : ProtestFormTitle.None,

            // وضعیت احراز لاگ صاحب منزل
            HomeOwnerLogVerificationStatus = personalHousingProtest?.RequestId.HasValue == true ? ProtestFormTitle.PersonalHousingHistory : ProtestFormTitle.None,
            // سوال پذیرشی دارای منزل هستید
            HasHouse = house?.HasHouse == true,

            // وضعیت سابقه مسکن
            HasHousingHistoryStatus = (buyAndSellProtest != null || propertyRentProtest != null || personalHousingProtest != null) == true
        };
    }
}
