using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SmartRef.Domain.Entities;

namespace SmartRef.Infrastructure.Persistence.Configurations;

public class ProjectCountriesConfiguration : IEntityTypeConfiguration<ProjectCountries>
{
    public void Configure(EntityTypeBuilder<ProjectCountries> builder)
    {
        builder.HasKey(pc => new { pc.ProjectId, pc.CountryId });

        builder.HasOne(pc => pc.Project)
            .WithMany(p => p.ProjectCountries)
            .HasForeignKey(pc => pc.ProjectId);

        builder.HasOne(pc => pc.Country)
            .WithMany(c => c.ProjectCountries)
            .HasForeignKey(pc => pc.CountryId);
    }
}
