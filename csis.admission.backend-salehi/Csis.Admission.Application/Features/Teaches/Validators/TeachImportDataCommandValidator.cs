using Csis.Admission.Application.Features.Teaches.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.Teaches.Validators;

public sealed class TeachImportDataCommandValidator : BaseValidator<TeachDataImportCommand>
{
    public TeachImportDataCommandValidator() {
        RuleFor(x => x.Codm).NotEmpty().WithName("òœ „—ò“");
        RuleFor(x => x.ProvinceId).NotEmpty().WithName("‘‰«”Â «” «‰");
        RuleFor(x => x.CityId).NotEmpty().WithName("‘‰«”Â ‘Â—");
        RuleFor(x => x.SchoolId).NotEmpty().WithName("‘‰«”Â „œ—”Â");
        RuleFor(x => x.EducationYearId).NotEmpty().WithName("”«·  Õ’Ì·Ì");
        RuleFor(x => x.EducationSemester).IsInEnum().WithName("‰Ì„”«·  Õ’Ì·Ì");
        RuleFor(x => x.EducationLevel).NotEmpty().WithName("„ﬁÿ⁄  Õ’Ì·Ì");
        RuleFor(x => x.Lesson).NotEmpty().WithName("œ—”");
        RuleFor(x => x.ApprovalCenter).NotEmpty().WithName("„—ò“ ÕÊ“ÊÌ");
        RuleFor(x => x.RecordIdInApprovalCenter).NotEmpty().WithName("‘‰«”Â  »·Ì€ œ— „—ò“ ÕÊ“ÊÌ");
    }
}
