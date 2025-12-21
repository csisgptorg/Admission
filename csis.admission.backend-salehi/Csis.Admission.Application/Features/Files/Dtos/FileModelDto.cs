using Csis.FileManagement;

namespace Csis.Admission.Application.Features.Files.Dtos;

/// <summary>
/// برای نمایش فایل ها 
/// </summary>
public sealed record FileModelDto
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
}
