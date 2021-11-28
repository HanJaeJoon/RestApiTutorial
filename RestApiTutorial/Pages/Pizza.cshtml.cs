using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestApiTutorial.Data;
using RestApiTutorial.Models;
using RestApiTutorial.Services;

namespace RestApiTutorial.Pages
{
    public class PizzaModel : PageModel
    {
        private readonly PizzaService _pizzaService;
        private readonly ILogger<PizzaModel> _logger;

        public PizzaModel(AppDbContext context, ILogger<PizzaModel> logger)
        {
            _pizzaService = new PizzaService(context);
            _logger = logger;
        }

        public List<Pizza> pizzas = new();

        [BindProperty]
        public Pizza NewPizza { get; set; } = new();

        public string GlutenFreeText(Pizza pizza)
        {
            if (pizza.IsGlutenFree)
                return "Gluten Free";
            return "Not Gluten Free";
        }

        public void OnGet()
        {
            pizzas = _pizzaService.GetAll();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _pizzaService.Add(NewPizza);

            return RedirectToPage("Pizza");
        }

        public IActionResult OnPostDelete(int id)
        {
            _pizzaService.Delete(id);

            return RedirectToPage("Pizza");
        }
    }
}