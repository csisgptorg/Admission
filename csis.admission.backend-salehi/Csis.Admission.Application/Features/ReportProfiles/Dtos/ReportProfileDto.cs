/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.ReportProfiles.Dtos;

/// <summary>
/// مدل نمایشی پروفایل گزارش
/// </summary>
public sealed record ReportProfileDto : BaseDto<ReportProfileDto, ReportProfile>, IUserInfoDto
{
    /// <summary>
    /// عنوان پروفایل
    /// </summary>
    public string Title { get; init; }

    /// <summary>
    /// توضیحات
    /// </summary>
    public string Description { get; init; }

    /// <summary>
    /// نوع پروفایل
    /// </summary>
    public ReportProfileType ProfileType { get; init; }

    /// <summary>
    /// شناسه کاربر سازنده پروفایل
    /// </summary>
    [JsonIgnore]
    public int CreatedById { get; init; }

    /// <summary>
    /// نام کاربر سازنده پروفایل
    /// </summary>
    public string CreatedBy { get; set; }

    /// <inheritdoc/>
    public override void CustomMappings(IMappingExpression<ReportProfile, ReportProfileDto> mapping) {
        mapping.ForMember(x => x.CreatedById, o => o.MapFrom(s => s.CreatedById ?? -1));
    }
}
