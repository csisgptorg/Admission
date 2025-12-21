using FluentValidation;
using Csis.Admission.Application.Features.CaseFilings.Commands;

namespace Csis.Admission.Application.Features.CaseFilings.Validator;

internal sealed class CreateAdmissionCaseCommissionIdentityByEmployeeCommandValidator
    : AbstractValidator<CreateAdmissionCaseCommissionIdentityByEmployeeCommand>
{
    public CreateAdmissionCaseCommissionIdentityByEmployeeCommandValidator() {
        RuleFor(x => x.CommissionRequestId).GreaterThan(0).NotEmpty().WithName("شناسه درخواست کمیسیون");
    }
}
