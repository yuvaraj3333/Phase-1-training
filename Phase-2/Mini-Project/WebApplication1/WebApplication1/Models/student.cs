using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class student
    {
        [Required]
        public int StudentID { get; set; }
        public string FirstName { get; set; }

        public string? LastName { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }
        public string Department { get; set; }
    }
}
