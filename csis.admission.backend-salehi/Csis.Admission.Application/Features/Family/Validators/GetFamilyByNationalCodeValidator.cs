using Csis.Admission.Application.Features.Students.Queries;
using FluentValidation;

namespace Csis.Admission.Application.Features.Family.Validators;
public sealed class GetFamilyByNationalCodeValidator : BaseValidator<GetFamilyByNationalCodeQuery>
{
    public GetFamilyByNationalCodeValidator() {
        RuleFor(i => i.NationalCode).NotEmpty().Length(10).WithName("کد ملی");
    }
}
