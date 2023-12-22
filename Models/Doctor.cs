using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        public string Image {  get; set; }

        [Display(Name = "Job Type")]
        public int JobId { get; set; }
        [ForeignKey("JobId")]
        public virtual Job_title Job_Title { get; set; }

        [Display(Name = "FullName")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User_N {  get; set; }

    }
}
