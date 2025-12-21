/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Domain.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Concurrent;
using System.Reflection;

namespace Csis.Admission.Persistence.Extensions;
internal static partial class ChangeTrackerExtensions
{
    private static readonly ConcurrentDictionary<Type, (MethodInfo setCreatedOn, MethodInfo setCreatedById, MethodInfo update, PropertyInfo deletedProp)> _entityInfo = [];

    internal static void SetBaseEntityProperties(this ChangeTracker changeTracker, int? currentUserId, int? delegatedUserId, DateTime currentDate) {
        var iEntityType = typeof(IEntity);
        var iGenericEntityType = typeof(IEntity<>);

        var entries = changeTracker
            .Entries()
            .Where(e => iEntityType.IsAssignableFrom(e.Entity.GetType()) ||
                        e.Entity.GetType().GetInterfaces().Any(i =>
                            i.IsGenericType && i.GetGenericTypeDefinition() == iGenericEntityType))
            .Where(e => e.State is EntityState.Added or EntityState.Modified);

        foreach ( var entityEntry in entries ) {
            if ( entityEntry.State is EntityState.Added ) {
                if ( entityEntry.Entity is IEntity entity ) {
                    entity.SetCreatedOn(currentDate);
                    entity.SetCreatedById(currentUserId, delegatedUserId);
                } else {
                    var entityType = entityEntry.Entity.GetType();
                    var (setCreatedOnMethod, setCreatedById, _, _) = GetEntityInfo(entityType);

                    setCreatedOnMethod.Invoke(entityEntry.Entity, [currentDate]);

                    if ( currentUserId.HasValue ) {
                        setCreatedById.Invoke(entityEntry.Entity, [currentUserId.Value, delegatedUserId]);
                    }
                }
            } else {
                if ( entityEntry.Entity is IEntity entity ) {

                    // Do not set update properties when soft deleted
                    if ( entity is ISoftDeletedEntity softDeletedEntity && softDeletedEntity.Deleted ) {
                        continue;
                    }

                    entity.Update(currentUserId, delegatedUserId, currentDate);
                } else {
                    var entityType = entityEntry.Entity.GetType();
                    var (_, _, updateMethod, deletedProp) = GetEntityInfo(entityType);

                    // Do not set update properties when soft deleted
                    if ( deletedProp is not null && Equals(deletedProp.GetValue(entityEntry.Entity), true) ) {
                        continue;
                    }

                    updateMethod.Invoke(entityEntry.Entity, [currentUserId, delegatedUserId, currentDate]);
                }
            }
        }
    }

    private static (MethodInfo setCreatedOn, MethodInfo setCreatedById, MethodInfo update, PropertyInfo deletedProp) GetEntityInfo(Type entityType) {
        if ( _entityInfo.TryGetValue(entityType, out var result) ) {
            return result;
        } else {
            var setCreatedOnMethod = entityType.GetMethod(nameof(IEntity.SetCreatedOn))
                ?? throw new Exception($"{nameof(IEntity.SetCreatedOn)} method not found on entity of type {entityType.Name}");

            var setCreatedByIdMethod = entityType.GetMethod(nameof(IEntity.SetCreatedById))
                ?? throw new Exception($"{nameof(IEntity.SetCreatedById)} method not found on entity of type {entityType.Name}");

            var updateMethod = entityType.GetMethod(nameof(IEntity.Update))
                ?? throw new Exception($"{nameof(IEntity.Update)} method not found on entity of type {entityType.Name}");

            var deletedProp = entityType.GetProperty(nameof(ISoftDeletedEntity.Deleted), BindingFlags.Instance | BindingFlags.Public);

            var methods = (setCreatedOnMethod, setCreatedByIdMethod, updateMethod, deletedProp);
            _entityInfo.TryAdd(entityType, methods);

            return methods;
        }
    }
}
