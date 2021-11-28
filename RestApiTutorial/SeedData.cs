using Microsoft.EntityFrameworkCore;
using RestApiTutorial.Data;
using RestApiTutorial.Models;

namespace RestApiTutorial;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var dbContext = new AppDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
        {
            if (dbContext.Pizzas.Any())
            {
                return;
            }

            PopulateTestData(dbContext);
        }
    }

    private static void PopulateTestData(AppDbContext dbContext)
    {
        foreach (var item in dbContext.Pizzas)
        {
            dbContext.Remove(item);
        }

        dbContext.SaveChanges();

        dbContext.Pizzas.AddRange(new List<Pizza>
        {
            new Pizza { Id = 1, Name = "Classic Italian", Price = 20.00M, Size = PizzaSize.Large, IsGlutenFree = false },
            new Pizza { Id = 2, Name = "Veggie", Price = 15.00M, Size = PizzaSize.Small, IsGlutenFree = true }
        });

        dbContext.SaveChanges();
    }
}
