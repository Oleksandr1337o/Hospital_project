using System;
using System.Collections.Generic;

namespace Hospital.Models.ViewModels.Day
{
    public class DayVM
    {
        public DateTime Date { get; set; }
        public bool IsWeekendOrHoliday { get; set; }

        public bool IsNotCurrentMonth { get; set; }
        public bool IsToday { get; set; }

        public string SelectedTime { get; set; }

    }
}
