/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Domain.Common;

namespace Csis.Admission.Application.Common.Dtos;

/// <summary>
/// Base generic DTO that implements <see cref="IStudentAbstractInfoDto"/>
/// </summary>
/// <typeparam name="TDto"></typeparam>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract record BaseStudentAbstractInfoDto<TDto, TEntity, TKey> : BaseDto<TDto, TEntity, TKey>, IDto<TKey>, IMappable, IStudentAbstractInfoDto
    where TDto : class, new()
    where TEntity : class, IEntity<TKey>, new()
{

    /// <inheritdoc/>
    public string Codm { get; set; }

    /// <inheritdoc/>
    public int? TakafolId { get; set; }

    /// <inheritdoc/>
    public string FirstName { get; set; }

    /// <inheritdoc/>
    public string LastName { get; set; }

    /// <inheritdoc/>
    public string Relation { get; set; }

    /// <inheritdoc/>
    public int? BranchId { get; set; }

    /// <inheritdoc/>
    public string BranchName { get; set; }
}

/// <summary>
/// Base generic DTO that implements <see cref="IStudentAbstractInfoDto"/> with int key
/// </summary>
/// <typeparam name="TDto"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public abstract record BaseStudentAbstractInfoDto<TDto, TEntity> : BaseStudentAbstractInfoDto<TDto, TEntity, int>, IDto, IMappable, IStudentAbstractInfoDto
    where TDto : class, new()
    where TEntity : class, IEntity, new()
{
}
