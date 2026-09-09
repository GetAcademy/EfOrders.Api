using EfOrders.Api.Infrastructure.Models;

namespace EfOrders.Api.Core.DomainServices
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync();

        Task<Order?> FindAsync(int id);

        Task<List<Order>> GetOrdersAboveTotal1000();
    }
}
