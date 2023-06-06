using Domain.Core.Products;

namespace Application.Common.Interfaces.Repositories;
public interface IProductCommandsRepository
{
    Task<Product?> GetById(Guid id, CancellationToken cancellationToken = default);
    void Add(Product product);
    void Update(Product product);
    void Remove(Product product);
}
