using Application.Common.Interfaces.Repositories.Commands;
using Domain.Core.Customers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

internal class CustomerCommandsRepository : ICustomerCommandsRepository
{
    private readonly DataContext dataContext;

    public CustomerCommandsRepository(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public async Task<Customer?> GetById(Guid id, CancellationToken cancellationToken = default) =>
        await dataContext
            .Set<Customer>()
            .FirstOrDefaultAsync(customer => customer.Id == id, cancellationToken);

    public async Task<Customer?> GetByUserId(string id, CancellationToken cancellationToken = default) =>
        await dataContext
            .Set<Customer>()
            .FirstOrDefaultAsync(customer => customer.UserId == id, cancellationToken);

    public void Add(Customer customer) =>
        dataContext.Set<Customer>().Add(customer);

    public void Update(Customer customer) =>
        dataContext.Set<Customer>().Update(customer);

    public void Remove(Customer customer) =>
        dataContext.Set<Customer>().Remove(customer);
}
