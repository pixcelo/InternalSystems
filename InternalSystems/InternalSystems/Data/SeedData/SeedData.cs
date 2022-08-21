using System;
using InternalSystems.Models;
using Microsoft.EntityFrameworkCore;

namespace InternalSystems.SeedData
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AccessContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AccessContext>>()))
            {
                // Look for any movies.
                if (context.AccessModel.Any())
                {
                    return;   // DB has been seeded
                }

                context.AccessModel.AddRange(
                    new AccessModel
                    {
                        Title = "When Harry Met Sally",
                        ReleaseDate = DateTime.Parse("1989-2-12"),
                        Genre = "Romantic Comedy",
                        Price = 7.99M
                    },

                    new AccessModel
                    {
                        Title = "Ghostbusters ",
                        ReleaseDate = DateTime.Parse("1984-3-13"),
                        Genre = "Comedy",
                        Price = 8.99M
                    },

                    new AccessModel
                    {
                        Title = "Ghostbusters 2",
                        ReleaseDate = DateTime.Parse("1986-2-23"),
                        Genre = "Comedy",
                        Price = 9.99M
                    },

                    new AccessModel
                    {
                        Title = "Rio Bravo",
                        ReleaseDate = DateTime.Parse("1959-4-15"),
                        Genre = "Western",
                        Price = 3.99M
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
