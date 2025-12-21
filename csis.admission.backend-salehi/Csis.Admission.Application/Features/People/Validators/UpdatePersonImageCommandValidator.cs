using FluentValidation;
using Csis.Admission.Application.Common;
using Csis.Admission.Application.Features.People.Commands;

namespace Csis.Admission.Application.Features.People.Validators;

/// <summary>
/// «⁄ »«—”‰Ã? ›—„«‰ »—Ê“—”«‰?  ’Ê?— ‘Œ’
/// </summary>
public sealed class UpdatePersonImageCommandValidator : BaseValidator<UpdatePersonImageCommand>
{
    public UpdatePersonImageCommandValidator()
    {
        RuleFor(x => x.PersonId)
            .GreaterThan(0)
            .WithMessage("‘‰«”Â ‘Œ’ ‰«„⁄ »— «” .");

        RuleFor(x => x.PersonImage)
            .NotEmpty()
            .WithMessage("‘‰«”Â  ’Ê?— «·“«„? «” .")
            .When(x => x.PersonImage.HasValue);
    }
}
