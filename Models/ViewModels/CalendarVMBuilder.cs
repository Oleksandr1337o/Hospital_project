using Hospital.Models.ViewModels.Day;
using Hospital.Models.ViewModels;
using System;

public class CalendarVMBuilder
{
    private DateTime _currentMonth;

    public CalendarVMBuilder(DateTime currentMonth)
    {
        _currentMonth = currentMonth;
    }

    public CalendarVM Build()
    {
        DayVM[,] days = new DayVM[6, 7];
        DateTime firstDayOfMonth = new DateTime(_currentMonth.Year, _currentMonth.Month, 1);

        int offset = (int)firstDayOfMonth.DayOfWeek;
        if (offset == 0)
            offset = 7;
        offset--;

        DateTime currentDate = DateTime.Now.Date;  
        DateTime date = firstDayOfMonth.AddDays(offset * -1);

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                days[i, j] = new DayVMBuilder().Build(date);


                date = date.AddDays(1);


                if (days[i, j].Date.Date < currentDate)
                {
                    days[i, j].IsNotCurrentMonth = true;
                }
            }
        }

        return new CalendarVM()
        {
            Date = _currentMonth,
            Days = days
        };
    }
}