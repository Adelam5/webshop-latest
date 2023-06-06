using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infrastructure.Persistence;
public class DbConnectionFactory
{
    private readonly IConfiguration configuration;

    public DbConnectionFactory(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(
            ConnectionHelper.GetConnectionString(configuration));
    }
}
