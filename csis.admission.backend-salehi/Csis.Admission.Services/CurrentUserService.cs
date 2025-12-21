using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Enums;
using Csis.Authorization.Services;

namespace Csis.Admission.Services;

/// <summary>سرویس اطلاعات کاربر جاری</summary>
internal sealed class CurrentUserService(ICsisAuthenticatedUserService authenticatedUser) : ICurrentUserService
{
    private int? _userId;
    private int? _delegatedUserId;
    private bool _userIdInitialized = false;
    private bool _delegatedUserIdInitialized = false;

    public async Task<int?> GetDelegatedUserIdAsync() {
        return await authenticatedUser.GetDelegatedUserIdAsync(throwExceptionIfFailed: false);
    }

    public async Task<int?> GetUserIdAsync() {
        return await authenticatedUser.GetUserIdAsync(throwExceptionIfFailed: false);
    }

    public async Task<bool> IsSenior() {
        return await authenticatedUser.IsAuthorizedToAsync(PermissionsEnum.SeniorPersonnel);
    }

    public async Task<bool> IsEmployee() {
        return await authenticatedUser.IsEmployeeLoggedInAsync();
    }

    public async Task<int?> Codm() {
        if ( _ = int.TryParse(await authenticatedUser.GetStudentCodmAsync(), out var codm) ) {
            return codm;
        }
        return null;
    }

    public async Task SetCodm(object obj) {
        var codmProp = obj.GetType().GetProperty("Codm");
        var codm = await Codm();
        if ( codm > 0 ) { codmProp.SetValue(obj, codm); }
    }

    public async Task<int?> PersonnelId() {
        var personnelId = await authenticatedUser.GetPersonnelIdAsync();
        if ( personnelId > 0 ) {
            return personnelId;
        }
        return null;
    }

    public async Task<bool> IsStudent() {
        return await authenticatedUser.IsStudentLoggedInAsync();
    }

    public async Task<int> GetEmployeeBranchIdAsync() {
        return await authenticatedUser.GetBranchIdAsync()
            ?? throw new EmptyBranchIdException();
    }

    public async Task<int> GetStudentBranchIdAsync() {
        return await authenticatedUser.GetStudentBranchIdAsync()
            ?? throw new EmptyBranchIdException();
    }

    public async Task<int?> GetPersonnelIdAsync() {
        return await authenticatedUser.GetPersonnelIdAsync();
    }

    public async Task<string> GetCodmAsync() {
        return await authenticatedUser.GetStudentCodmAsync();
    }

    public async Task<bool> IsEmployeeAsync() {
        return await authenticatedUser.IsEmployeeLoggedInAsync();
    }

    public async Task<bool> IsStudentAsync() {
        return await authenticatedUser.IsStudentLoggedInAsync();
    }

    public async Task<bool> IsAuthorizedAsync(PermissionsEnum permission) {
        return await authenticatedUser.IsAuthorizedToAsync(permission);
    }

    public async Task<bool> HasAccessToThisApplicationAsync() {
        return await authenticatedUser.IsCurrentApplicationAllowedAsync();
    }
}
