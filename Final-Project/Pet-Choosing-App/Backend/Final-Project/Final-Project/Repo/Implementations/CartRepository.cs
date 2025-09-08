using Final_Project.Models;
using Final_Project.Repo.Interfaces;
using Final_Project.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Repo.Implementations
{
    public class CartRepository : ICartRepository  // <--- must implement interface
    {
        private readonly PetDbContext _context;

        public CartRepository(PetDbContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetCartByUserIdAsync(int userId)
        {
            // Include CartItems to load the related items
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            // If cart does not exist, create an empty cart
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CartItems = new List<CartItem>()
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            return cart;
        }


        public async Task<CartItem> AddItemAsync(int userId, CartItem item)
        {
            // Get the user's cart
            var cart = await GetCartByUserIdAsync(userId);

            // Check if the item already exists
            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.PetId == item.PetId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
                _context.CartItems.Update(existingItem);
            }
            else
            {
                item.CartId = cart.CartId;  // Assign cart id
                _context.CartItems.Add(item);
            }

            await _context.SaveChangesAsync();
            return item;
        }


        public async Task<CartItem> UpdateItemAsync(CartItem item)
        {
            _context.CartItems.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> RemoveItemAsync(int cartItemId)
        {
            var item = await _context.CartItems.FindAsync(cartItemId);
            if (item == null) return false;

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
