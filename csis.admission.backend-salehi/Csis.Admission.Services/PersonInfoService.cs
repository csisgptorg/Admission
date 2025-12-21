/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Domain.Enums;
using Csis.Authorization.Services;
using Csis.Paging;
using Csis.Utilities.Extensions;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Reflection;

namespace Csis.Admission.Services;

/// <summary>
/// سرویس دریافت اطلاعات افراد
/// </summary>
internal sealed partial class PersonInfoService(
    IStudentDataService studentDataService,
    IEmployeeDataService employeeDataService,
    ICsisAuthorizationService csisAuthorizationService,
    ILogger<PersonInfoService> logger) : IPersonInfoService
{
    public async Task<List<TDto>> FillEmployeeInfoAsync<TDto>(List<TDto> dtoList) where TDto : IEmployeeInfoDto {
        if ( dtoList is null || dtoList.Count == 0 ) {
            return [];
        }

        // دریافت اطلاعات افراد
        var codmList = dtoList.Where(x => !x.TakafolId.HasValue || x.TakafolId.Value == 0).Select(x => x.PersonnelId);
        var takafolIds = dtoList.Where(x => x.TakafolId.HasValue && x.TakafolId.Value > 0).Select(x => x.TakafolId.Value);
        var people = await employeeDataService.GetEmployeesAndDependantsGroupInfoAsync(codmList, takafolIds);

        // دریافت لیست شعب
        var branches = await studentDataService.GetCsisBranchesAsync() ?? [];

        foreach ( var dto in dtoList ) {
            dto.TakafolId = dto.TakafolId <= 0 ? null : dto.TakafolId;
            var person = people
                .Where(x => x.PersonnelId == dto.PersonnelId)
                .Where(x => x.TakafolId == dto.TakafolId)
                .FirstOrDefault();

            if ( person is not null ) {
                dto.FirstName = person.FirstName;
                dto.LastName = person.LastName ?? "";

                if ( !dto.BranchId.HasValue ) {
                    dto.BranchId = person.BranchId;
                }

                dto.Relation = person.Relation;
                dto.Relation = person.Relation.HasValue() ? person.Relation : "سرپرست";
                dto.NationalId = person.NationalId;
                dto.Gender = person.Gender == 1 ? Gender.Male : Gender.Female;
                dto.GenderTitle = person.GenderTitle;
                dto.Mobile = person.Mobile;
            }

            var branch = branches.Where(x => x.Code == dto.BranchId).FirstOrDefault();
            if ( branch is not null ) {
                dto.BranchName = branch.Title;
            }

        }

        return dtoList;
    }

    async Task<IPagedList<TDto>> IPersonInfoService.FillEmployeeInfoAsync<TDto>(IPagedList<TDto> pagedList) {
        if ( pagedList is null || !pagedList.Any() ) {
            return pagedList;
        }

        var result = await FillEmployeeInfoAsync([.. pagedList]);

        return PagedList.Create(result, pagedList.PageIndex, pagedList.PageSize, pagedList.TotalCount, pagedList.SortExpression);
    }

    public async Task<TDto> FillEmployeeInfoAsync<TDto>(TDto dto) where TDto : IEmployeeInfoDto {
        return (await FillEmployeeInfoAsync([dto])).FirstOrDefault();
    }

    public async Task<List<TDto>> FillEmployeeAbstractInfoAsync<TDto>(List<TDto> dtoList) where TDto : IEmployeeAbstractInfoDto {
        if ( dtoList is null || dtoList.Count == 0 ) {
            return [];
        }

        // دریافت اطلاعات افراد
        var codmList = dtoList.Where(x => !x.TakafolId.HasValue || x.TakafolId.Value == 0).Select(x => x.PersonnelId);
        var takafolIds = dtoList.Where(x => x.TakafolId.HasValue && x.TakafolId.Value > 0).Select(x => x.TakafolId.Value);
        var people = await employeeDataService.GetEmployeesAndDependantsGroupInfoAsync(codmList, takafolIds);

        foreach ( var dto in dtoList ) {
            dto.TakafolId = dto.TakafolId <= 0 ? null : dto.TakafolId;
            var person = people
                .Where(x => x.PersonnelId == dto.PersonnelId)
                .Where(x => x.TakafolId == dto.TakafolId)
                .FirstOrDefault();

            if ( person is not null ) {
                dto.FirstName = person.FirstName;
                dto.LastName = person.LastName ?? "";
                dto.Relation = person.Relation;
                dto.Relation = person.Relation.HasValue() ? person.Relation : "سرپرست";
            }
        }

        return dtoList;
    }

    async Task<IPagedList<TDto>> IPersonInfoService.FillEmployeeAbstractInfoAsync<TDto>(IPagedList<TDto> pagedList) {
        if ( pagedList is null || !pagedList.Any() ) {
            return pagedList;
        }

        var result = await FillEmployeeAbstractInfoAsync([.. pagedList]);

        return PagedList.Create(result, pagedList.PageIndex, pagedList.PageSize, pagedList.TotalCount, pagedList.SortExpression);
    }

    public async Task<TDto> FillEmployeeAbstractInfoAsync<TDto>(TDto dto) where TDto : IEmployeeAbstractInfoDto {
        return (await FillEmployeeAbstractInfoAsync([dto])).FirstOrDefault();
    }

    public async Task<List<TDto>> FillStudentInfoAsync<TDto>(List<TDto> dtoList) where TDto : IStudentInfoDto {
        if ( dtoList is null || dtoList.Count == 0 ) {
            return [];
        }

        // دریافت اطلاعات افراد
        var codmList = dtoList.Where(x => !x.TakafolId.HasValue || x.TakafolId.Value == 0).Select(x => x.Codm);
        var takafolIds = dtoList.Where(x => x.TakafolId.HasValue && x.TakafolId.Value > 0).Select(x => x.TakafolId.Value);
        var people = await studentDataService.GetStudentsAndDependantsGroupInfoAsync(codmList, takafolIds);

        // دریافت لیست شعب
        var branches = await studentDataService.GetCsisBranchesAsync() ?? [];

        foreach ( var dto in dtoList ) {
            dto.TakafolId = dto.TakafolId <= 0 ? null : dto.TakafolId;
            var person = people
                .Where(x => x.Codm == dto.Codm)
                .Where(x => x.TakafolId == dto.TakafolId)
                .FirstOrDefault();

            if ( person is not null ) {
                dto.FirstName = person.FirstName;
                dto.LastName = person.LastName ?? "";

                if ( !dto.BranchId.HasValue ) {
                    dto.BranchId = person.BranchId;
                }

                dto.Relation = person.Relation.HasValue() ? person.Relation : "سرپرست";
                dto.RelationId = person.RelationId;
                dto.NationalId = person.NationalIdOrYektaCode;
                dto.Nationality = person.Nationality == 1 ? Nationality.Iranian : Nationality.NonIranian;
                dto.NationalityTitle = person.NationalityTitle;
                dto.Gender = person.Gender == 1 ? Gender.Male : Gender.Female;
                dto.GenderTitle = person.GenderTitle;
                dto.Mobile = person.Mobile;
                dto.BirthDate = person.BirthDate;
            }

            var branch = branches.Where(x => x.Code == dto.BranchId).FirstOrDefault();
            if ( branch is not null ) {
                dto.BranchName = branch.Title;
            }

        }

        return dtoList;
    }

    public async Task<IPagedList<TDto>> FillStudentInfoAsync<TDto>(IPagedList<TDto> pagedList) where TDto : class, IStudentInfoDto {
        if ( pagedList is null || !pagedList.Any() ) {
            return pagedList;
        }

        var result = await FillStudentInfoAsync([.. pagedList]);

        return PagedList.Create(result, pagedList.PageIndex, pagedList.PageSize, pagedList.TotalCount, pagedList.SortExpression);
    }

    public async Task<TDto> FillStudentInfoAsync<TDto>(TDto dto) where TDto : IStudentInfoDto {
        return (await FillStudentInfoAsync([dto])).FirstOrDefault();
    }

    public async Task<List<TDto>> FillStudentAbstractInfoAsync<TDto>(List<TDto> dtoList) where TDto : IStudentAbstractInfoDto {
        if ( dtoList is null || dtoList.Count == 0 ) {
            return [];
        }

        // دریافت اطلاعات افراد
        var codmList = dtoList.Where(x => !x.TakafolId.HasValue || x.TakafolId.Value == 0).Select(x => x.Codm);
        var takafolIds = dtoList.Where(x => x.TakafolId.HasValue && x.TakafolId.Value > 0).Select(x => x.TakafolId.Value);
        var people = await studentDataService.GetStudentsAndDependantsGroupInfoAsync(codmList, takafolIds);
        var branches = await studentDataService.GetCsisBranchesAsync() ?? [];

        foreach ( var dto in dtoList ) {
            dto.TakafolId = dto.TakafolId <= 0 ? null : dto.TakafolId;
            var person = people
                .Where(x => x.Codm == dto.Codm)
                .Where(x => x.TakafolId == dto.TakafolId)
                .FirstOrDefault();

            if ( person is not null ) {
                dto.FirstName = person.FirstName;
                dto.LastName = person.LastName ?? "";
                dto.Relation = person.Relation.HasValue() ? person.Relation : "سرپرست";
                dto.BranchId = person.BranchId;

                var branch = branches.Where(x => x.Code == dto.BranchId).FirstOrDefault();
                if ( branch is not null ) {
                    dto.BranchName = branch.Title;
                }
            }
        }

        return dtoList;
    }

    public async Task<IPagedList<TDto>> FillStudentAbstractInfoAsync<TDto>(IPagedList<TDto> pagedList) where TDto : class, IStudentAbstractInfoDto {
        if ( pagedList is null || !pagedList.Any() ) {
            return pagedList;
        }

        var result = await FillStudentAbstractInfoAsync([.. pagedList]);

        return PagedList.Create(result, pagedList.PageIndex, pagedList.PageSize, pagedList.TotalCount, pagedList.SortExpression);
    }

    public async Task<TDto> FillStudentAbstractInfoAsync<TDto>(TDto dto) where TDto : IStudentAbstractInfoDto {
        return (await FillStudentAbstractInfoAsync([dto])).FirstOrDefault();
    }

    public async Task<List<TDto>> FillUserInfoAsync<TDto>(List<TDto> dtoList, List<(Expression<Func<TDto, int>> IdProp, Expression<Func<TDto, string>> FullNameProp)> properties) where TDto : IUserInfoDto {
        if ( dtoList is null || dtoList.Count == 0 ) {
            logger.LogDebug("No data to fill user info.");
            return [];
        }

        var userIds = new List<int>();
        foreach ( var dto in dtoList ) {
            foreach ( var (IdProp, _) in properties ) {
                userIds.Add(IdProp.Compile().Invoke(dto));
            }
        }

        userIds = [.. userIds.Where(x => x > 0).Distinct()];
        logger.LogDebug("Getting user info  for users {@users}", userIds);

        var users = (await csisAuthorizationService.GetUserInfoByUserIdsAsync(userIds))?.Data ?? [];
        logger.LogDebug("User info received: {@users}", users);

        foreach ( var dto in dtoList ) {
            foreach ( var (IdProp, FullNameProp) in properties ) {
                var userId = IdProp.Compile().Invoke(dto);
                if ( userId <= 0 ) {
                    continue;
                }

                var user = users.FirstOrDefault(x => x.Id == userId);

                if ( user is not null ) {
                    if ( FullNameProp.Body is MemberExpression memberExpression && memberExpression.Member is PropertyInfo propertyInfo ) {
                        propertyInfo.SetValue(dto, $"{user.FirstName} {user.LastName}");
                        logger.LogDebug("Value set for userId {userId}: {fullName}", userId, $"{user.FirstName} {user.LastName}");
                    } else {
                        throw new Exception("The provided expression is not a valid property expression");
                    }
                }
            }
        }

        return dtoList;
    }

    public async Task<IPagedList<TDto>> FillUserInfoAsync<TDto>(IPagedList<TDto> pagedList, List<(Expression<Func<TDto, int>> IdProp, Expression<Func<TDto, string>> FullNameProp)> properties) where TDto : class, IUserInfoDto {
        if ( pagedList is null || !pagedList.Any() ) {
            return pagedList;
        }

        var result = await FillUserInfoAsync([.. pagedList], properties);

        return PagedList.Create(result, pagedList.PageIndex, pagedList.PageSize, pagedList.TotalCount, pagedList.SortExpression);
    }

    public async Task<TDto> FillUserInfoAsync<TDto>(TDto dto, List<(Expression<Func<TDto, int>> IdProp, Expression<Func<TDto, string>> FullNameProp)> properties) where TDto : IUserInfoDto {
        return (await FillUserInfoAsync([dto], properties)).FirstOrDefault();
    }
}
