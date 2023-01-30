using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SmartRef.Domain.Entities;

namespace SmartRef.Infrastructure.Persistence.Configurations;

public class ProjectContactsConfiguration : IEntityTypeConfiguration<ProjectContacts>
{
    public void Configure(EntityTypeBuilder<ProjectContacts> builder)
    {
        builder.HasKey(pc => new { pc.ProjectId, pc.ContactId, pc.ContactType });

        builder.HasOne(pc => pc.Project)
            .WithMany(p => p.ProjectContacts)
            .HasForeignKey(pc => pc.ProjectId);

        builder.HasOne(pc => pc.Contact)
            .WithMany(c => c.ProjectContacts)
            .HasForeignKey(pc => pc.ContactId);
    }
}