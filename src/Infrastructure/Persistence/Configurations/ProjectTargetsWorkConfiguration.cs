using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SmartRef.Domain.Entities;

namespace SmartRef.Infrastructure.Persistence.Configurations;

public class ProjectTargetsWorkConfiguration : IEntityTypeConfiguration<ProjectTargetsWork>
{
    public void Configure(EntityTypeBuilder<ProjectTargetsWork> builder)
    {
        builder.HasKey(ptw => new { ptw.ProjectId, ptw.TargetWorkId });

        builder.HasOne(ptw => ptw.Project)
            .WithMany(p => p.ProjectTargetsWork)
            .HasForeignKey(pwo => pwo.ProjectId);

        builder.HasOne(ptw => ptw.TargetWork)
            .WithMany(tw => tw.ProjectTargetsWork)
            .HasForeignKey(ptw => ptw.TargetWorkId);
    }
}
