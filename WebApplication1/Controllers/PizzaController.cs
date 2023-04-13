using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Migrations;
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

            var data = _context.Pizzas.Include(p => p.Category).ToArray();

            return View(data);
        }

        public IActionResult Details(int id)
        {
            var _context = new PizzaContext();
            Pizza pizza = _context.Pizzas.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
            return View(pizza);
        }

        [HttpGet]

        public IActionResult Create()
        {
            using (PizzaContext _context = new PizzaContext())
            {
                List<Category> categories = _context.Categories.ToList();

                PizzaFormModel model = new PizzaFormModel();
                model.Pizza = new Pizza();
                model.Categories = categories;
                return View("Create", model);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( PizzaFormModel data )
        {
			using PizzaContext _context = new PizzaContext();
            if (!ModelState.IsValid)
            {
                data.Categories = _context.Categories.ToList();
				return View("Create", data);
            }

            _context.Pizzas.Add(data.Pizza);
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

            var formModel = new PizzaFormModel
            {
                Pizza = pizzaToEdit,
                Categories = _context.Categories.ToList()
            };

			return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaFormModel data)
        {
            using var _context = new PizzaContext();
            if (!ModelState.IsValid)
            {
                data.Categories = _context.Categories.ToList();
                return View("Update", data);
            }
            Pizza pizzaToEdit = _context.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

            if(pizzaToEdit == null)
            {
                return NotFound();
            }

            pizzaToEdit.Name = data.Pizza.Name;
            pizzaToEdit.Description = data.Pizza.Description;
            pizzaToEdit.CategoryId = data.Pizza.CategoryId;
            pizzaToEdit.Image = data.Pizza.Image;
            pizzaToEdit.Price = data.Pizza.Price;

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
