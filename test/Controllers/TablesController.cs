using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using YourNamespace.Models;
using MongoDB.Bson;

namespace YourNamespace.Controllers
{
    public class TablesController : Controller
    {
        private readonly IMongoCollection<Table> _tables;

        public TablesController(IMongoClient client)
        {
            var database = client.GetDatabase("restaurant");
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

        [HttpGet]
        public IActionResult ListTables()
        {
            var tables = _tables.Find(table => true).ToList();
            return View("~/Views/Home/ListTables.cshtml", tables);
        }
        [HttpPost]
        public IActionResult DeleteTable(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var objectId = new ObjectId(id);
                _tables.DeleteOne(table => table.Id == objectId);
            }
            return RedirectToAction("DeleteTables");
        }

        public IActionResult DeleteTables()
        {
            var tables = _tables.Find(table => true).ToList();
            return View("~/Views/Home/DeleteTables.cshtml", tables);
        }

    }
}
