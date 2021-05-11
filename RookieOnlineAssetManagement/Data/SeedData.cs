using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace RookieOnlineAssetManagement.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any movies.
                if (context.Locations.Any() && context.Categories.Any())
                {
                    return;   // DB has been seeded
                }

                context.Locations.AddRange(
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
                );

                context.SaveChanges();

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
        }

    }
}