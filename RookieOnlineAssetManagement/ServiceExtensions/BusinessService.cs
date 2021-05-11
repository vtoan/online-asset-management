using Microsoft.Extensions.DependencyInjection;
using RookieOnlineAssetManagement.Services;

namespace RookieOnlineAssetManagement.ServiceExtensions
{
    public static class BusinessService
    {
        public static IServiceCollection AddBusinessService(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}