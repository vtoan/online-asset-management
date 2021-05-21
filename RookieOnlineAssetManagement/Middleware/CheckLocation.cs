using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Utils;

namespace RookieOnlineAssetManagement.Middleware
{
    public class CheckLocationMiddleware
    {
        private readonly RequestDelegate _next;

        public CheckLocationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<User> userManager)
        {
            var locaId = RequestHelper.GetLocationSession(context);
            if (locaId == null)
            {
                var user = await userManager.GetUserAsync(context.User);
                if (user != null)
                    RequestHelper.SetLocationSession(context, user.LocationId);
            }
            await _next(context);
        }
    }
}