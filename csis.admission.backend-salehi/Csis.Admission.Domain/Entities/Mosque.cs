using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>مسجد</summary>
public class Mosque : SoftDeletedBaseEntity, IFilterable
{
    /// <summary>
    /// کد مرکز
    /// </summary>
    public int Codm { get; set; }
    /// <summary>
    /// نام رسمی مسجد بر اساس مدارک حقوقی
    /// </summary>
    public string OfficialName { get; set; }

    /// <summary>
    /// نام‌های محلی یا نام‌های دیگر رایج در بین مردم
    /// </summary>
    public string LocalNames { get; set; }

    /// <summary>
    /// (کد پستی مسجد (کلید یکتا
    /// </summary>
    public long? PostalCode { get; set; }

    /// <summary>
    /// آیا مسجد دارای کد پستی است؟
    /// </summary>
    public bool? MosqueHasNotPostalCode { get; set; }

    /// <summary>
    /// (نوع محل فعالیت (مسجد، حسینیه، یا تکیه
    /// </summary>
    public ActivityPlaceType PlaceType { get; set; }

    /// <summary>
    /// آیا مسجد دارای خانه عالم است؟
    /// </summary>
    public bool? HasClergyHouse { get; set; }

    /// <summary>
    /// (وضعیت خانه عالم (قابل سکونت یا غیرقابل سکونت
    /// </summary>
    public ClergyHouseStatus? ClergyHouseStatus { get; set; }


    /// <summary>
    /// نقش پرکننده فرم مسجد
    /// </summary>
    public MosqueFormFillerRole MosqueFormRole { get; set; }

    /// <summary>
    /// وضعیت فعالیت سالانه مسجد
    /// </summary>
    public AnnualActivityType MosqueAnnualActivityStatus { get; set; }


    /// <summary> /// </summary>
    public int? MosqueAddressId { get; set; }

    /// <summary>
    /// آدرس مسجد
    /// </summary>
    public Address MosqueAddress { get; set; }

    /// <summary>
    /// شناسه فعالیت‌های مسجد
    /// </summary>
    public int MosqueActivityId { get; set; }

    /// <summary>
    /// فعالیت‌های مسجد
    /// </summary>
    public MosqueActivity MosqueActivity { get; set; }

    /// <summary>
    /// امام جماعت‌های مرتبط با این مسجد
    /// </summary>
    public List<ImamJamaat> Imams { get; set; }

    /// <summary>
    /// شناسه مسجد
    /// </summary>
    public string? ManualMosqueId { get; set; }

    /// <inheritdoc/>
    public string[] GetFilterableFields() {
        return [nameof(PostalCode), nameof(OfficialName)];
    }
}
