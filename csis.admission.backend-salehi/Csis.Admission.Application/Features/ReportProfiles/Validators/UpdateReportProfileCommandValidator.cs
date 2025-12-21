/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Features.ReportProfiles.Commands;

namespace Csis.Admission.Application.Features.ReportProfiles.Validators;

public sealed class UpdateReportProfileCommandValidator : BaseValidator<UpdateReportProfileCommand>
{
    public UpdateReportProfileCommandValidator() {
        RuleFor(x => x.Structure).NotNull().WithName("ساختار گزارش");
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200).WithName("عنوان پروفایل");
        RuleFor(x => x.Description).MaximumLength(500).WithName("توضیحات");
    }
}
