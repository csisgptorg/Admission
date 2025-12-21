using FluentValidation;
using Csis.Admission.Application.Features.ReportBuilders.Commands;

namespace Csis.Admission.Application.Features.ReportBuilders.Validators;
public sealed class CreateReportBuilderCommandValidator : BaseValidator<CreateReportBuilderCommand>
{
    public CreateReportBuilderCommandValidator() {
        RuleFor(x => x.Title).NotEmpty().WithName("عنوان");
    }
}
