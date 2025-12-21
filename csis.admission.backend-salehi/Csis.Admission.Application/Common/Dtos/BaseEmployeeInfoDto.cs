/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Domain.Common;
using Csis.Shared.Kernel.Public.Models.Employee;

namespace Csis.Admission.Application.Common.Dtos;

/// <summary>
/// Base generic DTO that implements <see cref="IEmployeeInfoDto"/>
/// </summary>
/// <typeparam name="TDto"></typeparam>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract record BaseEmployeeInfoDto<TDto, TEntity, TKey> : BaseDto<TDto, TEntity, TKey>, IDto<TKey>, IMappable, IEmployeeInfoDto
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

    /// <inheritdoc/>
    public Relation RelationId { get; set; }

    /// <inheritdoc/>
    public int? BranchId { get; set; }

    /// <inheritdoc/>
    public string BranchName { get; set; }

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
}

/// <summary>
/// Base generic DTO that implements <see cref="IEmployeeInfoDto"/> with int key
/// </summary>
/// <typeparam name="TDto"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public abstract record BaseEmployeeInfoDto<TDto, TEntity> : BaseEmployeeInfoDto<TDto, TEntity, int>, IDto, IMappable, IEmployeeInfoDto
    where TDto : class, new()
    where TEntity : class, IEntity, new()
{
}
