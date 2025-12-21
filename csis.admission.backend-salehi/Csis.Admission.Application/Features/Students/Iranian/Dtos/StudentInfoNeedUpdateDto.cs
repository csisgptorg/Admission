namespace Csis.Admission.Application.Features.Students.Dtos;

/// <summary>اطلاعات طلبه که نیاز به بروزرسانی دارند</summary>
public record StudentInfoNeedUpdateDto
{
    /// <summary>آدرس</summary>
    public bool MustUpdateAddress { get; init; }
    /// <summary>اشتغال</summary>
    public bool MustUpdateEmployment { get; init; }
    /// <summary>تصویر پروفایل</summary>
    public bool MustUpdatePicture { get; init; }
    /// <summary>مسکن</summary>
    public bool MustUpdateHouse { get; init; }
}
