using System.Collections.Generic;

namespace Final_Project.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int PetId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Order Order { get; set; } = null!;
        public Pet Pet { get; set; } = null!;
        public ICollection<Return> Returns { get; set; } = new List<Return>();
    }
}
