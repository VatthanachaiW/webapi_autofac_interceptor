using System;
using System.Linq;
using BookStore.API.Models;
using BookStore.API.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BookStore.API.Connections
{
  public static class DbInitial
  {
    public static void Seeding(this IServiceScope scope)
    {
      var services = scope.ServiceProvider;

      try
      {
        scope.ServiceProvider.GetService<IApplicationDbContext>().Database.Migrate();

        IApplicationDbContext context = services.GetService<IApplicationDbContext>();


        if (!context.Set<BookCategory>().Any())
        {
          var category = new BookCategory
          {
            Name = "Demo BookCategory"
          };

          context.Set<BookCategory>().Add(category);
          context.SaveChangeAsync().GetAwaiter().GetResult();
        }

        if (!context.Set<Book>().Any())
        {
          var category = context.Set<BookCategory>().FirstOrDefault(s => s.Name == "Demo BookCategory");
          var book = new Book
          {
            Name = "Demo Book",
            BookCategory = category
          };
          context.Set<Book>().Add(book);
          context.SaveChangeAsync().GetAwaiter().GetResult();
        }
      }
      catch (Exception ex)
      {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
      }
    }
  }
}