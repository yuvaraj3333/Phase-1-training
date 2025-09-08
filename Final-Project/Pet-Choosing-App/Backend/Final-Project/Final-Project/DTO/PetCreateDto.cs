namespace Final_Project.DTOs.Pets
{
    public class PetCreateDto
    {
        public string Name { get; set; }
        public string Breed { get; set; }
        public string Gender { get; set; }
        public decimal Price { get; set; }
        public int StoreId { get; set; }  // The store adding the pet
    }
}
