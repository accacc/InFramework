using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Date
{
    public static class DateTimeHelper
    {
        public static bool IsWeekday(DateTime date)
        {
            return (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday);
        }

        public static Boolean IsHourBetween(TimeSpan? startHour, TimeSpan? endHour)
        {
            var dateNow = DateTime.Now;

            Boolean IsHourBetween = false;
            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today;

            if (startHour >= endHour)
            {
                endDate = endDate.AddDays(1);
            }

            startDate = startDate.Date + startHour.Value;

            endDate = endDate.Date + endHour.Value;

            if ((dateNow >= startDate) && (dateNow <= endDate))
            {
                IsHourBetween = true;
            }
            return IsHourBetween;
        }


    }
}
