namespace Final_Project.Models
{
    public class Store
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        public int OwnerId { get; set; }

        public ICollection<Pet>? Pets { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
