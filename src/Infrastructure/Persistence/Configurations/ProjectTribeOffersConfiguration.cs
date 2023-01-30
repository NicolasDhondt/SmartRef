using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SmartRef.Domain.Entities;

namespace SmartRef.Infrastructure.Persistence.Configurations;

public class ProjectTribeOffersConfiguration : IEntityTypeConfiguration<ProjectTribeOffers>
{
    public void Configure(EntityTypeBuilder<ProjectTribeOffers> builder)
    {
        builder.HasKey(pto=> new { pto.ProjectId, pto.TribeOfferId });

        builder.HasOne(pto => pto.Project)
            .WithMany(p => p.ProjectTribeOffers)
            .HasForeignKey(pto => pto.ProjectId);

        builder.HasOne(pto => pto.TribeOffer)
            .WithMany(to => to.ProjectTribeOffers)
            .HasForeignKey(pto => pto.TribeOfferId);
    }
}
