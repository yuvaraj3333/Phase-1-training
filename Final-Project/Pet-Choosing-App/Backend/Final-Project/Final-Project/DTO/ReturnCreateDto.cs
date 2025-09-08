namespace Final_Project.DTOs.Returns
{
    public class ReturnCreateDto
    {
        public int OrderId { get; set; }
        public int OrderItemId { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
