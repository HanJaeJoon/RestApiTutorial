using RestApiTutorial.Data;
using RestApiTutorial.Models;

namespace RestApiTutorial.Services;
public class PizzaService
{
    private readonly AppDbContext _context;

    public PizzaService(AppDbContext context)
    {
        _context = context;
    }

    public List<Pizza> GetAll() => _context.Pizzas.ToList();

    public Pizza? Get(int id) => _context.Pizzas.FirstOrDefault(p => p.Id == id);

    public void Add(Pizza pizza)
    {
        _context.Pizzas.Add(pizza);

        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var pizza = Get(id);

        if (pizza is null)
        {
            return;
        }

        _context.Pizzas.Remove(pizza);
        _context.SaveChanges();
    }

    public void Update(int id, Pizza pizza)
    {
        var oldPizza = Get(id);

        if (oldPizza is null)
        {
            return;
        }

        _context.Entry(oldPizza).CurrentValues.SetValues(pizza);
        _context.SaveChanges();
    }
}