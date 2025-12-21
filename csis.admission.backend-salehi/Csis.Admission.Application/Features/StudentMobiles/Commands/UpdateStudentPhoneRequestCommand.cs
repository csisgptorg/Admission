using Csis.Notification;
using Csis.Authorization.Services;
using System.Text.RegularExpressions;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.StudentMobiles.Commands;

/// <summary>بروز رسانی موبایل طلبه</summary>
public sealed record UpdateStudentMobileRequestCommandAction(string Mobile, string? PreCodeTel, string? Tel);

/// <summary>بروز رسانی موبایل طلبه</summary>
public sealed record UpdateStudentPhoneRequestCommand(int? Codm, string? Mobile, string? PreCodeTel, string? Tel, string? Otp, bool Confirm) : IRequest<bool>;

internal sealed class UpdateStudentMobileRequestCommandHandler(
    ICsisWsmService wsmService,
    IRequestService requestService,
    IRepository<StudentSummary> repo,
    ICsisNotificationAdvancedService notifService,
    ICurrentUserService currentUser
    ) : IRequestHandler<UpdateStudentPhoneRequestCommand, bool>
{
    public async Task<bool> Handle(UpdateStudentPhoneRequestCommand command, CancellationToken cancellationToken) {
        var isEmployee = await currentUser.IsEmployee();
        var isSenior = await currentUser.IsSenior();

        _ = await Common.Utilities.SetCodm(command, currentUser);
        var isValidMobile = await ValidateMobileOwnership(command, isSenior, cancellationToken);

        if ( isSenior ) {
            switch ( command.Confirm ) {
                case false when isValidMobile:
                    return true;
                case false when !isValidMobile:
                    return false;
            }
        }

        if ( !isEmployee ) {
            await SendOtp(command, cancellationToken);
            await VerifyOtp(command, cancellationToken);
        }

        var request = new CreateRequestCommand(command, RequestFlow.DirectRegistration,RequestType.UpdateStudentPhone);
        _ = await requestService.Create(request, cancellationToken);

        return true;
    }

    private async Task<bool> ValidateMobileOwnership(UpdateStudentPhoneRequestCommand command, bool isSenior, CancellationToken cancellationToken) {

        if ( !Regex.IsMatch(command.Mobile, @"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$") ) {
            throw new CommandValidationException("شماره موبایل نامعتبر است.");
        }

        var student = await repo.GetOneAsync(x => x.Codm == command.Codm);
        if ( !isSenior && student.Mobile == command.Mobile ) {
            throw new CommandValidationException("شماره موبایل وارد شده با شماره موبایل فعلی شما یکسان است. لطفاً شماره جدیدی وارد کنید");
        }

        var validationRequest = new ValidateMobileOwnershipRequest(student.NationalCode, command.Mobile);
        var isValidMobile = await wsmService.ValidateMobileOwnership(validationRequest, cancellationToken);

        if ( !isValidMobile && !isSenior ) {
            throw new CommandValidationException("شماره موبایل وارد شده با کد ملی شما مطابقت ندارد. لطفاً شماره‌ای را وارد کنید که به نام خودتان ثبت شده باشد.");
        }

        if ( isSenior ) {
            if ( !command.Confirm ) {
                return isValidMobile;
            }

            return true;
        }

        return false;
    }

    private async Task SendOtp(UpdateStudentPhoneRequestCommand command, CancellationToken cancellationToken) {
        if ( !string.IsNullOrEmpty(command.Otp) ) { return; }

        var sendOtp = await notifService.SendOtpToMobile(new SendOtpToMobile(command.Mobile, command.GetType().Name), cancellationToken);
        if ( sendOtp.Succeeded == false ) {
            throw new CommandValidationException(sendOtp.Message);
        }

        throw new NeedOtpCommandException(sendOtp.Data.ExpiresInSeconds, sendOtp.Data.Message);
    }

    private async Task VerifyOtp(UpdateStudentPhoneRequestCommand command, CancellationToken cancellationToken) {
        if ( string.IsNullOrEmpty(command.Otp) ) { return; }

        var verifyOtp = new VerifyOtp(command.Otp, command.Mobile, command.GetType().Name);
        var verifyResult = await notifService.VerifyOtp(verifyOtp, cancellationToken);
        if ( verifyResult.Succeeded == false || verifyResult.Data == false ) {
            throw new CommandValidationException("کد تایید نامعتبر است.");
        }
    }
}
