using System.Text;
using Csis.Notification;
using Csis.Utilities.Extensions;
using Csis.Admission.Domain.Enums;
using Csis.Abstractions.Exceptions;
using Csis.Admission.Domain.Entities;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Dtos.RequestService;

namespace Csis.Admission.Services;

/// <summary>تایید کارمند</summary>
internal sealed partial class RequestService : IRequestService
{
    public async Task ApproveRequestByEmployee(ApproveRequestByEmployeeCommand command, CancellationToken cancellation) {

        var request = await repo.GetByIdAsTrackingAsync(command.RequestId, cancellationToken: cancellation, x => x.Approvers, x => x.Documents);
        var employee = await ValidateAndCurrentEmployee(request);

        StudentToEmployee(request, command, employee);
        StudentToEmployeeToSeniorEmployeeAndNextIsEmployee(request, command, employee);
        StudentToEmployeeToSeniorEmployeeAndNextIsSeniorEmployee(request, command, employee);
        EmployeeToSeniorEmployee(request, command, employee);
        await SendCommand(request, cancellation);
        if ( request.RecordId == -1 ) {
            throw new CommandValidationException("تایید درخواست انجام نشد، ثبت تغییرات درخواستی با خطا مواجه شد.");
        }

        await repo.UpdateAsync(request, true, cancellation);

        //await SendMessageOnReject(request, command, cancellation);
    }

    private async Task<EmployeeDto> ValidateAndCurrentEmployee(Request request) {
        if ( request.NextFlowApprover == RequestApprovalFlow.TheEnd ) {
            throw new CommandValidationException("فرآیند درخواست به پایان رسیده است.");
        }

        var employee = await CurrentEmployee();
        if ( request.NextFlowApprover == RequestApprovalFlow.SeniorEmployee && !employee.IsSenior ) {
            throw new CommandValidationException("فقط کاربر با سطح دسترسی ارشد می‌تواند وضعیت جدید ثبت کند.");
        }

        return employee;
    }

    private void StudentToEmployee(Request request, ApproveRequestByEmployeeCommand command, EmployeeDto employee) {
        if ( request.Flow != RequestFlow.StudentToEmployee ) {
            return;
        }

        request.ApprovalStatus = command.Status;
        request.NextFlowApprover = RequestApprovalFlow.TheEnd;

        request.EmployeeApprover(employee.PersonnelId, employee.FullName, command.Status, skipSmsOnRejection: command.SkipSmsOnRejected);
    }

    private void StudentToEmployeeToSeniorEmployeeAndNextIsEmployee(Request request, ApproveRequestByEmployeeCommand command, EmployeeDto employee) {
        if ( request.Flow != RequestFlow.StudentToEmployeeToSeniorEmployee || request.NextFlowApprover != RequestApprovalFlow.Employee ) {
            return;
        }

        if ( command.Status == ApprovalStatus.Approved ) {
            request.NextFlowApprover = RequestApprovalFlow.SeniorEmployee;

        } else {
            request.ApprovalStatus = command.Status;
            request.NextFlowApprover = RequestApprovalFlow.TheEnd;
        }

        request.EmployeeApprover(employee.PersonnelId, employee.FullName, command.Status, ApproverRole.Employee, command.SkipSmsOnRejected);
    }

    private void StudentToEmployeeToSeniorEmployeeAndNextIsSeniorEmployee(Request request, ApproveRequestByEmployeeCommand command, EmployeeDto employee) {
        if ( request.Flow != RequestFlow.StudentToEmployeeToSeniorEmployee || request.NextFlowApprover != RequestApprovalFlow.SeniorEmployee ) {
            return;
        }

        request.ApprovalStatus = command.Status;
        request.NextFlowApprover = RequestApprovalFlow.TheEnd;

        request.EmployeeApprover(employee.PersonnelId, employee.FullName, command.Status, ApproverRole.SeniorEmployee, command.SkipSmsOnRejected);
    }

    private void EmployeeToSeniorEmployee(Request request, ApproveRequestByEmployeeCommand command, EmployeeDto employee) {
        if ( request.Flow != RequestFlow.EmployeeToSeniorEmployee ) {
            return;
        }

        request.ApprovalStatus = command.Status;
        request.NextFlowApprover = RequestApprovalFlow.TheEnd;

        request.EmployeeApprover(employee.PersonnelId, employee.FullName, command.Status, ApproverRole.SeniorEmployee, command.SkipSmsOnRejected);
    }

    private async Task SendMessageOnReject(Request request, ApproveRequestByEmployeeCommand command, CancellationToken cancellation) {
        if ( command.Status == ApprovalStatus.Rejected && !command.SkipSmsOnRejected ) {
            var message = new StringBuilder();
            message.Append("فاضل گرامی، درخواست");
            message.Append(" " + request.Type.GetEnumDisplayName());
            message.Append(" رد شد.");
            await notificationService.SendMessageToStudent(new SendMessageToStudent(message.ToString(), [request.Codm], [DeliveryChannelEnum.Sms]), cancellation);
        }
    }
}
