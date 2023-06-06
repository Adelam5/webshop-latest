using Application.Common.Interfaces.Repositories.Queries;
using Application.Customers.Queries.GetById;
using Application.Customers.Queries.List;
using Dapper;
using Npgsql;

namespace Infrastructure.Persistence.Repositories;
public class CustomerQueriesRepository : ICustomerQueriesRepository
{
    private readonly DbConnectionFactory connectionFactory;

    public CustomerQueriesRepository(DbConnectionFactory connectionFactory)
    {
        this.connectionFactory = connectionFactory;

    }

    public async Task<List<ListCustomerDto>> GetAll(CancellationToken cancellationToken = default)
    {
        var sqlQuery = @"SELECT
                    c.Id,
                    u.First_Name as FirstName, 
                    u.Last_Name as LastName, 
                    u.Email, 
                    c.Address_Street as Street, 
                    c.Address_City as City,  
                    c.Address_State as State, 
                    c.Address_Zipcode as Zipcode 
                    FROM customers AS c
                    INNER JOIN ""AspNetUsers"" AS u 
                    ON u.Id = c.user_id";

        await using NpgsqlConnection connection = connectionFactory.CreateConnection();

        var products = await connection
            .QueryAsync<ListCustomerDto>(sqlQuery);

        return products.ToList();

    }

    public async Task<GetCustomerByIdResponse?> GetById(Guid id, CancellationToken cancellationToken)
    {

        var sqlQuery = @"SELECT c.Id, 
                    u.First_Name as FirstName, 
                    u.Last_Name as LastName, 
                    u.Email, 
                    c.Address_Street as Street, 
                    c.Address_City as City,  
                    c.Address_State as State, 
                    c.Address_Zipcode as Zipcode 
                    FROM customers AS c
                    INNER JOIN ""AspNetUsers"" AS u 
                    ON u.Id = c.User_Id
                    WHERE c.Id = @id";

        await using NpgsqlConnection connection = connectionFactory.CreateConnection();

        var result = await connection
            .QueryAsync<GetCustomerByIdResponse, CustomerAddress, GetCustomerByIdResponse>(
                sqlQuery,
                (customerDto, address) =>
                {
                    customerDto.Address = address;
                    return customerDto;
                },
                new
                {
                    Id = id,
                },
                splitOn: "Street");

        return result.FirstOrDefault();

    }

    public async Task<GetCustomerByIdResponse?> GetByUserId(string id, CancellationToken cancellationToken = default)
    {
        var sqlQuery = @"SELECT c.Id, 
                            u.First_Name as FirstName, 
                            u.Last_Name as LastName,  
                            u.Email, 
                            c.Address_Street as Street, 
                            c.Address_City as City,  
                            c.Address_State as State, 
                            c.Address_Zipcode as Zipcode 
                            FROM customers AS c
                            INNER JOIN ""AspNetUsers"" AS u 
                            ON u.Id = c.User_Id
                            WHERE u.Id = @id";

        await using NpgsqlConnection connection = connectionFactory.CreateConnection();

        var result = await connection
            .QueryAsync<GetCustomerByIdResponse, CustomerAddress, GetCustomerByIdResponse>(
                sqlQuery,
                (customerDto, address) =>
                {
                    customerDto.Address = address;
                    return customerDto;
                },
                new
                {
                    Id = id,
                },
                splitOn: "Street");

        return result.FirstOrDefault();

    }
}
