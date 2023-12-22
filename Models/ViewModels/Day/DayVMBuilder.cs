using System;

namespace Hospital.Models.ViewModels.Day
{
    public class DayVMBuilder
    {
        public DayVM Build(DateTime date)
        {
            return new DayVM()
            {
                Date = date,
                IsNotCurrentMonth = date.Month != DateTime.Now.Month,
                IsWeekendOrHoliday = date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday,
                IsToday = date.Date == DateTime.Now.Date
            };
        }
    }
}
