/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Csis.Admission.Persistence.Configurations;
internal sealed class ReportProfileConfiguration : SoftDeletedBaseEntityConfiguration<ReportProfile>
{
    private static readonly JsonSerializerOptions _serializerOptions = new() {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        MaxDepth = 64,
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public override void Configure(EntityTypeBuilder<ReportProfile> builder) {
        base.Configure(builder);

        builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Structure)
            .HasConversion(
                v => JsonSerializer.Serialize(v, _serializerOptions),
                v => JsonSerializer.Deserialize<ReportProfileStructure>(v, _serializerOptions));
    }
}
