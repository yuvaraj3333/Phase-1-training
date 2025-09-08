using Final_Project.AppDbContext;
using Final_Project.Models;
using Final_Project.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Repo.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly PetDbContext _context;

        public OrderRepository(PetDbContext context)
        {
            _context = context;
        }

        // Get all orders with their items and pets
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Pet)
                .ToListAsync();
        }

        // Get order by ID with items and pets
        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Pet)
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        // Add new order with items
        public async Task<Order> AddAsync(Order order, List<OrderItem> items)
        {
            order.OrderDate = DateTime.UtcNow;
            order.Status = "Pending";
            order.TotalAmount = items.Sum(i => i.Price * i.Quantity);

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            // Assign OrderId to items
            foreach (var item in items)
            {
                item.OrderId = order.OrderId;
            }

            await _context.OrderItems.AddRangeAsync(items);
            await _context.SaveChangesAsync();

            order.OrderItems = items;
            return order;
        }

        // Update status of an existing order
        public async Task<Order?> UpdateStatusAsync(int id, string status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return null;

            order.Status = status;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }

        // Delete an order
        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null) return false;

            if (order.OrderItems != null && order.OrderItems.Any())
                _context.OrderItems.RemoveRange(order.OrderItems);

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
