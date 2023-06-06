using Application.Common.Interfaces;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;
internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly DataContext context;

    public UnitOfWork(DataContext context)
    {
        this.context = context;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            throw new DatabaseUpdateException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new AppException(ex.Message);
        }

    }
}
