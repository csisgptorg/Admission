using System.Text;
using Csis.Notification;
using Csis.Utilities.Extensions;
using Csis.Admission.Domain.Enums;
using Csis.Abstractions.Exceptions;
using Csis.Admission.Domain.Entities;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Dtos.RequestService;

namespace Csis.Admission.Services;

/// <summary>تایید دو طلبه</summary>
internal sealed partial class RequestService : IRequestService
{
    public async Task ApproveRequestByStudent(ApproveRequestByStudentCommand dto, CancellationToken cancellationToken) {

        var codm = int.Parse(await authenticatedUser.GetStudentCodmAsync());
        var request = await repo.GetByIdAsTrackingAsync(dto.RequestId, x => x.Approvers, cancellationToken: cancellationToken);

        Validator(request, codm, cancellationToken);

        await UpdateRequestStatus(codm, request, dto, cancellationToken);
    }

    /// <summary>اعتبار سنجی</summary>
    private void Validator(Request request, int approverCodm, CancellationToken cancellation) {

        if ( request.NextFlowApprover == RequestApprovalFlow.TheEnd ) {
            throw new CommandValidationException("فرآیند درخواست به پایان رسیده است.");
        }

        if ( request.Flow != RequestFlow.DualStudents ) {
            throw new CommandValidationException("فرایند درخواست نیازمند تایید طلبه نمی باشد.");
        }

        if ( !request.Approvers.Any(x => x.ApproverCodm == approverCodm) ) {
            throw new CommandValidationException("شما به عنوان تأییدکننده برای این درخواست مشخص نشده‌اید.");
        }

        if ( request.Approvers.Any(x => x.ApproverCodm == approverCodm && x.Status != ApprovalStatus.Pending) ) {
            throw new CommandValidationException("شما پیش از این وضعیت درخواست را تعیین کرده‌اید.");
        }
    }

    private async Task UpdateRequestStatus(int approverCodm, Request request, ApproveRequestByStudentCommand dto, CancellationToken cancellation) {

        request.StudentApprover(approverCodm, dto.Status);
        DetermineRequestApprovalStatus(request);
        await SendCommand(request, cancellation);
        if ( request.RecordId == -1 ) {
            throw new CommandValidationException("تایید درخواست انجام نشد، ثبت تغییرات درخواستی با خطا مواجه شد.");
        }

        await repo.UpdateAsync(request, true, cancellation);
        await SendRejectMessage(request, approverCodm, cancellation);
    }

    private void DetermineRequestApprovalStatus(Request request) {

        // یکی اصل ثبت درخواست هست - دو تاهم تایید طلبه ها
        if ( request.Approvers.Count(x => x.Status == ApprovalStatus.Approved) == 3 ) {
            request.ApprovalStatus = ApprovalStatus.Approved;
        }

        if ( request.Approvers.Any(x => x.Status == ApprovalStatus.Rejected) ) {
            request.ApprovalStatus = ApprovalStatus.Rejected;
        }

        if ( request.ApprovalStatus == ApprovalStatus.Approved || request.ApprovalStatus == ApprovalStatus.Rejected ) {
            request.NextFlowApprover = RequestApprovalFlow.TheEnd;

        } else {
            request.NextFlowApprover = RequestApprovalFlow.Student;
        }
    }

    private async Task SendRejectMessage(Request request, int approverCodm, CancellationToken cancellation) {

        if ( request.ApprovalStatus == ApprovalStatus.Rejected ) {
            var sb = new StringBuilder();
            sb.Append("درخواست");
            sb.Append(" " + request.Type.GetEnumDisplayName());
            sb.Append(" توسط طلبه‌ با کد مرکز خدمات ");
            sb.Append(" " + approverCodm);
            sb.Append(" رد شده است.");
            await notificationService.SendMessageToStudent(new SendMessageToStudent(sb.ToString(), [request.Codm], [DeliveryChannelEnum.Sms]), cancellation);
        }
    }
}
