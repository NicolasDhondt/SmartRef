using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartRef.Domain.Entities;

namespace SmartRef.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasOne(c => c.Vertical)
           .WithMany(v => v.Customers)
           .HasForeignKey(c => c.VerticalId);

        builder.HasOne(c => c.Sector)
           .WithMany(v => v.Customers)
           .HasForeignKey(c => c.SectorId);
    }
}
