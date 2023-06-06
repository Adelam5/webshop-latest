using Domain.Core.Customers;

namespace Application.Common.Interfaces.Repositories.Commands;
public interface ICustomerCommandsRepository
{
    Task<Customer?> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<Customer?> GetByUserId(string id, CancellationToken cancellationToken = default);
    void Add(Customer customer);
    void Update(Customer customer);
    void Remove(Customer customer);
}
