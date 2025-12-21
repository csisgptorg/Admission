using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Students.Commands;

/// <summary>
/// تمدید پرونده دستی
/// </summary>
public sealed record ManualStudentExtensionCaseRequestCommand(int Codm, List<int> CaseValidityReasonId, string CaseValidityDate) : IRequest;

internal sealed class ManualStudentExtensionCaseRequestCommandHandler(
    IRequestService requestService,
    ICurrentUserService currentUserService)
    : IRequestHandler<ManualStudentExtensionCaseRequestCommand>
{
    public async Task Handle(ManualStudentExtensionCaseRequestCommand request, CancellationToken cancellationToken) {
        var isSenior = await currentUserService.IsSenior();
        if ( !isSenior ) {
            throw new CommandValidationException("فقط کاربران با سطح دسترسی ارشد مجاز به انجام این عملیات می باشند.");
        }

        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.ManualStudentExtensionCase);
        var requestResult = await requestService.Create(requestCommand, cancellationToken);

    }

}
