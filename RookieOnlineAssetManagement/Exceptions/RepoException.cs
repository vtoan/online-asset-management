using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Exceptions
{
    public  class RepoException : Exception
    {
        const string erroMessage = "Repo - ";
        public RepoException( string msg = "") : base(erroMessage + " " + msg)
        { }
    }
}

