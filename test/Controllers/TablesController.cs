using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using YourNamespace.Models;

namespace YourNamespace.Controllers
{
    public class TablesController : Controller
    {
        private readonly IMongoCollection<Table> _tables;

        public TablesController(IMongoDatabase database)
        {
            _tables = database.GetCollection<Table>("Tables");
        }

        [HttpGet]
        public IActionResult AddTable()
        {
            return View("~/Views/Home/AddTable.cshtml");
        }

        [HttpPost]
        public IActionResult AddTable(Table table)
        {
            if (ModelState.IsValid)
            {
                table.IsAvailable = true;

                _tables.InsertOne(table);
                return RedirectToAction("Index", "Home");
            }
            return View("~/Views/Home/AddTable.cshtml", table);
        }
    }
}
