namespace Csis.Admission.Application.Features.HousingAdmissionInfo.Dtos;

/// <summary>
/// اطلاعات مسکن و اعتراضات برای سامانه مسکن
/// </summary>
public sealed class HousingStatusInfoDto
{
    /// <summary>
    /// وضعیت مسکن 
    /// </summary>
    public HouseStatus? HousingStatus { get; init; }
    
    /// <summary>
    /// فرم جیم 
    /// </summary>
    public ProtestFormTitle? FormJ { get; init; }
    
    /// <summary>
    /// ثبت اسناد 
    /// </summary>
    public ProtestFormTitle? DocumentRegistration { get; init; }
    
    /// <summary>
    /// بانک مسکن 
    /// </summary>
    public ProtestFormTitle? HousingBank { get; init; }
    
    /// <summary>
    /// وضعیت تایید اعتراض بانک مسکن 
    /// </summary>
    public ProtestFormTitle? HousingBankObjectionStatus { get; init; }
    
    /// <summary>
    /// وضعیت اجاره ملک 
    /// </summary>
    public ProtestFormTitle? PropertyRentStatus { get; init; }
    
    /// <summary>
    /// وضعیت اعتراض اجاره 
    /// </summary>
    public ProtestFormTitle? RentObjectionStatus { get; init; }
    
    /// <summary>
    /// وضعیت خرید و فروش 
    /// </summary>
    public ProtestFormTitle? BuyAndSellStatus { get; init; }
    
    /// <summary>
    /// وضعیت لاگ صاحب منزل در پذیرش 
    /// </summary>
    public ProtestFormTitle? HomeOwnerLogStatus { get; init; }
    
    /// <summary>
    /// وضعیت احراز لاگ صاحب منزل پذیرش 
    /// </summary>
    public ProtestFormTitle? HomeOwnerLogVerificationStatus { get; init; }
    
    /// <summary>
    /// سوال پذیرشی دارای منزل هستید 
    /// </summary>
    public bool HasHouse { get; init; }
   
    /// <summary>
    /// وضعیت سابقه مسکن 
    /// </summary>
    public bool HasHousingHistoryStatus { get; init; }
}
