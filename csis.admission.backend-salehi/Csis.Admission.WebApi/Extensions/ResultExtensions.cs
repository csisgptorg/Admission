/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.WebApi.Extensions;

/// <summary>
/// Result extensions
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Convert any model to <see cref="Result{T}"/> structure
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="model"></param>
    /// <returns></returns>
    public static Result<TModel> ToResult<TModel>(this TModel model)
        where TModel : class {
        return Result<TModel>.Success(model);
    }

    /// <summary>
    /// Convert to result structure
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    /// <param name="pagedList"></param>
    /// <param name="mappedList"></param>
    /// <param name="routeValues"></param>
    /// <returns></returns>
    public static PaginatedResult<TDestination> ToPaginatedResult<T, TDestination>(this IPagedList<T> pagedList,
        IEnumerable<TDestination> mappedList, Dictionary<string, object> routeValues = default)
        where T : class where TDestination : class {

        return PaginatedResult<TDestination>.Success(mappedList, pagedList.TotalPages, pagedList.TotalCount,
            pagedList.PageIndex, pagedList.PageSize, routeValues);

    }

    /// <summary>
    /// Convert to result structure
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="pagedList"></param>
    /// <param name="routeValues"></param>
    /// <returns></returns>
    public static PaginatedResult<T> ToPaginatedResult<T>(this IPagedList<T> pagedList, Dictionary<string, object> routeValues = default)
        where T : class {

        return PaginatedResult<T>.Success(pagedList, pagedList.TotalPages, pagedList.TotalCount,
            pagedList.PageIndex, pagedList.PageSize, routeValues);

    }
}
