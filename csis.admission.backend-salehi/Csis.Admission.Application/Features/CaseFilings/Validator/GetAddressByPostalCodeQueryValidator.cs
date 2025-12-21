using Csis.Admission.Application.Features.CaseFilings.Queries;
using FluentValidation;

namespace Csis.Admission.Application.Features.CaseFilings.Validator;

public sealed class GetAddressByPostalCodeQueryValidator: BaseValidator<GetAddressByPostalCodeQuery>
{
    public GetAddressByPostalCodeQueryValidator() {
        RuleFor(x => x.Token).NotEmpty().WithName("توکن");
        RuleFor(x => x.PostalCode).NotEmpty().WithName("کد پستی");
    }
}
