using System.ComponentModel.DataAnnotations;

namespace MovieBookingApp.Models
{
    public class Audience
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Name must be at least 5 characters long.")]
        public string Name { get; set; }

        [Required]
        public string Gender { get; set; }

        [Range(6, 120, ErrorMessage = "Age must be greater than 5.")]
        public int Age { get; set; }
    }
}
