using Domain.Core.DeliveryMethods;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        ConfigureDeliveryMethodTable(builder);
        SeedDeliveryMethodTable(builder);
    }

    private void ConfigureDeliveryMethodTable(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.ToTable("DeliveryMethods");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.Name)
            .HasMaxLength(50);

        builder.Property(dm => dm.Price)
                 .HasColumnType("decimal(10,2)");

        builder.Property(dm => dm.Name)
            .HasMaxLength(50);

        builder.Property(dm => dm.Description)
            .HasMaxLength(250);

        builder.Property(dm => dm.DeliveryTime)
            .HasMaxLength(50);
    }

    private void SeedDeliveryMethodTable(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.HasData(
            new DeliveryMethod(Guid.Parse("511a434a-52d2-4a28-b5d4-d1be04fdc3f5"),
            "Free Delivery", "Free Delivery Description", "7-10 days", 0m),
            new DeliveryMethod(Guid.Parse("801f64d0-831b-4b60-b562-f89bc52f936e"),
            "Fast Delivery", "Fast Delivery Description", "1-3 days", 5m));
    }
}
