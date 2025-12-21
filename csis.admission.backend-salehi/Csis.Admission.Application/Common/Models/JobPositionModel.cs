/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common.Models;

/// <summary>
/// مدل جایگاه شغلی
/// </summary>
public sealed class JobPositionModel
{
    /// <summary>
    /// نام فرد دارای جایگاه
    /// </summary>
    [JsonPropertyName("firstName")]
    public string FirstName { get; init; }

    /// <summary>
    /// نام خانوادگی فرد دارای جایگاه
    /// </summary>
    [JsonPropertyName("lastName")]
    public string LastName { get; init; }

    /// <summary>
    /// شناسه
    /// </summary>
    [JsonPropertyName("staffId")]
    public string Id { get; init; }

    /// <summary>
    /// شناسه پدر
    /// </summary>
    [JsonPropertyName("staffParentId")]
    public string ParentId { get; init; }

    /// <summary>
    /// عنوان
    /// </summary>
    [JsonPropertyName("staffTitle")]
    public string Title { get; init; }

    /// <summary>
    /// کد پرسنلی دارنده جایگاه
    /// </summary>    
    public int? PersonnelId => PersonnelIdString.ToIntNullable();

    /// <summary>
    /// کد پرسنلی دارنده جایگاه
    /// </summary>
    [JsonPropertyName("personnelNo")]
    public string PersonnelIdString { get; init; }

    /// <summary>
    /// پست محوله
    /// </summary>
    [JsonPropertyName("isAssignedPost")]
    public bool IsAssignedPost { get; init; }

    /// <summary>
    /// نام کامل فرد دارای جایگاه
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";
}
