/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Persistence.Configurations;

internal sealed class UploadedFileConfiguration : BaseEntityConfiguration<UploadedFile>
{
    public override void Configure(EntityTypeBuilder<UploadedFile> builder) {
        base.Configure(builder);

        builder.Property(x => x.Description).HasMaxLength(100);
        builder.Property(x => x.FileIdentifier).IsRequired();
        builder.HasIndex(x => x.FileIdentifier).IsUnique();
    }
}
