namespace Final_Project.DTOs.Orders
{
    public class OrderItemResponseDto
    {
        public int OrderItemId { get; set; }
        public int PetId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
