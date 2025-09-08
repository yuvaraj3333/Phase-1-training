namespace Final_Project.Models
{
    public class Pet
    {
        public int PetId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StoreId { get; set; }

        public Store Store { get; set; } = null!;
    }
}
