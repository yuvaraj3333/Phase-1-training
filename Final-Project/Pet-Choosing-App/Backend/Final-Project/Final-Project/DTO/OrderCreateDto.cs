namespace Final_Project.DTOs.Orders
{
    public class OrderCreateDto
    {
        public int UserId { get; set; }
        public int StoreId { get; set; }
        public List<OrderItemCreateDto> Items { get; set; } = new List<OrderItemCreateDto>();
    }
}
