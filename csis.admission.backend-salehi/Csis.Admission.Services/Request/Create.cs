using Csis.Utilities.Extensions;
using Csis.Admission.Domain.Enums;
using Csis.Abstractions.Exceptions;
using Csis.Admission.Domain.Entities;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Dtos.RequestService;

namespace Csis.Admission.Services;

/// <summary>ثبت درخواست</summary>
internal sealed partial class RequestService : IRequestService
{
    public async Task<long> Create(CreateRequestCommand command, CancellationToken cancellationToken) {

        if ( command.DependentId > 0 ) {
            command.Codm = (await dependentRepo.GetByIdAsync(command.DependentId.Value, false, cancellationToken:cancellationToken)).Codm;
        }

        await Validate(command, cancellationToken);
        var employee = await CurrentEmployee();
        var request = await InitializeRequest(command, employee);

        DirectRegistration(request, employee);
        await DualStudents(request, employee, command.DualStudentsCodm, cancellationToken);
        StudentToEmployee(request);
        StudentToEmployeeToSeniorEmployee(request);
        EmployeeToSeniorEmployee(request);

        // save request
        await repo.InsertAsync(request, true, cancellationToken);

        // send command
        if ( request.Flow == RequestFlow.DirectRegistration ) {
            await SendCommand(request, cancellationToken);
            if ( request.RecordId == -1 ) {
                await repo.DeleteAsync(request, true, cancellationToken);
                throw new CommandValidationException("درخواست انجام نشد، ثبت تغییرات درخواستی با خطا مواجه شد.");
            }
        }

        return request.Id;
    }

    private async Task<Request> InitializeRequest(CreateRequestCommand command, EmployeeDto employee) {

        // entity
        var request = command.ToEntity();
        request.UserAgentInfo = contextAccessor.GetUserAgent().ToJson();

        // student family
        var studentFamily = await studentMobileRepository.GetFamily(request.Codm);
        request.Student = studentFamily.Where(x => x.DependentId == null).Select(x => x.FirstName + " " + x.LastName).Single();
        request.Dependent = studentFamily.Where(x => x.DependentId != null && x.DependentId == request.DependentId).Select(x => x.FirstName + " " + x.LastName).SingleOrDefault();

        // documents
        if ( command.Documents?.Any() == true ) {
            request.Documents = command.Documents.Select(x => new RequestDocument(x.FileId, x.Type)).ToList();
        }

        // source
        request.Source = request.CreatorPersonnelId.HasValue ? DataSource.Employee : DataSource.Student;

        // create
        request.PersonnelId = employee?.PersonnelId;
        request.CreatorPersonnelId = request.PersonnelId;
        request.Employee = employee?.FullName;

        // date time
        var dateTime = Csis.Utilities.PersianDateTime.Now;
        request.DateCreated = dateTime.ToString().StringDateToInt().Value;
        request.TimeCreated = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second);

        return request;
    }

    private void DirectRegistration(Request request, EmployeeDto employee) {

        if ( request.Flow != RequestFlow.DirectRegistration ) {
            return;
        }

        // request
        request.ApprovalStatus = ApprovalStatus.Approved;
        request.NextFlowApprover = RequestApprovalFlow.TheEnd;

        // approver
        request.DirectApproved(GetApproverRole(employee));
    }

    private async Task DualStudents(Request request, EmployeeDto employee, int[] codms, CancellationToken cancellation) {

        if ( request.Flow != RequestFlow.DualStudents ) {
            return;
        }

        // request
        request.ApprovalStatus = ApprovalStatus.Pending;
        request.NextFlowApprover = RequestApprovalFlow.Student;

        // approvers
        request.DirectApproved(GetApproverRole(employee));
        var dualStudents = await studentRepo.GetAllAsync(x => codms.Contains(x.Codm), cancellationToken: cancellation);
        request.NeedDualStudentApprovers(dualStudents);
    }

    private void StudentToEmployee(Request request) {

        if ( request.Flow != RequestFlow.StudentToEmployee ) {
            return;
        }

        // request
        request.ApprovalStatus = ApprovalStatus.Pending;
        request.NextFlowApprover = RequestApprovalFlow.Employee;

        // approvers
        request.DirectApproved(ApproverRole.Student);
        //request.NeedPersonnelApprover();
    }

    private void StudentToEmployeeToSeniorEmployee(Request request) {

        if ( request.Flow != RequestFlow.StudentToEmployeeToSeniorEmployee ) {
            return;
        }

        // request
        request.ApprovalStatus = ApprovalStatus.Pending;
        request.NextFlowApprover = RequestApprovalFlow.Employee;

        // approvers
        request.DirectApproved(ApproverRole.Student);
        //request.NeedPersonnelApprover();
        //request.NeedPersonnelApprover(ApproverRole.SeniorEmployee);
    }

    private void EmployeeToSeniorEmployee(Request request) {

        if ( request.Flow != RequestFlow.EmployeeToSeniorEmployee ) {
            return;
        }

        // request
        request.ApprovalStatus = ApprovalStatus.Pending;
        request.NextFlowApprover = RequestApprovalFlow.SeniorEmployee;

        // approvers
        request.DirectApproved(ApproverRole.Employee);
        //request.NeedPersonnelApprover(ApproverRole.SeniorEmployee);
    }

    private ApproverRole GetApproverRole(EmployeeDto employee) {
        var role = ApproverRole.Student;
        if ( employee != null ) {
            role = ApproverRole.Employee;
        }
        if ( employee?.IsSenior == true ) {
            role = ApproverRole.SeniorEmployee;
        }
        return role;
    }

    private async Task Validate(CreateRequestCommand command, CancellationToken cancellationToken) {
        var requestAlreadyExists = await repo.ExistsAsync(x =>
            x.Codm == command.Codm &&
            x.DependentId.GetValueOrDefault() == command.DependentId.GetValueOrDefault() &&
            x.Type == command.Type &&
            x.NextFlowApprover != RequestApprovalFlow.TheEnd
            , cancellationToken: cancellationToken);
        if ( requestAlreadyExists ) {
            throw new CommandValidationException("هم‌اکنون درخواستی مشابه در جریان است و امکان ثبت درخواست جدید وجود ندارد.");
        }
    }
}
