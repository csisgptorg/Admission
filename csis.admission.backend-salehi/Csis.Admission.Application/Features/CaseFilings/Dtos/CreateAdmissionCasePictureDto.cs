using Csis.CompareImageAi.Dtos;

namespace Csis.Admission.Application.Features.CaseFilings.Dtos;

/// <summary>
/// تصویر پروفایل متقاضی
/// </summary>
/// <param name="FileId"></param>
/// <param name="ImageAnalysisResultDto"></param>
public sealed record CreateAdmissionCasePictureDto
{
    /// <summary>
    /// شناسه فایل تصویر جدید
    /// </summary>
    public Guid FileId { get; init; }

    /// <summary>
    /// شناسه فایل تصویر قدیمی (شناسنامه)
    /// </summary>
    public Guid? OldImageFileId { get; init; }

    /// <summary>
    /// نتیجه تحلیل تصویر
    /// </summary>
    public ImageAnalysisResultDto? ImageAnalysisResultDto { get; init; }
};
