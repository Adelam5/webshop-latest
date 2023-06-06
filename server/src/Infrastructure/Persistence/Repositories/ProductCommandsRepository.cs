using Application.Common.Interfaces.Repositories;
using Domain.Core.Products;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
internal sealed class ProductCommandsRepository : IProductCommandsRepository
{
    private readonly DataContext dataContext;

    public ProductCommandsRepository(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }
    public async Task<Product?> GetById(
        Guid id, CancellationToken cancellationToken = default) =>
        await dataContext
            .Set<Product>()
            .FirstOrDefaultAsync(product => product.Id == id, cancellationToken);

    public void Add(Product product) =>
        dataContext.Set<Product>().Add(product);

    public void Update(Product product) =>
        dataContext.Set<Product>().Update(product);

    public void Remove(Product product) =>
        dataContext.Set<Product>().Remove(product);
}
