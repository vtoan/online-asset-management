using Microsoft.Extensions.DependencyInjection;
using RookieOnlineAssetManagement.Services;

namespace RookieOnlineAssetManagement.ServiceExtensions
{
    public static class BusinessService
    {
        public static IServiceCollection AddBusinessService(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAssignmentService, AssignmentService>();
            services.AddScoped<IReturnRequestService, ReturnRequestService>();
            return services;
        }
    }
}