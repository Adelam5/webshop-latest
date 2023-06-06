using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(DataContext).Assembly,
            t => t.FullName != null && t.FullName.StartsWith("Infrastructure.Persistence.Configurations"));
    }
}
