using Final_Project.DTO;
using Final_Project.DTOs.Pets;
using Final_Project.Models;
using Final_Project.Repo.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IPetRepository _petRepo;

        public PetsController(IPetRepository petRepo)
        {
            _petRepo = petRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetResponseDto>>> GetAll()
        {
            var pets = await _petRepo.GetAllAsync();
            var dtos = pets.Select(p => new PetResponseDto
            {
                PetId = p.PetId,
                Name = p.Name,
                Breed = p.Breed,
                Gender = p.Gender,
                Price = p.Price,
                StoreId = p.StoreId
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PetResponseDto>> GetById(int id)
        {
            var pet = await _petRepo.GetByIdAsync(id);
            if (pet == null) return NotFound();

            var dto = new PetResponseDto
            {
                PetId = pet.PetId,
                Name = pet.Name,
                Breed = pet.Breed,
                Gender = pet.Gender,
                Price = pet.Price,
                StoreId = pet.StoreId
            };
            return Ok(dto);
        }

        [HttpPost]
        [Authorize(Roles = "Store")]
        public async Task<ActionResult<PetResponseDto>> Create([FromBody] PetCreateDto dto)
        {
            var pet = new Pet
            {
                Name = dto.Name,
                Breed = dto.Breed,
                Gender = dto.Gender,
                Price = dto.Price,
                StoreId = dto.StoreId
            };

            var createdPet = await _petRepo.AddAsync(pet);

            var response = new PetResponseDto
            {
                PetId = createdPet.PetId,
                Name = createdPet.Name,
                Breed = createdPet.Breed,
                Gender = createdPet.Gender,
                Price = createdPet.Price,
                StoreId = createdPet.StoreId
            };

            return CreatedAtAction(nameof(GetById), new { id = response.PetId }, response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Store")]
        public async Task<ActionResult<PetResponseDto>> Update(int id, [FromBody] PetCreateDto dto)
        {
            var pet = await _petRepo.GetByIdAsync(id);
            if (pet == null) return NotFound();

            pet.Name = dto.Name;
            pet.Breed = dto.Breed;
            pet.Gender = dto.Gender;
            pet.Price = dto.Price;
            pet.StoreId = dto.StoreId;

            var updatedPet = await _petRepo.UpdateAsync(pet);

            return Ok(new PetResponseDto
            {
                PetId = updatedPet.PetId,
                Name = updatedPet.Name,
                Breed = updatedPet.Breed,
                Gender = updatedPet.Gender,
                Price = updatedPet.Price,
                StoreId = updatedPet.StoreId
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Store")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _petRepo.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
