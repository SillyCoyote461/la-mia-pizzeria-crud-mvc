using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PizzaController : Controller
    {
        private readonly ILogger<PizzaController> _logger;

        public PizzaController(ILogger<PizzaController> logger)
        {
            _logger = logger;

        }

        public IActionResult Index()
        {
            using var _context = new PizzaContext();
            var pizzas = _context.Pizzas.ToArray();
            return View(pizzas);
        }

        public IActionResult Details(int id)
        {
            var _context = new PizzaContext();
            Pizza pizza = _context.Pizzas.FirstOrDefault(p => p.Id == id);
            return View(pizza);
        }

        [HttpGet]

        public IActionResult Create()
        {

            return View("Create");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( Pizza pizza )
        {
            if (!ModelState.IsValid)
            {
                return View("Create", pizza);
            }

            Pizza record = new Pizza();
            record.Name = pizza.Name;
            record.Description = pizza.Description;
            record.Price = pizza.Price;
            record.Image = pizza.Image;

            using var _context = new PizzaContext();

            _context.Pizzas.Add(record);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
