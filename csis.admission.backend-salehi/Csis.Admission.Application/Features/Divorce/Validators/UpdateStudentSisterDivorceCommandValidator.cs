using FluentValidation;
using Csis.Admission.Application.Common;
using Csis.Admission.Application.Common.Helpers;
using Csis.Admission.Application.Features.Divorce.Commands;

namespace Csis.Admission.Application.Features.Divorce.Validators;

/// <summary>
/// «⁄ »«—”‰Ã? ›—„ À»  ÿ·«ﬁ ÿ·»Â ŒÊ«Â—
/// </summary>
public sealed class UpdateStudentSisterDivorceCommandValidator : BaseValidator<UpdateStudentSisterDivorceRequestCommand>
{
    public UpdateStudentSisterDivorceCommandValidator()
    {
        RuleFor(x => x.Codm)
            .GreaterThan(0)
            .WithMessage("òœ „—ò“ ‰«„⁄ »— «” .");

        RuleFor(x => x.DivorceDate)
            .NotEmpty()
            .WithMessage(" «—?Œ ÿ·«ﬁ «·“«„? «” .")
            .Matches(Constants.StringDateFormatRegex)
            .WithMessage("ﬁ«·»  «—?Œ ÿ·«ﬁ ’Õ?Õ ‰„?ù»«‘œ. ﬁ«·» „Ê—œ «‰ Ÿ«—: ????/??/??");

        RuleFor(x => x.SpouseNationalCode)
            .NotEmpty()
            .WithMessage("òœ „·? Â„”— «·“«„? «” .")
            .Matches(NationalCodeHelper.NationalCodeRegexPattern)
            .WithMessage("òœ „·? Â„”— »«?œ ?? —ﬁ„ »«‘œ.")
            .Must(NationalCodeHelper.IsValidNationalCode)
            .WithMessage("òœ „·? Â„”— „⁄ »— ‰„?ù»«‘œ.");

        RuleFor(x => x.SpouseBirthDate)
            .NotEmpty()
            .WithMessage(" «—?Œ  Ê·œ Â„”— «·“«„? «” .")
            .Matches(Constants.StringDateFormatRegex)
            .WithMessage("ﬁ«·»  «—?Œ  Ê·œ Â„”— ’Õ?Õ ‰„?ù»«‘œ. ﬁ«·» „Ê—œ «‰ Ÿ«—: ????/??/??");
    }
}
