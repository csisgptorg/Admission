using Csis.Admission.Application.Common.Helpers;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Common.Models;
using Microsoft.AspNetCore.Http;

namespace Csis.Admission.Application.Features.CaseFilings.Commands;

/// <summary>
/// تایید اطلاعات بانکی
/// </summary>
public sealed record CreateBankAccountInformationCommand : IRequest
{
    /// <summary>توکن</summary>
    public Guid Token { get; init; }

    /// <summary>تأیید اطلاعات بانکی</summary>
    public string BankAccountNumber { get; init; }

    /// <summary> </summary>
    public Guid? FileId { get; set; }
}
internal sealed class ConfirmBankAccountInformationCommandHandler(
    IHttpContextAccessor contextAccessor,
    IRepository<StudentSummary> repository,
    ICsisWsmService csisWsmService,
    IRepository<AdmissionCaseUser, Guid> caseUserRepo)
    : IRequestHandler<CreateBankAccountInformationCommand>
{
    public async Task Handle(CreateBankAccountInformationCommand request, CancellationToken cancellationToken) {
        if ( !Common.Utilities.IsDevMode(contextAccessor.HttpContext) &&  await repository.ExistsAsync(
                x => x.BankAccountNumber == request.BankAccountNumber,
                cancellationToken: cancellationToken) ) {
            throw new CommandValidationException("شماره حساب بانکی وارد شده قبلا در سیستم ثبت شده است.");
        }
        var admissionCaseUser = await caseUserRepo.GetByIdAsTrackingAsync(request.Token, cancellationToken: cancellationToken)
                                ?? throw new CommandValidationException("شناسه نامعتبر است.");

        switch ( admissionCaseUser.Citizenship ) {
            case Citizenship.Iranian: {
                var founded = await csisWsmService.ValidateSibaAccountNumber(
                    new ValidateSibaAccountNumberRequest(-1, admissionCaseUser.NationalCode, request.BankAccountNumber),
                    cancellationToken);
                if ( !founded ) {
                    throw new CommandValidationException("شماره حساب بانکی وارد شده معتبر نمی باشد.");
                }
                admissionCaseUser.Payloads = PayloadHelper.AddPayloadsToString(new { request.BankAccountNumber }, admissionCaseUser.Payloads, nameof(AdmissionCasePayloadName.BankAccount));
                break;
            }
            case Citizenship.NonIranian:
                if ( string.IsNullOrWhiteSpace(request.FileId.ToString()) ) {
                    throw new CommandValidationException("آپلود مدرک بانکی برای اتباع غیر ایرانی الزامی می باشد");
                }

                admissionCaseUser.Payloads = PayloadHelper.AddPayloadsToString(new { request.BankAccountNumber, request.FileId }, admissionCaseUser.Payloads, nameof(AdmissionCasePayloadName.BankAccount));

                break;
            default:
                throw new CommandValidationException("وضعیت تابعیت نامعتبر است.");
        }

        admissionCaseUser.CaseStep = AdmissionCaseStep.BankAccountVerified;
        await caseUserRepo.UpdateAsync(admissionCaseUser, cancellationToken: cancellationToken);
    }
}
