using System.Globalization;

namespace RSI_REST_SERVER.Utilities
{
    public static class Utilities
    {
        public static int GetWeekNumber(DateTime date)
        {
            CultureInfo ci = CultureInfo.CurrentCulture;
            return ci.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static DateTime GetLocalDateTime(DateTime date)
        {
            return date.ToLocalTime();
        }

        public static DateTime FirstDayOfWeek(DateTime date)
        {
            DateTime inputDate = GetLocalDateTime(date);
            int diff = (7 + (inputDate.DayOfWeek - DayOfWeek.Monday)) % 7;
            return inputDate.AddDays(-1 * diff).Date;
        }

        public static DateTime LastDayOfWeek(DateTime date)
        {
            DateTime inputDate = GetLocalDateTime(date);
            int diff = (7 - (inputDate.DayOfWeek - DayOfWeek.Monday)) % 7;
            return inputDate.AddDays(diff).Date;
        }
    }
}
