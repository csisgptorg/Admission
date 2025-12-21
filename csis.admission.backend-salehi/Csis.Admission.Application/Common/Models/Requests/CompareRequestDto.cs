namespace Csis.Admission.Application.Common.Models.Requests;

/// <summary>
/// DTO ورودی برای مقایسه داده‌ها
/// </summary>
public sealed class CompareRequestDto
{
    /// <summary>(کد مرکز (اختیاری</summary>
    public int? Codm { get; init; }
    /// <summary>نوع Entity</summary>
    public string EntityType { get; init; }

    /// <summary>داده‌های جدید</summary>
    public object NewData { get; init; }

    /// <summary>(شناسه تکفل (اختیاری</summary>
    public long? DependentId { get; init; }
}
