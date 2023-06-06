using Domain.Core.DeliveryMethods;

namespace Application.Common.Interfaces.Repositories;
public interface IDeliveryMethodRepository
{
    Task<List<DeliveryMethod>> List(CancellationToken cancellationToken = default);
    Task<DeliveryMethod?> GetById(Guid id, CancellationToken cancellationToken = default);
}
