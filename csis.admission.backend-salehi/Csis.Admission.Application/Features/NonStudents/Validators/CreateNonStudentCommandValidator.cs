using Csis.Admission.Application.Common;
using Csis.Admission.Application.Features.NonStudents.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.NonStudents.Validators;

public sealed class CreateNonStudentCommandValidator : BaseValidator<CreateNonStudentCommand>
{
    public CreateNonStudentCommandValidator() {
    }
}
