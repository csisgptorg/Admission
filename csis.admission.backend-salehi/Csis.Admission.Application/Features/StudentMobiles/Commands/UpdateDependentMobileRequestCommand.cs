using System.Net;
using Csis.Notification;
using Csis.Authorization.Services;
using System.Text.RegularExpressions;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.StudentMobiles.Commands;

/// <summary>بروز رسانی موبایل تکفل</summary>
public sealed record UpdateDependentMobileRequestCommandAction(long DependentId, string Mobile);

/// <summary>بروز رسانی موبایل تکفل</summary>
public sealed record UpdateDependentMobileRequestCommand : IRequest
{
    public int? Codm { get; set; }
    public long DependentId { get; set; }
    public string Mobile { get; set; }
    public string Otp { get; set; }
};

internal sealed class UpdateDependentMobileRequestCommandHandler(
    ICsisWsmService wsmService,
    IRequestService requestService,
    IRepository<DependentSummary, long> repo,
    ICsisNotificationAdvancedService notifService,
    ICurrentUserService currentUser
    ) : IRequestHandler<UpdateDependentMobileRequestCommand>
{
    public async Task Handle(UpdateDependentMobileRequestCommand command, CancellationToken cancellationToken) {
        var isEmployee = await currentUser.IsEmployee();
        _ = await Common.Utilities.SetCodm(command, currentUser);
        await ValidateMobileOwnership(command, cancellationToken);

        if ( !isEmployee ) {
            await SendOtp(command, cancellationToken);
            await VerifyOtp(command, cancellationToken);
        }

        var request = new CreateRequestCommand(command, RequestFlow.DirectRegistration);
        _ = await requestService.Create(request, cancellationToken);
    }

    private async Task ValidateMobileOwnership(UpdateDependentMobileRequestCommand command, CancellationToken cancellationToken) {

        if ( !Regex.IsMatch(command.Mobile, @"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$") ) {
            throw new CommandValidationException("شماره موبایل نامعتبر است.");
        }

        var dependent = await repo.GetOneAsync(x => x.Id == command.DependentId);
        if ( dependent.Mobile == command.Mobile ) {
            throw new CommandValidationException("شماره موبایل وارد شده با شماره موبایل فعلی شما یکسان است. لطفاً شماره جدیدی وارد کنید");
        }

        var validationRequest = new ValidateMobileOwnershipRequest(dependent.NationalCode, command.Mobile);
        var isValidMobile = await wsmService.ValidateMobileOwnership(validationRequest, cancellationToken);

        if ( !isValidMobile ) {
            throw new CommandValidationException("شماره موبایل و کدملی تطابق ندارند.");
        }
    }

    private async Task SendOtp(UpdateDependentMobileRequestCommand command, CancellationToken cancellationToken) {
        if ( !string.IsNullOrEmpty(command.Otp) ) { return; }

        var sendOtp = await notifService.SendOtpToMobile(new SendOtpToMobile(command.Mobile, command.GetType().Name), cancellationToken);
        if ( sendOtp.Succeeded == false ) {
            throw new CommandValidationException(sendOtp.Message);
        }

        throw new NeedOtpCommandException(sendOtp.Data.ExpiresInSeconds, sendOtp.Data.Message);
    }

    private async Task VerifyOtp(UpdateDependentMobileRequestCommand command, CancellationToken cancellationToken) {
        if ( string.IsNullOrEmpty(command.Otp) ) { return; }

        var verifyOtp = new VerifyOtp(command.Otp, command.Mobile, command.GetType().Name);
        var verifyResult = await notifService.VerifyOtp(verifyOtp, cancellationToken);
        if ( verifyResult.Succeeded == false || verifyResult.Data == false ) {
            throw new CommandValidationException("کد تایید نامعتبر است.");
        }
    }
}
