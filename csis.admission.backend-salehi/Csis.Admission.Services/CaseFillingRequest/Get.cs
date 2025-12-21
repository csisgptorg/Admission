using Csis.Admission.Domain.Enums;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Helpers;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Dtos.RequestService;

namespace Csis.Admission.Services;

/// <summary>دریافت درخواست ها</summary>
internal sealed partial class CaseFillingRequestService : ICaseFillingRequestService
{
    //TODO مشکل امنیتی - نباید بتواند هر درخواستی را ببیند! فقط درخواست خودش یا درخواست هایی که باید آنها را تایید کند
    public async Task<CaseFillingRequestDto> GetById(long id, CancellationToken cancellationToken) {
        var result = await repo.GetByIdAsync<CaseFillingRequestDto>(id, false, cancellationToken:cancellationToken);

        // تبدیل رشته JSON به مدل مشخص شده در jsonPayloadModel و تنظیم اطلاعات فایل
        await FileInfoHelper.SetRequestFilesInfoAsync([result], fileManagementService);
        result.PayloadModelObject = System.Text.Json.JsonSerializer.Deserialize<object>(result.JsonPayload);

        return result;
    }

    public async Task<List<CaseFillingRequestDto>> GetAllByCodmAsync(int? codm,bool? isCompleted, CancellationToken cancellationToken) {
        if ( codm == null ) {
            codm = int.Parse(await authenticatedUser.GetStudentCodmAsync());
        }
        var result = await repo.GetAllAsync<CaseFillingRequestDto>(x => 
            x.Codm == codm && 
            (
                isCompleted == null || 
                (isCompleted == true && x.NextFlowApprover==RequestApprovalFlow.TheEnd) || 
                (isCompleted == false && x.NextFlowApprover != RequestApprovalFlow.TheEnd)
            ), false, cancellationToken);

        // تبدیل رشته JSON به مدل مشخص شده در jsonPayloadModel و تنظیم اطلاعات فایل
        await FileInfoHelper.SetRequestFilesInfoAsync([.. result], fileManagementService);
        foreach ( var request in result ) {
            Console.WriteLine($"Request ID: {request.Id}, Model Type: {request.PayloadModelName},RequestType: {request.Type}");
            request.PayloadModelObject = System.Text.Json.JsonSerializer.Deserialize<object>(request.JsonPayload);
        }

        return result;
    }

    private async Task<CaseFillingEmployeeDto> CurrentEmployee() {
        var personnelId = await authenticatedUser.GetPersonnelIdAsync();
        if ( !personnelId.HasValue ) {
            return null;
        }

        try {
            var employee = await employeeService.GetEmployeeInfoAsync(personnelId.Value);
            var fullName = employee != null ? employee.FirstName + " " + employee.LastName : "فاقد مشخصات";
            var isSenior = await authenticatedUser.IsAuthorizedToAsync(PermissionsEnum.SeniorPersonnel);
            return new CaseFillingEmployeeDto(personnelId.Value, fullName, isSenior);
        } catch ( Exception e ) {
            return new CaseFillingEmployeeDto(personnelId.Value, "فاقد مشخصات", false);
        }

    }
}
