using Microsoft.EntityFrameworkCore;
using RestApiTutorial.Models;

namespace RestApiTutorial.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pizza> Pizzas => Set<Pizza>();
    }
}