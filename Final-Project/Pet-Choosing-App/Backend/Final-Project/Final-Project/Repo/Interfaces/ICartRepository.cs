using Final_Project.Models;
using System.Threading.Tasks;

namespace Final_Project.Repo.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByUserIdAsync(int userId);
        Task<CartItem> AddItemAsync(int userId, CartItem item);
        Task<bool> RemoveItemAsync(int cartItemId);
        Task<CartItem> UpdateItemAsync(CartItem item);
    }
}
