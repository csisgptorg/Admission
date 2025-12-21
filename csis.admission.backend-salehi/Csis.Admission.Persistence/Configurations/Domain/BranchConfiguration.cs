namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class BranchConfiguration : BaseEntityConfiguration<Branch, short>
{
    public override void Configure(EntityTypeBuilder<Branch> builder) {
        base.Configure(builder);
        builder.ToTable("Branches", "Base");

        builder.Ignore(b => b.CreatedByDelegatedId);
        builder.Ignore(b => b.CreatedById);
        builder.Ignore(b => b.CreatedOn);   
        builder.Ignore(b => b.Description);
        builder.Ignore(b => b.LastUpdatedByDelegatedId);
        builder.Ignore(b => b.LastUpdatedById);
        builder.Ignore(b => b.UpdatedOn);
    }
}
