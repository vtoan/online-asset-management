using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RookieOnlineAssetManagement.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RookieOnlineAssetManagement.Data
{
    public class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // ============= //
                if (!context.Locations.Any())
                {
                    var locas = new List<Location>(){
                    new Entities.Location
                    {
                        LocationId = Guid.NewGuid().ToString(),
                        LocationName = "HCM"
                    },
                    new Entities.Location
                    {
                        LocationId = Guid.NewGuid().ToString(),
                        LocationName = "HN"
                    }
                };
                    context.Locations.AddRange(locas);
                    context.SaveChanges();
                }
                // ============= //
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(
                                  new Entities.Category
                                  {
                                      CategoryId = Guid.NewGuid().ToString(),
                                      CategoryName = "Laptop",
                                      ShortName = "LT",
                                      NumIncrease = 0
                                  },
                                   new Entities.Category
                                   {
                                       CategoryId = Guid.NewGuid().ToString(),
                                       CategoryName = "Personal Computer",
                                       ShortName = "PC",
                                       NumIncrease = 0
                                   }
                              );
                    context.SaveChanges();
                }
                // ============= //
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync("admin"))
                    await roleManager.CreateAsync(new IdentityRole("admin"));
                if (!await roleManager.RoleExistsAsync("user"))
                    await roleManager.CreateAsync(new IdentityRole("user"));
                // ============= //
                if (!context.Users.Any())
                {
                    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
                    var loca = context.Locations.ToList();
                    await userManager.CreateAsync(new User()
                    {
                        FirstName = "Admin",
                        UserName = "admin-hcm",
                        LastName = "HCM",
                        StaffCode = "SD0001",
                        Gender = false,
                        IsChange = false,
                        LocationId = loca.Where(item => item.LocationName == "HCM").First().LocationId

                    }, "123456");

                    await userManager.CreateAsync(new User()
                    {
                        FirstName = "Admin",
                        LastName = "HN",
                        UserName = "admin-hn",
                        StaffCode = "SD0002",
                        Gender = false,
                        IsChange = false,
                        LocationId = loca.Where(item => item.LocationName == "HN").First().LocationId,
                    }, "123456");

                    var userHCMRole = await userManager.FindByNameAsync("admin-hcm");
                    var userHNRole = await userManager.FindByNameAsync("admin-hn");
                    var roleAdmin = await context.Roles.Where(item => item.NormalizedName == "admin").FirstAsync();
                    await context.UserRoles.AddAsync(
                        new IdentityUserRole<string>() { UserId = userHCMRole.Id, RoleId = roleAdmin.Id }
                    );
                    await context.UserRoles.AddAsync(
                       new IdentityUserRole<string>() { UserId = userHNRole.Id, RoleId = roleAdmin.Id }
                   );
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}