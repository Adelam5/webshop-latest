using Domain.Constants;
using Domain.Core.Common.ValueObjects;
using Domain.Core.Products.Events;
using Domain.Exceptions.Photo;
using Domain.Exceptions.ProductExceptions;
using Domain.Primitives;
using System.Text.RegularExpressions;

namespace Domain.Core.Products;
public sealed class Product : AggregateRoot, IAuditableEntity, ICacheableEntity
{
#pragma warning disable CS8618
    private Product() : base() { }
#pragma warning restore CS8618

    private Product(Guid id, string name, string description, decimal price) : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
        Photo = Photo.Create();
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public Photo Photo { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime ModifiedOnUtc { get; set; }
    public string? ModifiedBy { get; set; }

    public static Product Create(string name, string description, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > CommonConstants.StringMaxLength)
            throw new InvalidProductNameException();

        if (string.IsNullOrWhiteSpace(description) || description.Length > CommonConstants.TextMaxLength)
            throw new InvalidProductDescriptionException();

        if (price < PriceConstants.MinValue)
            throw new InvalidProductPriceException();

        Product product = new(
            Guid.NewGuid(),
            name,
            description,
            price);

        return product;
    }

    public void UpdateProduct(string name, string description, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > CommonConstants.StringMaxLength)
            throw new InvalidProductNameException();

        if (string.IsNullOrWhiteSpace(description) || description.Length > CommonConstants.TextMaxLength)
            throw new InvalidProductDescriptionException();

        if (price < 0)
            throw new InvalidProductPriceException();

        Name = name;
        Description = description;
        Price = price;

    }

    public void UpdatePhoto(string photoUrl, string name)
    {
        if (string.IsNullOrWhiteSpace(photoUrl))
        {
            throw new InvalidPhotoUrlException();
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidPhotoNameException();
        }

        if (!Regex.IsMatch(photoUrl, UrlPatterns.ImageUrlPattern, RegexOptions.IgnoreCase))
        {
            throw new InvalidPhotoUrlException();
        }

        Photo = Photo.Create(photoUrl, name);

    }

    public void MarkAsDeleted()
    {
        RaiseDomainEvent(new ProductDeletedDomainEvent(Guid.NewGuid(), Photo.Name));
    }

}
