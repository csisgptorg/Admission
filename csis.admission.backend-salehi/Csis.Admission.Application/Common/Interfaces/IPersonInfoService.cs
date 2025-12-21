/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Dtos;
using Csis.Paging;
using System.Linq.Expressions;

namespace Csis.Admission.Application.Common.Interfaces;

/// <summary>
/// سرویس دریافت اطلاعات افراد
/// </summary>
public partial interface IPersonInfoService
{
    /// <summary>
    /// دریافت اطلاعات کارمند و پر کردن فیلدهای مرتبط
    /// </summary>
    /// <param name="dtoList"></param>
    /// <returns></returns>
    Task<List<TDto>> FillEmployeeInfoAsync<TDto>(List<TDto> dtoList) where TDto : IEmployeeInfoDto;

    /// <summary>
    /// دریافت اطلاعات کارمند و پر کردن فیلدهای مرتبط
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <param name="pagedList">لیست صفحه بندی شده</param>
    /// <returns></returns>
    Task<IPagedList<TDto>> FillEmployeeInfoAsync<TDto>(IPagedList<TDto> pagedList) where TDto : class, IEmployeeInfoDto;

    /// <summary>
    /// دریافت اطلاعات کارمند و پر کردن فیلدهای مرتبط
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<TDto> FillEmployeeInfoAsync<TDto>(TDto dto) where TDto : IEmployeeInfoDto;

    /// <summary>
    /// دریافت اطلاعات کارمند و پر کردن فیلدهای مرتبط
    /// </summary>
    /// <param name="dtoList"></param>
    /// <returns></returns>
    Task<List<TDto>> FillEmployeeAbstractInfoAsync<TDto>(List<TDto> dtoList) where TDto : IEmployeeAbstractInfoDto;

    /// <summary>
    /// دریافت اطلاعات کارمند و پر کردن فیلدهای مرتبط
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <param name="pagedList">لیست صفحه بندی شده</param>
    /// <returns></returns>
    Task<IPagedList<TDto>> FillEmployeeAbstractInfoAsync<TDto>(IPagedList<TDto> pagedList) where TDto : class, IEmployeeAbstractInfoDto;

    /// <summary>
    /// دریافت اطلاعات کارمند و پر کردن فیلدهای مرتبط
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<TDto> FillEmployeeAbstractInfoAsync<TDto>(TDto dto) where TDto : IEmployeeAbstractInfoDto;

    /// <summary>
    /// دریافت اطلاعات طلبه و پر کردن فیلدهای مرتبط
    /// </summary>
    /// <param name="dtoList"></param>
    /// <returns></returns>
    Task<List<TDto>> FillStudentInfoAsync<TDto>(List<TDto> dtoList) where TDto : IStudentInfoDto;

    /// <summary>
    /// دریافت اطلاعات طلبه و پر کردن فیلدهای مرتبط
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <param name="pagedList">لیست صفحه بندی شده</param>
    /// <returns></returns>
    Task<IPagedList<TDto>> FillStudentInfoAsync<TDto>(IPagedList<TDto> pagedList) where TDto : class, IStudentInfoDto;

    /// <summary>
    /// دریافت اطلاعات طلبه و پر کردن فیلدهای مرتبط
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<TDto> FillStudentInfoAsync<TDto>(TDto dto) where TDto : IStudentInfoDto;

    /// <summary>
    /// دریافت اطلاعات طلبه و پر کردن فیلدهای مرتبط
    /// </summary>
    /// <param name="dtoList"></param>
    /// <returns></returns>
    Task<List<TDto>> FillStudentAbstractInfoAsync<TDto>(List<TDto> dtoList) where TDto : IStudentAbstractInfoDto;

    /// <summary>
    /// دریافت اطلاعات طلبه و پر کردن فیلدهای مرتبط
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <param name="pagedList">لیست صفحه بندی شده</param>
    /// <returns></returns>
    Task<IPagedList<TDto>> FillStudentAbstractInfoAsync<TDto>(IPagedList<TDto> pagedList) where TDto : class, IStudentAbstractInfoDto;

    /// <summary>
    /// دریافت اطلاعات طلبه و پر کردن فیلدهای مرتبط
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<TDto> FillStudentAbstractInfoAsync<TDto>(TDto dto) where TDto : IStudentAbstractInfoDto;

    /// <summary>
    /// دریافت اطلاعات کارمند و پر کردن فیلدهای مرتبط
    /// </summary>
    /// <param name="dtoList"></param>
    /// <param name="properties">پراپرتی‌ها جهت پر کردن داده</param>
    /// <returns></returns>
    Task<List<TDto>> FillUserInfoAsync<TDto>(List<TDto> dtoList, List<(Expression<Func<TDto, int>> IdProp, Expression<Func<TDto, string>> FullNameProp)> properties) where TDto : IUserInfoDto;

    /// <summary>
    /// دریافت اطلاعات کارمند و پر کردن فیلدهای مرتبط
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <param name="pagedList">لیست صفحه بندی شده</param>
    /// <param name="properties">پراپرتی‌ها جهت پر کردن داده</param>
    /// <returns></returns>
    Task<IPagedList<TDto>> FillUserInfoAsync<TDto>(IPagedList<TDto> pagedList, List<(Expression<Func<TDto, int>> IdProp, Expression<Func<TDto, string>> FullNameProp)> properties) where TDto : class, IUserInfoDto;

    /// <summary>
    /// دریافت اطلاعات کارمند و پر کردن فیلدهای مرتبط
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="properties">پراپرتی‌ها جهت پر کردن داده</param>
    /// <returns></returns>
    Task<TDto> FillUserInfoAsync<TDto>(TDto dto, List<(Expression<Func<TDto, int>> IdProp, Expression<Func<TDto, string>> FullNameProp)> properties) where TDto : IUserInfoDto;
}
