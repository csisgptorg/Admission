namespace Csis.Admission.Application.Features.CaseFilings.Commands;

/// <summary>
/// (تایید اطلاعات هویتی گام سوم(تاییدیه
/// </summary>
public sealed record ConfirmIdentityInformationCommand : IRequest
{
    /// <summary>توکن</summary>
    public Guid Token { get; init; }

    /// <summary>تأیید اطلاعات هویتی</summary>
    public bool ConfirmIdentityInformation { get; init; }
}

internal sealed class ConfirmVerifyingIdentityInformationCommandHandler(
    IRepository<AdmissionCaseUser, Guid> caseUserRepo,
    IMapper mapper)
    : IRequestHandler<ConfirmIdentityInformationCommand>
{
    public async Task Handle(ConfirmIdentityInformationCommand request, CancellationToken cancellationToken) {

        if ( !request.ConfirmIdentityInformation ) {
            throw new CommandValidationException("برای ادامه باید اطلاعات هویتی را تایید کنید , اگر اطلاعات هویتی شما اشتباه است لطفا با پشتیبانی تماس بگیرید.");
        }

        var admissionCaseUser = await caseUserRepo.GetByIdAsTrackingAsync(request.Token, cancellationToken: cancellationToken)
                                ?? throw new CommandValidationException("شناسه نامعتبر است.");

        admissionCaseUser.ConfirmIdentityInformation = request.ConfirmIdentityInformation;
        admissionCaseUser.CaseStep = AdmissionCaseStep.IdentityVerified;
        await caseUserRepo.UpdateAsync(admissionCaseUser, cancellationToken: cancellationToken);
    }
}



