using FluentValidation;
using Csis.Admission.Application.Features.Documents.Commands;

namespace Csis.Admission.Application.Features.Documents.Validators;

public sealed class CreateDocumentCommandValidator : BaseValidator<CreateDocumentCommand>
{
    public CreateDocumentCommandValidator() {
        RuleFor(x => x.Type).IsInEnum().WithName("نوع فایل");
    }
}
