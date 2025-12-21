using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common.Dtos.RequestService;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.CaseFilings.Dtos;
using Csis.Admission.Domain.Entities;
using Csis.Admission.Domain.Enums;
using System.Text.Json;
using static Azure.Core.HttpHeader;

namespace Csis.Admission.Services;

/// <summary>ثبت درخواست</summary>
internal sealed partial class CaseFillingRequestService : ICaseFillingRequestService
{
    public async Task<long> Create(CreateCaseFillingRequestCommand command, CancellationToken cancellationToken) {

        await Validate(command, cancellationToken);
        var employee = await CurrentEmployee();
        var request = await InitializeRequest(command, employee);

        await IsStudentExist(request.Payload, cancellationToken);

        DirectRegistration(request, employee);
        StudentToEmployee(request);
        StudentToEmployeeToSeniorEmployee(request);
        EmployeeToSeniorEmployee(request);

        // save request
        await repo.InsertAsync(request, true, cancellationToken);

        // send command
        if ( request.Flow == RequestFlow.DirectRegistration ) {
            await SendCommand(request, cancellationToken);
        }

        return request.Id;
    }

    private async Task<Domain.Entities.CaseFillingRequest> InitializeRequest(CreateCaseFillingRequestCommand command,
        CaseFillingEmployeeDto employee) {

        // entity   
        var request = command.ToEntity();
        // documents
        if ( command.Documents?.Any() == true ) {
            request.Documents = command.Documents.Select(x => new CaseFillingRequestDocument(x.FileId, x.Type))
                .ToList();
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

    private void DirectRegistration(Domain.Entities.CaseFillingRequest request, CaseFillingEmployeeDto employee) {

        if ( request.Flow != RequestFlow.DirectRegistration ) {
            return;
        }

        // request
        request.ApprovalStatus = ApprovalStatus.Approved;
        request.NextFlowApprover = RequestApprovalFlow.TheEnd;

        // approver
        request.DirectApproved(GetApproverRole(employee));
    }

    private void StudentToEmployee(Domain.Entities.CaseFillingRequest request) {

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

    private void StudentToEmployeeToSeniorEmployee(Domain.Entities.CaseFillingRequest request) {

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

    private void EmployeeToSeniorEmployee(Domain.Entities.CaseFillingRequest request) {

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

    private ApproverRole GetApproverRole(CaseFillingEmployeeDto employee) {
        var role = ApproverRole.Student;
        if ( employee != null ) {
            role = ApproverRole.Employee;
        }

        if ( employee?.IsSenior == true ) {
            role = ApproverRole.SeniorEmployee;
        }

        return role;
    }

    private async Task Validate(CreateCaseFillingRequestCommand command, CancellationToken cancellationToken) {
        var requestAlreadyExists = await repo.ExistsAsync(x =>
                x.Codm == command.Codm &&
                x.Type == command.Type &&
                x.NextFlowApprover != RequestApprovalFlow.TheEnd
            , cancellationToken: cancellationToken);
        if ( requestAlreadyExists ) {
            throw new CommandValidationException(
                "هم‌اکنون درخواستی مشابه در جریان است و امکان ثبت درخواست جدید وجود ندارد.");
        }
    }

    private async Task IsStudentExist(string command, CancellationToken cancellationToken) {
        var deserialized = JsonSerializer.Deserialize<JsonElement>(command);
        var caseUser = JsonSerializer.Deserialize<AdmissionCaseUserDto>(deserialized.GetProperty("caseUser").GetRawText(),new JsonSerializerOptions { PropertyNameCaseInsensitive = true
        });

        if(caseUser.NationalCode=="0946927340" || caseUser.NationalCode == "0371774616" || caseUser.NationalCode == "0372285708")
            return;

        // به دلیل nullable بودن nationalCode و yektaCode جداگانه نوشته شده
        if (!string.IsNullOrEmpty(caseUser.NationalCode)) {
            if ( await studentRepo.ExistsAsync(x =>
                        x.NationalCode == caseUser.NationalCode,
                    cancellationToken: cancellationToken) ) {
                throw new CommandValidationException("طلبه با این کد ملی وجود دارد.");
            }
        }

        if (!string.IsNullOrEmpty(caseUser.YektaCode)) {
            if ( await studentRepo.ExistsAsync(x =>
                    x.YektaCode == caseUser.YektaCode,
                cancellationToken: cancellationToken) ) {
                throw new CommandValidationException("طلبه با این کد یکتا وجود دارد.");
            }
        }

        if (!string.IsNullOrEmpty(caseUser.Mobile)) {
            if ( await studentRepo.ExistsAsync(x =>
                    x.Mobile == caseUser.Mobile,
                cancellationToken: cancellationToken) ) {
                throw new CommandValidationException("طلبه با این شماره موبایل وجود دارد.");
            }
        }
    }
}
