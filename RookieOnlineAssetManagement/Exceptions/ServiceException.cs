using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Exceptions
{
    public class ServiceException : Exception
    {
        public ServiceException() { }
        DayOfWeek dayofweek;
        public Exception AgeNoValidException()
        {
            throw new Exception("Age is not valid");
        }
        public Exception JoinDateException()
        {           
            throw new Exception("Joined Date is : " + dayofweek.ToString());
        }
        public Exception CompareDateException()
        {
            throw new Exception("Joined Date is smaller tham Birth Of Date");
        }
        public Exception ReturnDateNotValidException()
        {
            throw new Exception("Return date not valid !");
        }
        public Exception PageException()
        {
            throw new Exception("Page and Page size not valid !");
        }
        public Exception CompareDateAssignedException()
        {
            throw new Exception("Assigned Date is smaller than Today");
        }

    }
}
