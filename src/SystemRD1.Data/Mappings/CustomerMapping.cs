using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SystemRD1.Domain.Entities;

namespace SystemRD1.Data.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(50);
            
            builder.OwnsOne(c => c.Address, address =>
            {
                address.Property(a => a.Street)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            builder.OwnsOne(c => c.Address, address =>
            {
                address.Property(a => a.Number)
                    .IsRequired()
                    .HasMaxLength(7);
            });

            builder.OwnsOne(c => c.Address, address =>
            {
                address.Property(a => a.Neighborhood)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            builder.OwnsOne(c => c.Address, address => 
            {
                address.Property(a => a.ZipCode)
                    .IsRequired()
                    .HasMaxLength(8);
            });

            builder.OwnsOne(c => c.Address, address => 
            {
                address.Property(a => a.City)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            builder.OwnsOne(c => c.Address, address =>
            {
                address.Property(a => a.State)
                    .IsRequired()
                    .HasMaxLength(2);
            });
        }
    }
}
