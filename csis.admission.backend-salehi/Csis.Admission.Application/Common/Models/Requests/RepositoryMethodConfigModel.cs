namespace Csis.Admission.Application.Common.Models.Requests;

/// <summary>
/// تنظیمات Repository و Method برای Entity خاص
/// </summary>
public record RepositoryMethodConfigRequestModel
{
    /// <summary>نوع Repository (مثل IStudentMobileRepository)</summary>
    public Type RepositoryType { get; init; }

    /// <summary>نام Method که باید صدا زده شود (مثل GetFamily)</summary>
    public string MethodName { get; init; }

    /// <summary>
    /// تابعی که از نتیجه Method، داده مورد نظر را Extract می‌کند
    /// Parameters: (result, codm, dependentId)
    /// </summary>
    public Func<object, int, long?, object> ExtractData { get; init; }
}
