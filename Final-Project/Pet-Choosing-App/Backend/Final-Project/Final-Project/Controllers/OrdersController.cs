using Final_Project.DTOs.Orders;
using Final_Project.Models;
using Final_Project.Repo.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;

        public OrdersController(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        [HttpPost]
        public async Task<ActionResult<OrderResponseDto>> CreateOrder(OrderCreateDto dto)
        {
            if (dto.Items == null || !dto.Items.Any())
                return BadRequest("Order must contain at least one item.");

            var orderItems = dto.Items.Select(i => new OrderItem
            {
                PetId = i.PetId,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList();

            var order = new Order
            {
                UserId = dto.UserId,
                StoreId = dto.StoreId,
                OrderItems = orderItems
            };

            var createdOrder = await _orderRepo.AddAsync(order, orderItems);

            var response = new OrderResponseDto
            {
                OrderId = createdOrder.OrderId,
                UserId = createdOrder.UserId,
                StoreId = createdOrder.StoreId,
                OrderDate = createdOrder.OrderDate,
                Status = createdOrder.Status,
                TotalAmount = createdOrder.TotalAmount,
                Items = createdOrder.OrderItems?
                    .Select(i => new OrderItemResponseDto
                    {
                        OrderItemId = i.OrderItemId,
                        PetId = i.PetId,
                        Quantity = i.Quantity,
                        Price = i.Price
                    }).ToList() ?? new List<OrderItemResponseDto>()
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetOrders()
        {
            var orders = await _orderRepo.GetAllAsync();

            var response = orders.Select(o => new OrderResponseDto
            {
                OrderId = o.OrderId,
                UserId = o.UserId,
                StoreId = o.StoreId,
                OrderDate = o.OrderDate,
                Status = o.Status,
                TotalAmount = o.TotalAmount,
                Items = o.OrderItems?
                    .Select(i => new OrderItemResponseDto
                    {
                        OrderItemId = i.OrderItemId,
                        PetId = i.PetId,
                        Quantity = i.Quantity,
                        Price = i.Price
                    }).ToList() ?? new List<OrderItemResponseDto>()
            });

            return Ok(response);
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult<OrderResponseDto>> UpdateStatus(int id, [FromBody] string status)
        {
            var updatedOrder = await _orderRepo.UpdateStatusAsync(id, status);
            if (updatedOrder == null) return NotFound();

            var response = new OrderResponseDto
            {
                OrderId = updatedOrder.OrderId,
                UserId = updatedOrder.UserId,
                StoreId = updatedOrder.StoreId,
                OrderDate = updatedOrder.OrderDate,
                Status = updatedOrder.Status,
                TotalAmount = updatedOrder.TotalAmount,
                Items = updatedOrder.OrderItems?
                    .Select(i => new OrderItemResponseDto
                    {
                        OrderItemId = i.OrderItemId,
                        PetId = i.PetId,
                        Quantity = i.Quantity,
                        Price = i.Price
                    }).ToList() ?? new List<OrderItemResponseDto>()
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _orderRepo.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
