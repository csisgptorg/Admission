using FluentValidation;
using Csis.Admission.Application.Common;
using Csis.Admission.Application.Common.Helpers;
using Csis.Admission.Application.Features.Marriages.Commands;

namespace Csis.Admission.Application.Features.Marriages.Validators;

/// <summary>
/// «⁄ »«—”‰Ã? ›—„ À»  «“œÊ«Ã ÿ·»Â ŒÊ«Â—
/// </summary>
public sealed class UpdateStudentSisterMarriageCommandValidator : BaseValidator<UpdateStudentSisterMarriageCommand>
{
    public UpdateStudentSisterMarriageCommandValidator()
    {
        RuleFor(x => x.MarriageDate)
            .NotEmpty()
            .WithMessage(" «—?Œ «“œÊ«Ã «·“«„? «” .")
            .Matches(Constants.StringDateFormatRegex)
            .WithMessage("ﬁ«·»  «—?Œ «“œÊ«Ã ’Õ?Õ ‰„?ù»«‘œ. ﬁ«·» „Ê—œ «‰ Ÿ«—: ????/??/??");

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
