using Final_Project.AppDbContext;
using Final_Project.DTOs.Pets;
using Final_Project.DTOs.Stores;
using Final_Project.Models;
using Final_Project.Repo.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;
        private readonly PetDbContext _context;

        public StoresController(IStoreRepository storeRepository, PetDbContext context)
        {
            _storeRepository = storeRepository;
            _context = context;
        }

        // GET: api/stores
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<StoreResponseDto>>> GetStores()
        {
            var stores = await _storeRepository.GetAllAsync();

            var response = stores.Select(s => new StoreResponseDto
            {
                StoreId = s.StoreId,
                StoreName = s.StoreName,
                Location = s.Location,
                OwnerId = s.OwnerId, // ✅ no null handling needed
                Pets = s.Pets?.Select(p => new PetResponseDto
                {
                    PetId = p.PetId,
                    Name = p.Name,
                    Breed = p.Breed,
                    Gender = p.Gender,
                    Price = p.Price
                }).ToList() ?? new List<PetResponseDto>()
            }).ToList();

            return Ok(response);
        }

        // GET: api/stores/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<StoreResponseDto>> GetStore(int id)
        {
            var store = await _storeRepository.GetByIdAsync(id);
            if (store == null) return NotFound();

            var response = new StoreResponseDto
            {
                StoreId = store.StoreId,
                StoreName = store.StoreName,
                Location = store.Location,
                OwnerId = store.OwnerId,
                Pets = store.Pets?.Select(p => new PetResponseDto
                {
                    PetId = p.PetId,
                    Name = p.Name,
                    Breed = p.Breed,
                    Gender = p.Gender,
                    Price = p.Price
                }).ToList() ?? new List<PetResponseDto>()
            };

            return Ok(response);
        }

        // POST: api/stores
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<StoreResponseDto>> CreateStore(StoreCreateDto dto)
        {
            var store = new Store
            {
                StoreName = dto.StoreName,
                Location = dto.Location,
                OwnerId = (int)dto.OwnerId
            };

            var created = await _storeRepository.AddAsync(store);

            var response = new StoreResponseDto
            {
                StoreId = created.StoreId,
                StoreName = created.StoreName,
                Location = created.Location,
                OwnerId = created.OwnerId,
                Pets = new List<PetResponseDto>()
            };

            return CreatedAtAction(nameof(GetStore), new { id = created.StoreId }, response);
        }

        // PUT: api/stores/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<StoreResponseDto>> UpdateStore(int id, StoreUpdateDto dto)
        {
            if (id != dto.StoreId) return BadRequest();

            var store = new Store
            {
                StoreId = dto.StoreId,
                StoreName = dto.StoreName,
                Location = dto.Location,
                OwnerId = (int)dto.OwnerId
            };

            var updated = await _storeRepository.UpdateAsync(store);

            var response = new StoreResponseDto
            {
                StoreId = updated.StoreId,
                StoreName = updated.StoreName,
                Location = updated.Location,
                OwnerId = updated.OwnerId,
                Pets = updated.Pets?.Select(p => new PetResponseDto
                {
                    PetId = p.PetId,
                    Name = p.Name,
                    Breed = p.Breed,
                    Gender = p.Gender,
                    Price = p.Price
                }).ToList() ?? new List<PetResponseDto>()
            };

            return Ok(response);
        }

        // DELETE: api/stores/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var store = await _context.Stores
                .Include(s => s.Pets)
                .Include(s => s.Orders)
                    .ThenInclude(o => o.OrderItems)
                .FirstOrDefaultAsync(s => s.StoreId == id);

            if (store == null) return NotFound();

            // Delete order items
            foreach (var order in store.Orders ?? Enumerable.Empty<Order>())
            {
                if (order.OrderItems != null && order.OrderItems.Any())
                {
                    _context.OrderItems.RemoveRange(order.OrderItems);
                }
            }

            // Delete orders
            if (store.Orders != null && store.Orders.Any())
            {
                _context.Orders.RemoveRange(store.Orders);
            }

            // Delete pets
            if (store.Pets != null && store.Pets.Any())
            {
                _context.Pets.RemoveRange(store.Pets);
            }

            // Delete store
            _context.Stores.Remove(store);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
