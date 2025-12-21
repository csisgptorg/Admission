using Csis.FileManagement;

namespace Csis.Admission.Application.Features.Files.Dtos;

/// <summary>
/// برای نمایش فایل ها 
/// </summary>
public sealed record CaseFilingFileModelDto
{
    /// <summary>
    /// لینک دانلود 
    /// </summary>
    public string Link { get; set; }
    /// <summary>
    /// نام کامل
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// نوع فایل
    /// </summary>
    public FileTypeEnum? FileType { get; set; }

    /// <summary>
    /// شناسه فایل در سامانه مدیریت فایل
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// بخش مربوطه
    /// /// </summary>
    public RelatedSection RelatedSection { get; set; }

}
/// <summary>
/// بخش مربوط به نمایش تصاویر
/// </summary>
public enum RelatedSection
{
    Bank = 1,
    NewImage = 2,
    OldImage = 3
}

