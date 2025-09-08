using System;
using System.Collections.Generic;

namespace Final_Project.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }        
        public int StoreId { get; set; }         
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }

        public User User { get; set; } = null!;
        public Store Store { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<Return> Returns { get; set; } = new List<Return>();
    }
}
