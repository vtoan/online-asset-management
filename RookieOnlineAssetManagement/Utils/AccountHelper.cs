using System;

namespace RookieOnlineAssetManagement.Utils
{
    public class AccountHelper
    {
        public static string GenerateAccountPass(string userName, DateTime dateBirth)
        {
            return userName + "@" + dateBirth.ToString("ddMMyyyy");
        }
    }
}