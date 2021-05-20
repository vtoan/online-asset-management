using Microsoft.AspNetCore.Http;

namespace RookieOnlineAssetManagement.Utils
{
    public class RequestHelper
    {
        public static readonly string locationKey = "locationId";
        public static void SetLocationSession(HttpContext context, string locationId)
        {
            context.Session.SetString(locationKey, locationId);
        }

        public static string GetLocationSession(HttpContext context)
        {
            return context.Session.GetString(locationKey);
        }
    }
}