/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Domain.Common;

namespace Csis.Admission.Application.Common.Dtos;

/// <summary>
/// Base generic DTO that implements <see cref="IEmployeeAbstractInfoDto"/>
/// </summary>
/// <typeparam name="TDto"></typeparam>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract record BaseEmployeeAbstractInfoDto<TDto, TEntity, TKey> : BaseDto<TDto, TEntity, TKey>, IDto<TKey>, IMappable, IEmployeeAbstractInfoDto
    where TDto : class, new()
    where TEntity : class, IEntity<TKey>, new()
{
    /// <inheritdoc/>
    public int PersonnelId { get; set; }

    /// <inheritdoc/>
    public int? TakafolId { get; set; }

    /// <inheritdoc/>
    public string FirstName { get; set; }

    /// <inheritdoc/>
    public string LastName { get; set; }

    /// <inheritdoc/>
    public string Relation { get; set; }
}

/// <summary>
/// Base generic DTO that implements <see cref="IEmployeeAbstractInfoDto"/> with int key
/// </summary>
/// <typeparam name="TDto"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public abstract record BaseEmployeeAbstractInfoDto<TDto, TEntity> : BaseEmployeeAbstractInfoDto<TDto, TEntity, int>, IDto, IMappable, IEmployeeAbstractInfoDto
    where TDto : class, new()
    where TEntity : class, IEntity, new()
{
}
