using System;

namespace Final_Project.Models
{
    public class Return
    {
        public int ReturnId { get; set; }
        public int OrderId { get; set; }
        public int OrderItemId { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime ReturnDate { get; set; }
        public string Status { get; set; } = string.Empty;

        public Order Order { get; set; } = null!;
        public OrderItem OrderItem { get; set; } = null!;
    }
}
