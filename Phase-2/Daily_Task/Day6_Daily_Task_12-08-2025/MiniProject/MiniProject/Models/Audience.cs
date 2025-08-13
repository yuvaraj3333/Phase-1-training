using System;
using System.ComponentModel.DataAnnotations;

namespace MovieBookingApp.Models
{
    public class Audience
    {
        public string Name { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }
        public int Id { get; internal set; }
    }
}
