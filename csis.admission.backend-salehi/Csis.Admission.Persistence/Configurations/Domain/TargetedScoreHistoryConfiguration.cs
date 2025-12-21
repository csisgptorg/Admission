namespace Csis.Admission.Persistence.Configurations.Domain;
internal sealed class TargetedScoreHistoryConfiguration : BaseEntityConfiguration<TargetedScoreHistory>
{
    public override void Configure(EntityTypeBuilder<TargetedScoreHistory> builder) {
        base.Configure(builder);

        builder.ToTable("TbHadafmandiEmtiazHistory");

        builder.Property(x => x.TargetedScoreJson).HasColumnName("JsonData");
        builder.Property(x => x.Date).HasColumnName("ProcessStartDate");
        builder.Property(x => x.Time).HasColumnName("ProcessStartTime");
        builder.Property(x => x.Version).HasColumnName("ProcessVersion");

        builder.Ignore("CreatedById");
        builder.Ignore("CreatedOn");
        builder.Ignore("DeletedById");
        builder.Ignore("DeletedOn");
        builder.Ignore("Description");
        builder.Ignore("LastUpdatedById");
        builder.Ignore("UpdatedOn");
        builder.Ignore("CreatedByDelegatedId");
        builder.Ignore("LastUpdatedByDelegatedId");
    }
}
