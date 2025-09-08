using Final_Project.DTOs.Pets;

namespace Final_Project.DTOs.Stores
{
    public class StoreResponseDto
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        public int OwnerId { get; set; }

        public List<PetResponseDto> Pets { get; set; } = new();
    }
}
