using EfOrders.Api.Core.DomainServices;
using EfOrders.Api.Infrastructure.Data;
using EfOrders.Api.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EfOrders.Api.Infrastructure
{
    public class EfOrderRepository(OrdersDbContext context) : IOrderRepository
    {
        public async Task<List<Order>> GetAllAsync()
        {
            return await context
                .Orders
                .ToListAsync();
        }


        public async Task<Order?> FindAsync(int id)
        {
            return await context
                .Orders
                .SingleOrDefaultAsync(
                    order => order.Id == id);
        }

        public async Task<List<Order>> GetOrdersAboveTotal1000()
        {
            /* Versjon 1 - filtrering i databasen*/
            return await context
                .Orders
                .Where(o=>o.TotalAmount>1000)
                .ToListAsync();

            var orders1 = context.Orders;
            var filteredOrders1 = orders1.Where(o => o.TotalAmount > 1000);
            var ordersList1 = await filteredOrders1.ToListAsync();

            /* Versjon 1 - filtrering i C#*/
            return await context
                .Orders
                .AsAsyncEnumerable()
                .Where(o=>o.TotalAmount>1000)
                .ToListAsync();

            var orders2 = context.Orders.AsAsyncEnumerable(); // her skjer spørringen mot db
            var filteredOrders2 = orders2.Where(o => o.TotalAmount > 1000); // skjer i minnet
            return await filteredOrders2.ToListAsync();
        }

        public async Task Create(Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
        }
    }
}
