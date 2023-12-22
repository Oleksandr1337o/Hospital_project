using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
    public class Job_title
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
