using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Utils
{
    public class DateTimeHelper
    {
        public static bool CheckDateGreaterThan(DateTime SmallDate, DateTime BigDate)
        {
            return !(SmallDate > BigDate);
        }
        public static bool CheckAgeGreaterThan(int age, DateTime BirthOfDate)
        {
            return (DateTime.Now.Year - BirthOfDate.Year >= age);
        }
        public static bool CheckIsSaturdayOrSunday(DateTime JoinedDate, out DayOfWeek dayofweek)
        {
            dayofweek = JoinedDate.DayOfWeek;
            return (dayofweek == DayOfWeek.Sunday || dayofweek == DayOfWeek.Sunday);
        }
        public static bool IsDateTime(string text)
        {
            DateTime dateTime;
            bool isDateTime;
            // Check for empty string.
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            isDateTime = DateTime.TryParse(text, out dateTime);

            return isDateTime;
        }
    }
}