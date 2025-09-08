namespace Final_Project.DTO
{
    public class CartItemResponseDto
    {
        public int CartItemId { get; set; }
        public int PetId { get; set; }
        public string PetName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total => Price * Quantity;
    }
}
