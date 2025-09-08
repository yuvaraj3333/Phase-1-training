namespace Final_Project.DTOs.Pets
{
    public class PetResponseDto
    {
        public int PetId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Breed { get; set; }
        public string Gender { get; set; }
        public decimal Price { get; set; }
        public int StoreId { get; set; }
    }
}
