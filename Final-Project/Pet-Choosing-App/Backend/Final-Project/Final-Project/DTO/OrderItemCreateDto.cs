namespace Final_Project.DTOs.Orders
{
    public class OrderItemCreateDto
    {
        public int PetId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } 
    }
}
