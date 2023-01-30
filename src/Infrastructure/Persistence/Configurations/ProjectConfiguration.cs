using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SmartRef.Domain.Entities;

namespace SmartRef.Infrastructure.Persistence.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasOne(p => p.Customer)
           .WithMany(c => c.Projects)
           .HasForeignKey(p => p.CustomerId);
    }
}
