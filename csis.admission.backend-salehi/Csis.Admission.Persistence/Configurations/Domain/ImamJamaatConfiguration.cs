using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Csis.Admission.Domain.Entities;
using Csis.Admission.Domain.Enums;
using System.Text.Json;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class ImamJamaatConfiguration : SoftDeletedBaseEntityConfiguration<ImamJamaat>
{
    public override void Configure(EntityTypeBuilder<ImamJamaat> builder)
    {
        base.Configure(builder);

        builder.ToTable("ImamJamaat", "dbo");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.MonthlyNonCashAssistance)
            .HasConversion(
                enumList => enumList == null || enumList.Count == 0
                    ? null
                    : JsonSerializer.Serialize(
                        enumList.Select(e => (int)e).ToList(),
                        (JsonSerializerOptions)null
                    ),
                json => DeserializeEnumList<NonCashAssistanceFromMosque>(json)
            )
            .HasColumnType("nvarchar(500)")
            .IsRequired(false);

        builder.HasOne(x => x.Mosque)
            .WithMany(m => m.Imams)
            .HasForeignKey(x => x.MosqueId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    /// <summary>
    /// Deserialize JSON to enum list, handling both integer arrays and string (enum name) arrays
    /// </summary>
    private static List<TEnum> DeserializeEnumList<TEnum>(string json) where TEnum : struct, Enum
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<TEnum>();
        }

        try
        {
            // Try to deserialize as integer array first (expected format)
            var intList = JsonSerializer.Deserialize<List<int>>(json, (JsonSerializerOptions)null);
            if (intList != null)
            {
                return intList.Select(i => (TEnum)Enum.ToObject(typeof(TEnum), i)).ToList();
            }
        }
        catch (JsonException)
        {
            // If integer deserialization fails, try string array (enum names)
            try
            {
                var stringList = JsonSerializer.Deserialize<List<string>>(json, (JsonSerializerOptions)null);
                if (stringList != null)
                {
                    return stringList
                        .Where(s => Enum.TryParse<TEnum>(s, true, out _))
                        .Select(s => Enum.Parse<TEnum>(s, true))
                        .ToList();
                }
            }
            catch
            {
                // If both fail, return empty list
            }
        }

        return new List<TEnum>();
    }
}
