using FluentValidation;
using Csis.Admission.Application.Features.ReportBuilders.Commands;

namespace Csis.Admission.Application.Features.ReportBuilders.Validators;

public sealed class UpdateReportBuilderCommandValidator : BaseValidator<UpdateReportBuilderCommand>
{
    public UpdateReportBuilderCommandValidator() {
        RuleFor(x => x.Title).NotEmpty().WithName("عنوان");
    }
}

