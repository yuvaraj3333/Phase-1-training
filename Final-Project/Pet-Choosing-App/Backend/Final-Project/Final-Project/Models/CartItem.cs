namespace Final_Project.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int PetId { get; set; }
        public int Quantity { get; set; }

        public Cart Cart { get; set; } = null!;
        public Pet Pet { get; set; } = null!;
    }
}
