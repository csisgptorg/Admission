using Csis.Admission.Domain.Enums;
using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;
/// <summary>
/// Research
/// </summary>
public class Research : SoftDeletedBaseEntity, IAuditable
{
    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// نوع پژوهش
    /// </summary>
    public ResearchType? Type { get; set; }

    /// <summary>
    /// آیدی عنوان پژوهشی
    /// </summary>
    public short? SubjectId { get; set; }

    /// <summary>
    /// عنوان پژوهشی
    /// </summary>
    public ResearchSubject Subject { get; set; }

    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Year
    /// </summary>
    public short? Year { get; set; }

    /// <summary>
    /// زبان
    /// </summary>
    public ResearchLanguage? Language { get; set; }

    /// <summary>
    /// BookPublisher
    /// </summary>
    public string BookPublisher { get; set; }

    /// <summary>
    /// BookShabak
    /// </summary>
    public string BookShabak { get; set; }

    /// <summary>
    /// ArticlePublication
    /// </summary>
    public string ArticlePublication { get; set; }

    /// <summary>
    /// ProjectEmployer
    /// </summary>
    public string ProjectEmployer { get; set; }

    #region AuditLog لاگ خودکار
    /// <inheritdoc/>
    public Guid? TempId { get; set; }

    /// <inheritdoc/>
    public DataSource? AuditDataSource { get; set; }

    /// <inheritdoc/>
    public int? AuditRequestId { get; set; }

    /// <inheritdoc/>
    public int? AuditPersonId { get; set; }
    #endregion
}

