using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Middleware;
using RookieOnlineAssetManagement.ServiceExtensions;

namespace RookieOnlineAssetManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //db
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            //extension
            services.AddIdentityConfig();
            services.AddRepositories();
            services.AddBusinessService();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
            //other
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSwaggerGen();
            services.AddSession();
            //spa
            // In production, the React files will be served from this directory
            // services.AddSpaStaticFiles(configuration =>
            // {
            //     configuration.RootPath = "ClientApp/build";
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            // {
            //     serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
            // }

            app.UseGlobalHandlerException();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rookie Online Asset Management API V1");
                c.RoutePrefix = string.Empty;
            });

            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            //     // app.UseMigrationsEndPoint();
            // }
            // else
            // {
            //     // app.UseExceptionHandler("/Error");
            //     app.UseStatusCodePages();
            //     app.UseHsts();
            // }
            app.UseSession();
            app.UseCors(b =>
            {
                b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("total-pages").WithHeaders("total-pages");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<CheckLocationMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });


            // app.UseSpa(spa =>
            // {
            //     spa.Options.SourcePath = "ClientApp";

            //     if (env.IsDevelopment())
            //     {
            //         spa.UseReactDevelopmentServer(npmScript: "start");
            //     }
            // });
        }
    }
}
