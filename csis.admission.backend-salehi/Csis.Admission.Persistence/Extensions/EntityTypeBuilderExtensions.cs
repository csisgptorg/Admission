using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Extensions;

/// <summary>
/// Entity type builder extension methods
/// </summary>
internal static class EntityTypeBuilderExtensions
{
    /// <summary>
    /// Configures the schema of table that the entity type maps to when targeting a relational database.
    /// Table name is plurized name of entity
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="builder">The schema of the table</param>
    /// <param name="schema"></param>
    /// <returns></returns>
    internal static EntityTypeBuilder<TEntity> HasSchema<TEntity>(this EntityTypeBuilder<TEntity> builder, string schema)
        where TEntity : class {
        return builder.ToTable(builder.Metadata.GetTableName().Pluralize(), schema);
    }
}
