using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestApiTutorial.Models;
using RestApiTutorial.Services;

namespace RestApiTutorial.Pages
{
    public class PizzaModel : PageModel
    {
        private readonly ILogger<PizzaModel> _logger;

        public PizzaModel(ILogger<PizzaModel> logger)
        {
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
            pizzas = PizzaService.GetAll();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            PizzaService.Add(NewPizza);
            return RedirectToPage("Pizza");
        }
        public IActionResult OnPostDelete(int id)
        {
            PizzaService.Delete(id);
            return RedirectToPage("Pizza");
        }
    }
}