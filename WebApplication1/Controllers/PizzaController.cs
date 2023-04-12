using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Models;

namespace Pizzeria.Controllers
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

        [HttpGet]
        public IActionResult Update(int id)
        {
            using var _context = new PizzaContext();
            Pizza pizzaToEdit = _context.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();
            if (pizzaToEdit == null)
            {
                return NotFound();
            }

            return View(pizzaToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Pizza data)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", data);
            }
            using var _context = new PizzaContext();
            Pizza pizzaToEdit = _context.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

            if(pizzaToEdit == null)
            {
                return NotFound();
            }
            
            pizzaToEdit.Name = data.Name;
            pizzaToEdit.Description = data.Description;
            pizzaToEdit.Price = data.Price;
            pizzaToEdit.Image = data.Image;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            using var _context = new PizzaContext();
            Pizza pizzaToDelete = _context.Pizzas.Where(p => p.Id == id).FirstOrDefault();
            if(pizzaToDelete == null)
            {
                return NotFound();
            }

            _context.Pizzas.Remove(pizzaToDelete);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
