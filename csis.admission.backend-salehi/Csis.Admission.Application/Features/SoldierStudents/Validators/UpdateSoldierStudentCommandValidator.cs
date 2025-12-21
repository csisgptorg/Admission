using Csis.Admission.Application.Common;
using Csis.Admission.Application.Features.SoldierStudents.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.SoldierStudents.Validators;

public sealed class UpdateSoldierStudentCommandValidator : BaseValidator<UpdateSoldierStudentCommand>
{
    public UpdateSoldierStudentCommandValidator() {
    }
}
