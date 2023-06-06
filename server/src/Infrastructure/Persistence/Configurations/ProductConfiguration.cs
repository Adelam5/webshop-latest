using Domain.Constants;
using Domain.Core.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        ConfigureProductsTable(builder);
    }

    private void ConfigureProductsTable(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(TableNames.Products);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever();

        builder.Property(m => m.Name)
            .HasMaxLength(CommonConstants.StringMaxLength);

        builder.Property(m => m.Description)
            .HasMaxLength(CommonConstants.TextMaxLength);

        builder.Property(m => m.Price)
            .HasColumnType(CommonConstants.DecimalColumnType);

        builder.OwnsOne(p => p.Photo, photoBuilder =>
        {
            photoBuilder.Property(photo => photo.Name).HasDefaultValue(PhotoConstants.DefaultPhotoName);
            photoBuilder.Property(photo => photo.Url).HasDefaultValue(PhotoConstants.DefaultPhotoUrl);
        });
    }
}
