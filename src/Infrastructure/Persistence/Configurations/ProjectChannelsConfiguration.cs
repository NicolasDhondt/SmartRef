using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SmartRef.Domain.Entities;

namespace SmartRef.Infrastructure.Persistence.Configurations;

public class ProjectChannelsConfiguration : IEntityTypeConfiguration<ProjectChannels>
{
    public void Configure(EntityTypeBuilder<ProjectChannels> builder)
    {
        builder.HasKey(pc => new { pc.ProjectId, pc.ChannelId });

        builder.HasOne(pc => pc.Project)
            .WithMany(p => p.ProjectChannels)
            .HasForeignKey(pc => pc.ProjectId);

        builder.HasOne(pc => pc.Channel)
            .WithMany(c => c.ProjectChannels)
            .HasForeignKey(pc => pc.ChannelId);
    }
}