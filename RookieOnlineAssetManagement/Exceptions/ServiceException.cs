using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Exceptions
{
    public class ServiceException : Exception
    {
        const string erroMessage = "Service - ";
        public ServiceException(string msg = "") : base(erroMessage + " " + msg)
        { } 
    }
}
