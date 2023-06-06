using Application.Common.Interfaces.Repositories;
using Application.Orders.Queries.GetById;
using Application.Orders.Queries.GetCurrentUserOrders;
using Dapper;
using Npgsql;

namespace Infrastructure.Persistence.Repositories;
internal class OrderQueriesRepository : IOrderQueriesRepository
{
    private readonly DbConnectionFactory connectionFactory;

    public OrderQueriesRepository(DbConnectionFactory connectionFactory)
    {
        this.connectionFactory = connectionFactory;
    }

    public async Task<GetByIdOrderResponse?> GetById(Guid id)
    {
        var sqlQuery = @"SELECT o.Id, 
                    oc.First_Name || E' ' || oc.Last_Name AS Customer,
                    o.Payment_Status AS PaymentStatus,
                    o.Subtotal + o.Delivery_Method_Price AS Total,
					o.Created_On_Utc AS CreatedOnUtc,
					o.Modified_On_Utc AS ModifiedOnUtc,
                    oc.Delivery_Address_Street AS Street,
                    oc.Delivery_Address_City AS City,
                    oc.Delivery_Address_State AS State,
                    oc.Delivery_Address_Zipcode AS Zipcode,
                    oi.Order_Item_Id AS ProductId,
                    oi.Name,
                    oi.Price,
                    oi.Quantity
                    FROM orders AS o
					INNER JOIN order_customer AS oc
					ON oc.Order_Id = o.Id
                    INNER JOIN order_items AS oi
                    ON oi.Order_Id = o.Id
                    WHERE o.Id = @id";

        await using NpgsqlConnection connection = connectionFactory.CreateConnection();

        var ordersDictionary = new Dictionary<Guid, GetByIdOrderResponse>();
        var order = await connection
            .QueryAsync<GetByIdOrderResponse, CustomerAddress, Item, GetByIdOrderResponse>(sqlQuery,
                        (orderDto, address, item) =>
                        {
                            if (!ordersDictionary.TryGetValue(orderDto.Id, out GetByIdOrderResponse orderEntry))
                            {
                                orderEntry = orderDto;
                                orderEntry.Items = orderEntry.Items ?? new List<Item>();
                                orderEntry.Address = address;
                                ordersDictionary.Add(orderEntry.Id, orderEntry);
                            }
                            orderEntry.Items.Add(item);
                            return orderDto;
                        },
                        new
                        {
                            Id = id,
                        },
                        splitOn: "Street, ProductId");

        return order.FirstOrDefault();
    }

    public Task<List<GetByIdOrderResponse>> GetByCustomer(Guid customerId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<GetCurrentUserOrdersResponse>> GetCurrentUserOrders(string userId)
    {
        var sqlQuery = @"SELECT o.Id,
                    o.Payment_Status AS PaymentStatus,
                    o.Subtotal + o.Delivery_Method_Price AS Total,
					o.Created_On_Utc AS CreatedOnUtc,
					o.Modified_On_Utc AS ModifiedOnUtc
                    FROM orders AS o
                    WHERE o.User_Id = @userId";

        await using NpgsqlConnection connection = connectionFactory.CreateConnection();

        var orders = await connection
            .QueryAsync<GetCurrentUserOrdersResponse>(sqlQuery, new
            {
                UserId = userId
            });

        return orders.ToList();
    }
}
