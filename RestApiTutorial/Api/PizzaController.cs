using Microsoft.AspNetCore.Mvc;
using RestApiTutorial.Data;
using RestApiTutorial.Models;
using RestApiTutorial.Services;

namespace RestApiTutorial.Api;

public class PizzaController : BaseApiController
{
    private readonly PizzaService _pizzaService;

    public PizzaController(AppDbContext context)
    {
        _pizzaService = new PizzaService(context);
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<Pizza>> GetAll()
    {
        return _pizzaService.GetAll();
    }

    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = _pizzaService.Get(id);

        if (pizza == null)
        {
            return NotFound();
        }

        return pizza;
    }

    //// POST action
    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        _pizzaService.Add(pizza);

        return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
    }

    //// PUT action
    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if (id != pizza.Id)
        {
            return BadRequest();
        }

        var existingPizza = _pizzaService.Get(id);

        if (existingPizza is null)
        {
            return NotFound();
        }

        _pizzaService.Update(id, pizza);

        return NoContent();
    }

    //// DELETE action
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = _pizzaService.Get(id);

        if (pizza is null)
        {
            return NotFound();
        }

        _pizzaService.Delete(id);

        return NoContent();
    }
}
