namespace Final_Project.DTOs.Pets
{
    public class StoreUpdateDto
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string Location { get; set; }
        public int? OwnerId { get; set; }
    }
}
