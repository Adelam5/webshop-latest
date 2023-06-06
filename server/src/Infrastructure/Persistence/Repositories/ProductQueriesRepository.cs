using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Products.Queries.GetById;
using Application.Products.Queries.List;
using Dapper;
using Npgsql;

namespace Infrastructure.Persistence.Repositories;
internal class ProductQueriesRepository : IProductQueriesRepository
{
    private readonly DbConnectionFactory connectionFactory;
    private readonly ICacheService cacheService;

    public ProductQueriesRepository(DbConnectionFactory connectionFactory, ICacheService cacheService)
    {
        this.connectionFactory = connectionFactory;
        this.cacheService = cacheService;
    }

    public async Task<List<ListProductsResponse>> GetAll(CancellationToken cancellationToken = default)
    {
        return await cacheService.Get("products",
            async () =>
            {
                var sqlQuery = @"SELECT Id, Name, Description, Price, Photo_Url
                             FROM products";

                await using NpgsqlConnection connection = connectionFactory.CreateConnection();

                var products = await connection
                    .QueryAsync<ListProductsResponse>(sqlQuery);

                return products.ToList();
            }, cancellationToken) ?? new List<ListProductsResponse>();
    }

    public async Task<GetProductByIdResponse?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await cacheService.Get($"product-{id}",
            async () =>
            {
                var sqlQuery = @"SELECT Id, 
                                Name, 
                                Description, 
                                Price, 
                                Photo_Name as PhotoName, 
                                Photo_Url as PhotoUrl
                                FROM products where Id = @id";

                await using NpgsqlConnection connection = connectionFactory.CreateConnection();

                var result = await connection.QueryAsync<GetProductByIdResponse>(
                   sqlQuery,
                   new
                   {
                       Id = id,
                   });

                return result.FirstOrDefault();
            }, cancellationToken);
    }
}
