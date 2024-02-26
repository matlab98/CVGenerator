using System.Globalization;

namespace CVDynamicGenerator.WebApi.Utilities
{
    public class FormatDateTime
    {
        public static DateTime DateNow(DateTime utcTime)
        {
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");

            DateTime custDateTime = TimeZoneInfo.ConvertTime(utcTime, tz);
            //DateTime custDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, myZone);
            return custDateTime;
        }

        public static string MonthYear(DateTime utcTime)
        {
            CultureInfo culture = new CultureInfo("en-US");
            return utcTime.ToString("MMMM, yyyy", culture);
        }
    }
}
