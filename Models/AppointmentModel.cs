using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Models
{
    public class AppointmentModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string SelectedTime { get; set; }

        public string UserId { get; set; }

        public string DoctorId { get; set; }
    }
}
