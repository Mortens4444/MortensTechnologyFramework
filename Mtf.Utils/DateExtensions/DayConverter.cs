using Mtf.Utils.EnumExtensions;

namespace Mtf.Utils.DateExtensions
{
    public class DayConverter
    {
        public Day? GetDayFromString(string dayName)
        {
            if (dayName.Length > 3)
            {
                dayName = dayName.Substring(0, 3);
            }
            dayName = dayName.ToLower();
            switch (dayName)
            {
                case "mon":
                    return Day.Monday;
                case "tue":
                    return Day.Tuesday;
                case "wed":
                    return Day.Wednesday;
                case "thu":
                    return Day.Thursday;
                case "fri":
                    return Day.Friday;
                case "sat":
                    return Day.Saturday;
                case "sun":
                    return Day.Sunday;
                default:
                    return null;
            }
        }

        public string GetStringFromDay(Day day)
        {
            return day == Day.Unknown ? null : day.GetDescription();
        }
    }
}