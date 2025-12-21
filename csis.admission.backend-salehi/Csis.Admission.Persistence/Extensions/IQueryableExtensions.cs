/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using AutoMapper;
using Csis.Admission.Application.Common;
using Csis.Admission.Domain.Common;
using Csis.Utilities.Extensions;
using System.Linq.Expressions;
using System.Reflection;

namespace Csis.Admission.Persistence.Extensions;

/// <summary>
/// IQueryable extension methods
/// </summary>
internal static class IQueryableExtensions
{
    #region Private Variables
    private static readonly MethodInfo _containsMethod = typeof(string).GetMethod("Contains", [typeof(string)]);
    private static readonly MethodInfo _startsWithMethod = typeof(string).GetMethod("StartsWith", [typeof(string), typeof(StringComparison)]);
    private static readonly MethodInfo _endsWithMethod = typeof(string).GetMethod("EndsWith", [typeof(string)]);
    #endregion

    #region Private Methods
    private static Expression GetExpression(ParameterExpression param, SearchFilter searchFilter) {
        MemberExpression member;
        if ( searchFilter.Field.Contains('.') ) {
            member = (MemberExpression) searchFilter.Field.Split(".").Aggregate((Expression) param, GetCaseInsensitivePropertyExpression);
        } else {
            member = GetCaseInsensitivePropertyExpression(param, searchFilter.Field);
        }

        object typedValue;
        var memberType = Nullable.GetUnderlyingType(member.Type) ?? member.Type;

        if ( memberType.IsEnum ) {
            typedValue = Enum.Parse(memberType, searchFilter.Value);
        } else if ( memberType == typeof(DateTime) ) {
            typedValue = DateTime.Parse(searchFilter.Value);
        } else if ( memberType == typeof(DateOnly) ) {
            typedValue = DateOnly.Parse(searchFilter.Value);
        } else if ( memberType == typeof(Guid) ) {
            typedValue = Guid.TryParse(searchFilter.Value, out var guidValue) ? guidValue : Guid.Empty;
        } else {
            typedValue = Convert.ChangeType(searchFilter.Value, memberType);
        }

        var constant = Expression.Constant(typedValue);

        Expression expression;

        if ( Nullable.GetUnderlyingType(member.Type) is not null ) {
            // Handle nullable properties
            var hasValue = Expression.Property(member, "HasValue");
            var value = Expression.Property(member, "Value");

            expression = Expression.Condition(
                Expression.Property(member, "HasValue"),
                GetComparisonExpression(value, constant, searchFilter.Operator),
                Expression.Constant(false)
            );
        } else {
            // Regular comparison for non-nullable properties
            expression = GetComparisonExpression(member, constant, searchFilter.Operator);
        }

        return expression;
    }

    private static Expression GetComparisonExpression(MemberExpression member, ConstantExpression constant, SearchOperator @operator) {
        return @operator switch {
            SearchOperator.Equal => Expression.Equal(member, constant),
            SearchOperator.GreaterThan => Expression.GreaterThan(member, constant),
            SearchOperator.GreaterThanOrEqual => Expression.GreaterThanOrEqual(member, constant),
            SearchOperator.LessThan => Expression.LessThan(member, constant),
            SearchOperator.LessThanOrEqual => Expression.LessThanOrEqual(member, constant),
            SearchOperator.NotEqual => Expression.NotEqual(member, constant),
            SearchOperator.Contains => Expression.Call(member, _containsMethod, constant),
            SearchOperator.StartsWith => Expression.Call(member, _startsWithMethod, constant, Expression.Constant(StringComparison.InvariantCultureIgnoreCase)),
            SearchOperator.EndsWith => Expression.Call(member, _endsWithMethod, constant),
            _ => Expression.Equal(member, constant),
        };
    }

    /// <summary>
    /// Get property expression case insensitive
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="param"></param>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private static MemberExpression GetCaseInsensitivePropertyExpression(Expression param, string propertyName) {
        var propertyInfo = param.Type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        if ( propertyInfo is not null ) {
            return Expression.Property(param, propertyInfo);
        }

        throw new Exception($"Property with name '{propertyName}' not found on '{param.Type.Name}'.");
    }
    #endregion

