using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Generic;

namespace Hospital.Models.ViewModels
{
    public class DoctorVM
    {
        public Doctor Doctor { get; set; }
        public IEnumerable<SelectListItem> JobSelectList { get; set; }

        public IEnumerable<SelectListItem> UserSelectList { get; set; }
    }
}
