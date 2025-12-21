using System.Text;
using Csis.Notification;
using Csis.Utilities.Extensions;
using Csis.Admission.Domain.Enums;
using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Dtos.RequestService;

namespace Csis.Admission.Services;

/// <summary>تایید کارمند</summary>
internal sealed partial class CaseFillingRequestService : ICaseFillingRequestService
{
    public async Task ApproveRequestByEmployee(ApproveCaseFillingRequestByEmployeeCommand command, CancellationToken cancellation) {

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

        if ( command.Status == ApprovalStatus.Rejected ) {
            var foundedCaseUser = await caseUserRepository.GetOneAsync(x => x.RequestId == command.RequestId, cancellationToken: cancellation);
            await caseUserRepository.DeleteAsync(foundedCaseUser.Id, cancellationToken: cancellation);
        }

        //await SendMessageOnReject(request, command, cancellation);
    }

    private async Task<CaseFillingEmployeeDto> ValidateAndCurrentEmployee(Domain.Entities.CaseFillingRequest request) {
        if ( request.NextFlowApprover == RequestApprovalFlow.TheEnd ) {
            throw new CommandValidationException("فرآیند درخواست به پایان رسیده است.");
        }

        var employee = await CurrentEmployee();
        if ( request.NextFlowApprover == RequestApprovalFlow.SeniorEmployee && !employee.IsSenior ) {
            throw new CommandValidationException("فقط کاربر با سطح دسترسی ارشد می‌تواند وضعیت جدید ثبت کند.");
        }

        return employee;
    }

    private void StudentToEmployee(Domain.Entities.CaseFillingRequest request, ApproveCaseFillingRequestByEmployeeCommand command, CaseFillingEmployeeDto employee) {
        if ( request.Flow != RequestFlow.StudentToEmployee ) {
            return;
        }

        request.ApprovalStatus = command.Status;
        request.NextFlowApprover = RequestApprovalFlow.TheEnd;

        request.EmployeeApprover(employee.PersonnelId.Value, employee.FullName, command.Status, skipSmsOnRejection: command.SkipSmsOnRejected);
    }

    private void StudentToEmployeeToSeniorEmployeeAndNextIsEmployee(Domain.Entities.CaseFillingRequest request, ApproveCaseFillingRequestByEmployeeCommand command, CaseFillingEmployeeDto employee) {
        if ( request.Flow != RequestFlow.StudentToEmployeeToSeniorEmployee || request.NextFlowApprover != RequestApprovalFlow.Employee ) {
            return;
        }

        if ( command.Status == ApprovalStatus.Approved ) {
            request.NextFlowApprover = RequestApprovalFlow.SeniorEmployee;

        } else {
            request.ApprovalStatus = command.Status;
            request.NextFlowApprover = RequestApprovalFlow.TheEnd;
        }

        request.EmployeeApprover(employee.PersonnelId.Value, employee.FullName, command.Status, ApproverRole.Employee, command.SkipSmsOnRejected);
    }

    private void StudentToEmployeeToSeniorEmployeeAndNextIsSeniorEmployee(Domain.Entities.CaseFillingRequest request, ApproveCaseFillingRequestByEmployeeCommand command, CaseFillingEmployeeDto employee) {
        if ( request.Flow != RequestFlow.StudentToEmployeeToSeniorEmployee || request.NextFlowApprover != RequestApprovalFlow.SeniorEmployee ) {
            return;
        }

        request.ApprovalStatus = command.Status;
        request.NextFlowApprover = RequestApprovalFlow.TheEnd;

        request.EmployeeApprover(employee.PersonnelId.Value, employee.FullName, command.Status, ApproverRole.SeniorEmployee, command.SkipSmsOnRejected);
    }

    private void EmployeeToSeniorEmployee(Domain.Entities.CaseFillingRequest request, ApproveCaseFillingRequestByEmployeeCommand command, CaseFillingEmployeeDto employee) {
        if ( request.Flow != RequestFlow.EmployeeToSeniorEmployee ) {
            return;
        }

        request.ApprovalStatus = command.Status;
        request.NextFlowApprover = RequestApprovalFlow.TheEnd;

        request.EmployeeApprover(employee.PersonnelId.Value, employee.FullName, command.Status, ApproverRole.SeniorEmployee, command.SkipSmsOnRejected);
    }

    private async Task SendMessageOnReject(Domain.Entities.CaseFillingRequest request, ApproveCaseFillingRequestByEmployeeCommand command, CancellationToken cancellation) {
        if ( command.Status == ApprovalStatus.Rejected && !command.SkipSmsOnRejected ) {
            var message = new StringBuilder();
            message.Append("فاضل گرامی، درخواست");
            message.Append(" " + request.Type.GetEnumDisplayName());
            message.Append(" رد شد.");
            await notificationService.SendMessageToStudent(new SendMessageToStudent(message.ToString(), [request.Codm], [DeliveryChannelEnum.Sms]), cancellation);
        }
    }
}
