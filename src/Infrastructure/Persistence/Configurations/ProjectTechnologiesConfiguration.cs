using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SmartRef.Domain.Entities;

namespace SmartRef.Infrastructure.Persistence.Configurations;

public class ProjectTechnologiesConfiguration : IEntityTypeConfiguration<ProjectTechnologies>
{
    public void Configure(EntityTypeBuilder<ProjectTechnologies> builder)
    {
        builder.HasKey(pt => new { pt.ProjectId, pt.TechnologyId });

        builder.HasOne(pt => pt.Project)
            .WithMany(p => p.ProjectTechnologies)
            .HasForeignKey(pt => pt.ProjectId);

        builder.HasOne(pt => pt.Technology)
            .WithMany(t => t.ProjectTechnologies)
            .HasForeignKey(pt => pt.TechnologyId);
    }
}
