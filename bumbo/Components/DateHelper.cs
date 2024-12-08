using System.Globalization;

namespace bumbo.Components
{
    public class DateHelper
    {
        public int GetCurrentYear()
        {
            return DateTime.Now.Year;
        }

        public int GetCurrentWeek()
        {
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            Calendar calendar = cultureInfo.Calendar;

            DateTime currentDate = DateTime.Now;

            return calendar.GetWeekOfYear(currentDate, cultureInfo.DateTimeFormat.CalendarWeekRule, cultureInfo.DateTimeFormat.FirstDayOfWeek);
        }

    }
}
