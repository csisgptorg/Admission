using Csis.Admission.Application.Features.Students.Queries;
using FluentValidation;

namespace Csis.Admission.Application.Features.Family.Validators;

public sealed class GetFamilyByYektaCodeValidator : BaseValidator<GetFamilyByYektaCodeQuery>
{
    public GetFamilyByYektaCodeValidator() {
        RuleFor(i => i.YektaCode).NotEmpty().Length(10).WithName("کد یکتا");
    }
}
