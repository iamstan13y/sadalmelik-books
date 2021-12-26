using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using sadalmelik_books.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sadalmelik_books.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if (!context.Books.Any())
                {
                    context.Books.Add(new Book
                    {
                        Author = "Keith Stanley",
                        DateAdded = DateTime.Now,
                        CoverUrl = "https://....",
                        DateRead = DateTime.Now.AddDays(-10),
                        IsRead = true,
                        Description = "Some Shitty Description",
                        Genre = "The Genre",
                        Rate = 5,
                        Title = "Social Engineering: The Ultimate Guide.",
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}
