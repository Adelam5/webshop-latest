using Application.Common.Interfaces.Repositories;
using Domain.Core.DeliveryMethods;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
internal class DeliveryMethodRepository : IDeliveryMethodRepository
{
    private readonly DataContext context;

    public DeliveryMethodRepository(DataContext context)
    {
        this.context = context;
    }

    public async Task<List<DeliveryMethod>> List(CancellationToken cancellationToken = default) =>
    await context
        .Set<DeliveryMethod>()
        .ToListAsync(cancellationToken);

    public async Task<DeliveryMethod?> GetById(Guid id, CancellationToken cancellationToken = default) =>
        await context
            .Set<DeliveryMethod>()
            .FirstOrDefaultAsync(dm => dm.Id == id, cancellationToken);
}
