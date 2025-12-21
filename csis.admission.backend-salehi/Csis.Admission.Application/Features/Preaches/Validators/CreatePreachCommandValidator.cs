using FluentValidation;
using System.Text.RegularExpressions;
using Csis.Admission.Application.Features.Preaches.Commands;

namespace Csis.Admission.Application.Features.Preaches.Validators;

public sealed class CreatePreachCommandValidator : BaseValidator<CreatePreachCommand>
{
    public CreatePreachCommandValidator() {

        RuleFor(x => x.StartDate)
            .Must(StartDate => Regex.IsMatch(StartDate, @"^(13[0-9]{2}|14[0-9]{2})[-\/](0[1-9]|1[0-2])[-\/](0[1-9]|[12][0-9]|3[01])$"))
            .WithMessage("فرمت تاریخ باید به صورت yyyy/MM/dd باشد.");

        RuleFor(x => x.EndDate)
             .Must(EndDate => string.IsNullOrWhiteSpace(EndDate) || Regex.IsMatch(EndDate, @"^(13[0-9]{2}|14[0-9]{2})[-\/](0[1-9]|1[0-2])[-\/](0[1-9]|[12][0-9]|3[01])$"))
             .WithMessage("فرمت تاریخ باید به صورت yyyy/MM/dd باشد.");

    }
}