    #region Internal Extension Methods
    /// <summary>
    /// Create paged list from <see cref="IQueryable{T}"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    /// <param name="pageIndex">Page index</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="sortExpression">Expression used to sort entities</param>
    /// <param name="propertyMappings">Map properties in sort expression to real property names</param>
    /// <param name="ignoredProperties">List of property name that should be excluded from sort expression</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    internal static async Task<IPagedList<TEntity>> ToPagedListAsync<TEntity, TKey>(
        this IQueryable<TEntity> query,
        int pageIndex,
        int pageSize,
        string sortExpression = "",
        Dictionary<string, string> propertyMappings = default,
        IEnumerable<string> ignoredProperties = default,
        CancellationToken cancellationToken = default) where TEntity : class, IEntity<TKey> {
        return await PagedList.CreateAsync(query, pageIndex, pageSize, sortExpression, "", propertyMappings, ignoredProperties, cancellationToken);
    }

    /// <summary>
    /// Create paged list from <see cref="IQueryable{T}"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    /// <param name="pageIndex">Page index</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="sortExpression">Expression used to sort entities</param>
    /// <param name="propertyMappings">Map properties in sort expression to real property names</param>
    /// <param name="ignoredProperties">List of property name that should be excluded from sort expression</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    internal static async Task<IPagedList<TEntity>> ToPagedListAsync<TEntity>(
        this IQueryable<TEntity> query,
        int pageIndex,
        int pageSize,
        string sortExpression = "",
        Dictionary<string, string> propertyMappings = default,
        IEnumerable<string> ignoredProperties = default,
        CancellationToken cancellationToken = default) where TEntity : class, IEntity {
        return await PagedList.CreateAsync(query, pageIndex, pageSize, sortExpression, "", propertyMappings, ignoredProperties, cancellationToken);
    }

    /// <summary>
    /// Create paged list from <see cref="IOrderedQueryable{T}"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    /// <param name="pageIndex">Page index</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    internal static async Task<IPagedList<TEntity>> ToPagedListAsync<TEntity, TKey>(
        this IOrderedQueryable<TEntity> query,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken = default) where TEntity : class, IEntity<TKey> {
        return await PagedList.CreateAsync(query, pageIndex, pageSize, cancellationToken);
    }

    /// <summary>
    /// Create paged list from <see cref="IOrderedQueryable{T}"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    /// <param name="pageIndex">Page index</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    internal static async Task<IPagedList<TEntity>> ToPagedListAsync<TEntity>(
        this IOrderedQueryable<TEntity> query,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken = default) where TEntity : class, IEntity {
        return await PagedList.CreateAsync(query, pageIndex, pageSize, cancellationToken);
    }

    /// <summary>
    /// Create paged list from <see cref="IQueryable{T}"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto">Mapped type</typeparam>
    /// <param name="query"></param>
    /// <param name="pageIndex">Page index</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="sortExpression">Expression used to sort entities</param>
    /// <param name="propertyMappings">Map properties in sort expression to real property names</param>
    /// <param name="ignoredProperties">List of property name that should be excluded from sort expression</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    internal static async Task<IPagedList<TDto>> ToPagedListAsync<TEntity, TKey, TDto>(
        this IQueryable<TEntity> query,
        IConfigurationProvider configurationProvider,
        int pageIndex,
        int pageSize,
        string sortExpression = "",
        Dictionary<string, string> propertyMappings = default,
        IEnumerable<string> ignoredProperties = default,
        CancellationToken cancellationToken = default) where TEntity : class, IEntity<TKey>
        where TDto : class, new() {
        return await PagedList.CreateAsync<TEntity, TDto>(query, pageIndex, pageSize, sortExpression, configurationProvider, "", propertyMappings, ignoredProperties, cancellationToken);
    }

    /// <summary>
    /// Apply dynamic search filters
    /// </summary>
    /// <typeparam name="TEntity">Entity type that dynamic search is applied on</typeparam>
    /// <typeparam name="TKey">Primary key type of entity</typeparam>
    /// <param name="source"></param>
    /// <param name="searchFilters">List of search filters</param>
    /// <returns></returns>
    internal static IQueryable<TEntity> ApplySearchFilters<TEntity, TKey>(
        this IQueryable<TEntity> source,
        List<SearchFilter> searchFilters) where TEntity : class, IEntity<TKey> where TKey : IEquatable<TKey> {
        try {
            if ( searchFilters is null || searchFilters.Count == 0 ) {
                return source;
            }

            var param = Expression.Parameter(typeof(TEntity), "e");

            Expression body = null;
            foreach ( var filter in searchFilters ) {
                if ( filter is null || filter.Value is null || !filter.Field.HasValue() ) {
                    continue;
                }

                try {
                    var expression = GetExpression(param, filter);

                    body = body is null ? expression : Expression.AndAlso(body, expression);
                } catch {
                    continue;
                }
            }

            if ( body is null ) {
                return source;
            }

            var lambda = Expression.Lambda<Func<TEntity, bool>>(body, param);
            return source.Where(lambda);
        } catch {
            return source;
        }
    }
    #endregion
}
