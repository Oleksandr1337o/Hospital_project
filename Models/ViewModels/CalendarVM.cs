using Hospital.Models.ViewModels.Day;
using System;

namespace Hospital.Models.ViewModels
{
    public class CalendarVM
    {
        public DateTime Date { get; set; }
        public DayVM[,] Days { get; set; }

    }
}
