// Controllers/DishesController.cs
using Microsoft.AspNetCore.Mvc;
using YourNamespace.Models;
using MongoDB.Driver;

namespace YourNamespace.Controllers
{
    public class DishesController : Controller
    {
        private readonly IMongoCollection<Dish> _dishesCollection;

        public DishesController(IMongoDatabase database)
        {
            _dishesCollection = database.GetCollection<Dish>("Dishes");
        }

        public IActionResult AddDish()
        {
            return View("~/Views/Home/AddDish.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> AddDish(Dish dish)
        {
            if (ModelState.IsValid)
            {
                await _dishesCollection.InsertOneAsync(dish);
                return RedirectToAction("Index", "Home");
            }
            return View("~/Views/Home/AddDish.cshtml", dish);
        }
    }
}
