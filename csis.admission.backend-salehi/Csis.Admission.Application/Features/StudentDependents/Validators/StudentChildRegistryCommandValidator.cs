using FluentValidation;
using System.Text.RegularExpressions;
using Csis.Admission.Application.Features.StudentDependents.Commands;

namespace Csis.Admission.Application.Features.SoldierStudents.Validators;

/// <inheritdoc/>
public sealed class StudentChildRegistryCommandValidator : BaseValidator<StudentChildRegistryCommand>
{
    /// <inheritdoc/>
    public StudentChildRegistryCommandValidator() {
        RuleFor(x => x.NationalCode).Matches(new Regex(Utilities.Constants.Regex.PersonNationalId)).WithMessage("کد ملی معتبر نمی باشد.");
        RuleFor(x => x.BirthDate).Matches(new Regex(Constants.StringDateFormatRegex)).WithMessage("قالب تاریخ تولد صحیح نمی باشد.");
    }
}
