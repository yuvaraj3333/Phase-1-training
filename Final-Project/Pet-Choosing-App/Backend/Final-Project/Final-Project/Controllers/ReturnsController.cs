using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final_Project.DTOs.Returns;
using Final_Project.Models;
using Final_Project.Repo.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnsController : ControllerBase
    {
        private readonly IReturnRepository _returnRepo;

        public ReturnsController(IReturnRepository returnRepo)
        {
            _returnRepo = returnRepo;
        }

        [HttpPost]
        public async Task<ActionResult<ReturnResponseDto>> CreateReturn([FromBody] ReturnCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Return data is required.");

            var returnRequest = new Return
            {
                OrderId = dto.OrderId,
                OrderItemId = dto.OrderItemId,
                Reason = dto.Reason ?? string.Empty,
                ReturnDate = DateTime.UtcNow,
                Status = "Pending"
            };

            var createdReturn = await _returnRepo.AddAsync(returnRequest);
            if (createdReturn == null)
                return StatusCode(500, "Failed to create return.");

            var response = new ReturnResponseDto
            {
                ReturnId = createdReturn.ReturnId,
                OrderId = createdReturn.OrderId,
                OrderItemId = createdReturn.OrderItemId,
                Reason = createdReturn.Reason,
                ReturnDate = createdReturn.ReturnDate,
                Status = createdReturn.Status
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReturnResponseDto>>> GetReturns()
        {
            var returns = await _returnRepo.GetAllAsync();
            if (returns == null)
                return Ok(new List<ReturnResponseDto>());

            var response = returns.Select(r => new ReturnResponseDto
            {
                ReturnId = r.ReturnId,
                OrderId = r.OrderId,
                OrderItemId = r.OrderItemId,
                Reason = r.Reason,
                ReturnDate = r.ReturnDate,
                Status = r.Status
            }).ToList();

            return Ok(response);
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult<ReturnResponseDto>> UpdateStatus(int id, [FromBody] ReturnUpdateStatusDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Status))
                return BadRequest("Status is required.");

            var updatedReturn = await _returnRepo.UpdateStatusAsync(id, dto.Status.Trim());
            if (updatedReturn == null)
                return NotFound();

            var response = new ReturnResponseDto
            {
                ReturnId = updatedReturn.ReturnId,
                OrderId = updatedReturn.OrderId,
                OrderItemId = updatedReturn.OrderItemId,
                Reason = updatedReturn.Reason,
                ReturnDate = updatedReturn.ReturnDate,
                Status = updatedReturn.Status
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _returnRepo.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
