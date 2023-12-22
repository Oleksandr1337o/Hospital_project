using System.Collections;
using System.Collections.Generic;

namespace Hospital.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Doctor> Doctors { get; set; }
        public IEnumerable<Job_title> JobTitles { get; set; }
        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }
    }
}
