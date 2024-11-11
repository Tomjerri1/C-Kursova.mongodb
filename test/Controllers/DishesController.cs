using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Threading.Tasks;
using YourNamespace.Models;

namespace YourNamespace.Controllers
{
    public class DishesController : Controller
    {
        private readonly IMongoCollection<Dish> _dishesCollection;

        public DishesController(IMongoDatabase database)
        {
            _dishesCollection = database.GetCollection<Dish>("Dishes");
        }

        [HttpGet]
        public IActionResult AddDish()
        {
            return View("~/Views/Home/AddDish.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> AddDish(Dish newDish)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Home/AddDish.cshtml", newDish);
            }

            await _dishesCollection.InsertOneAsync(newDish);
            return RedirectToAction("ManageDishes");
        }

        public IActionResult ListDishes(string searchQuery)
    {
        var query = _dishesCollection.AsQueryable();

        if (!string.IsNullOrEmpty(searchQuery))
        {
            string normalizedSearchQuery = searchQuery.ToLower();
            query = query.Where(d => d.Name.ToLower().Contains(normalizedSearchQuery));
        }

        var dishes = query.ToList();
        return View("~/Views/Home/ListDishes.cshtml", dishes);
    }

        public async Task<IActionResult> ManageDishes()
        {
            var dishes = await _dishesCollection.Find(d => true).ToListAsync();
            return View("~/Views/Home/ManageDishes.cshtml", dishes);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var dish = await _dishesCollection.Find(d => d.Id == id).FirstOrDefaultAsync();
            if (dish == null)
            {
                return NotFound();
            }
            return View("~/Views/Home/EditDish.cshtml", dish);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Dish updatedDish)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Home/EditDish.cshtml", updatedDish);
            }

            await _dishesCollection.ReplaceOneAsync(d => d.Id == updatedDish.Id, updatedDish);
            return RedirectToAction("ManageDishes");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _dishesCollection.DeleteOneAsync(d => d.Id == id);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return RedirectToAction("ManageDishes");
        }
    }
}
