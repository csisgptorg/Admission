using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Schools.Dtos;

/// <summary>مدرسه</summary>
public sealed record SchoolDto : BaseDto<SchoolDto, School, short>
{
    /// <summary>عنوان</summary>
    public string Title { get; set; }
}
