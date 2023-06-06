using Domain.Constants;
using Domain.Core.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        ConfigureOrdersTable(builder);
        ConfigureOrderItemTable(builder);
        ConfigureOrderCustomerTable(builder);
    }

    private void ConfigureOrderCustomerTable(EntityTypeBuilder<Order> builder)
    {
        builder.OwnsOne(o => o.Customer, cb =>
        {
            cb.ToTable(TableNames.OrderCustomer);

            cb.WithOwner().HasForeignKey("order_id");

            cb.Property(c => c.Id)
                .HasColumnName("order_customer_id")
                .ValueGeneratedNever();

            cb.OwnsOne(c => c.DeliveryAddress);

            cb.Property(c => c.FirstName)
                .HasMaxLength(CommonConstants.StringMaxLength);

            cb.Property(c => c.LastName)
                .HasMaxLength(CommonConstants.StringMaxLength);
        });

    }

    private void ConfigureOrderItemTable(EntityTypeBuilder<Order> builder)
    {
        builder.OwnsMany(o => o.Items, oib =>
        {
            oib.ToTable(TableNames.OrderItems);

            oib.WithOwner().HasForeignKey("order_id");

            oib.Property(i => i.Id)
                .HasColumnName("order_item_id")
                .ValueGeneratedNever();

            oib.Property(i => i.Name)
                .HasMaxLength(CommonConstants.StringMaxLength);

            oib.Property(i => i.Price)
                .HasColumnType(CommonConstants.DecimalColumnType);
        });

        builder.Metadata.FindNavigation(nameof(Order.Items))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureOrdersTable(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(TableNames.Orders);

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .ValueGeneratedNever();

        builder.Property(o => o.Subtotal)
            .HasColumnType(CommonConstants.DecimalColumnType);

        builder.Property(o => o.PaymentStatus)
            .HasConversion<string>();

        builder.OwnsOne(o => o.DeliveryMethod, odm =>
        {

            odm.Property(dm => dm.Price)
                .HasColumnType(CommonConstants.DecimalColumnType);

            odm.Property(dm => dm.Name)
                .HasMaxLength(CommonConstants.StringMaxLength);
        });
    }
}
