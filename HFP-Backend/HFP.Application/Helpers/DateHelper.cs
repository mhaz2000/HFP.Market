using System.Globalization;

namespace HFP.Application.Helpers
{
    internal static class DateHelper
    {
        public static DateTime ToDate(this string date, bool isStart)
        {
            var splitDate = date.Split("/");

            return isStart ? new DateTime(int.Parse(splitDate[0]), int.Parse(splitDate[1]), int.Parse(splitDate[2]), 0, 0, 0)
                : new DateTime(int.Parse(splitDate[0]), int.Parse(splitDate[1]), int.Parse(splitDate[2]), 23, 59, 59);
        }
    }
}
