using Final_Project.DTO;
using Final_Project.Models;
using Final_Project.Repo.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepo;

        public CartController(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            int userId = int.Parse(User.FindFirst("id").Value);
            var cart = await _cartRepo.GetCartByUserIdAsync(userId);

            return Ok(new
            {
                CartId = cart.CartId,
                UserId = cart.UserId,
                Items = cart.CartItems.Select(ci => new
                {
                    CartItemId = ci.CartItemId,
                    PetId = ci.PetId,
                    Quantity = ci.Quantity
                })
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] CartItemCreateDto dto)
        {
            int userId = int.Parse(User.FindFirst("id").Value);

            var cartItem = new CartItem
            {
                PetId = dto.PetId,
                Quantity = dto.Quantity
            };

            var addedItem = await _cartRepo.AddItemAsync(userId, cartItem);
            return Ok(addedItem);
        }

        [HttpPut("{cartItemId}")]
        public async Task<ActionResult> UpdateItem(int cartItemId, CartItemCreateDto dto)
        {
            var item = await _cartRepo.UpdateItemAsync(new CartItem
            {
                CartItemId = cartItemId,
                PetId = dto.PetId,
                Quantity = dto.Quantity
            });

            return Ok(item);
        }

        [HttpDelete("{cartItemId}")]
        public async Task<ActionResult> RemoveItem(int cartItemId)
        {
            var result = await _cartRepo.RemoveItemAsync(cartItemId);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
