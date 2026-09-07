using EfOrders.Api.Core.DomainServices;
using EfOrders.Api.Data;
using EfOrders.Api.Models;
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

        public async Task Create(Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
        }
    }
}
