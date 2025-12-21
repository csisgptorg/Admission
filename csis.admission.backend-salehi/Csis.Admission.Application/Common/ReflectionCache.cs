/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Domain.Common;
using Csis.Utilities.Annotations;
using System.Reflection;

namespace Csis.Admission.Application.Common;

/// <summary>
/// In-memory storage for caching expensive reflection operation on application startup
/// </summary>
public static class ReflectionCache
{
    static ReflectionCache() {
        EntityTree = [];

        var iEntity = typeof(IEntity);
        var entityTypes = iEntity.Assembly
            .GetExportedTypes()
            .Where(t => iEntity.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract && t.IsPublic)
            .ToList();

        foreach ( var entityType in entityTypes ) {
            var relations = new List<EntityRelation>();
            var navigations = entityType.GetProperties()
                .Where(p => iEntity.IsAssignableFrom(p.PropertyType) && p.PropertyType.IsPublic);
            foreach ( var navigation in navigations ) {
                if ( navigation.PropertyType == entityType ) {
                    continue;
                }

                var ignoreForeignKeyAttribute = navigation.GetCustomAttribute<IgnoreForeignKeyAttribute>(false);
                if ( ignoreForeignKeyAttribute is not null ) {
                    continue;
                }

                var foreignKeyAttribute = navigation.GetCustomAttribute<ForeignKeyAttribute>(false);
                var foreignKeyPropertyName = foreignKeyAttribute is not null ? foreignKeyAttribute.ForeignKeyPropertyName : $"{navigation.Name}Id";
                var foreignKey = entityType.GetProperty(foreignKeyPropertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase)
                    ?? throw new Exception($"Expected foreign key for navigation '{navigation.Name}' with property name '{foreignKeyPropertyName}' not found on entity '{entityType.Name}'");
                relations.Add(new EntityRelation(navigation.PropertyType, foreignKey));
            }

            if ( relations.Count > 0 ) {
                EntityTree.Add(entityType, relations);
            }
        }

    }

    /// <summary>
    /// A map between entity to related parents
    /// </summary>
    public static Dictionary<Type, List<EntityRelation>> EntityTree { get; }
}
