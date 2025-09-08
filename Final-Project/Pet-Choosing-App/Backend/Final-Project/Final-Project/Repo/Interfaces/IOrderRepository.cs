using Final_Project.Models;

namespace Final_Project.Repo.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(int id);
        Task<Order> AddAsync(Order order, List<OrderItem> items);
        Task<Order> UpdateStatusAsync(int id, string status);
        Task<bool> DeleteAsync(int id);
    }
}
