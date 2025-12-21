using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// موجودیت ازدواج
/// </summary>
public sealed class Marriage : SoftDeletedBaseEntity, IFilterable
{
    /// <summary>
    /// شناسه شوهر
    /// به دلیل ثبت تکفل ها نالبل است
    /// </summary>
    public int? HusbandPersonId { get; set; }

    /// <summary>
    /// شناسه همسر
    /// به دلیل ثبت تکفل ها نالبل است
    /// </summary>
    public int? WifePersonId { get; set; }

    /// <summary>
    /// تاریخ ازدواج
    /// </summary>
    public DateOnly? MarriageDate { get; set; }

    /// <summary>
    /// تاریخ طلاق
    /// </summary>
    public DateOnly? DivorceDate { get; set; }

    /// <summary>
    /// تاریخ فوت
    /// </summary>
    public DateOnly? DeathDate { get; set; }

    /// <summary>
    /// آیا ازدواج پایان یافته است؟
    /// </summary>
    public bool IsEnded => DivorceDate.HasValue || DeathDate.HasValue;

    /// <summary>
    /// شوهر
    /// </summary>
    public Person HusbandPerson { get; private set; }

    /// <summary>
    /// همسر
    /// </summary>
    public Person WifePerson { get; private set; }

    /// <inheritdoc/>
    public string[] GetFilterableFields() {
        return [];
    }
}
