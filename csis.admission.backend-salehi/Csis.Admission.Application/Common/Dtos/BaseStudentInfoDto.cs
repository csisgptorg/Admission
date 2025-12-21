/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Domain.Common;

namespace Csis.Admission.Application.Common.Dtos;

/// <summary>
/// Base generic DTO that implements <see cref="IStudentInfoDto"/>
/// </summary>
/// <typeparam name="TDto"></typeparam>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract record BaseStudentInfoDto<TDto, TEntity, TKey> : BaseDto<TDto, TEntity, TKey>, IDto<TKey>, IMappable, IStudentInfoDto
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
    public int? BranchId { get; set; }

    /// <inheritdoc/>
    public string BranchName { get; set; }

    /// <inheritdoc/>
    public string Relation { get; set; }

    /// <inheritdoc/>
    public int? RelationId { get; set; }

    /// <inheritdoc/>
    public string NationalId { get; set; }

    /// <inheritdoc/>
    public Nationality Nationality { get; set; }

    /// <inheritdoc/>
    public string NationalityTitle { get; set; }

    /// <inheritdoc/>
    public Gender Gender { get; set; }

    /// <inheritdoc/>
    public string GenderTitle { get; set; }

    /// <inheritdoc/>
    public string Mobile { get; set; }

    /// <inheritdoc/>
    public string BirthDate { get; set; }
}

/// <summary>
/// Base generic DTO that implements <see cref="IStudentInfoDto"/> with int key
/// </summary>
/// <typeparam name="TDto"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public abstract record BaseStudentInfoDto<TDto, TEntity> : BaseStudentInfoDto<TDto, TEntity, int>, IDto, IMappable, IStudentInfoDto
    where TDto : class, new()
    where TEntity : class, IEntity, new()
{
}
