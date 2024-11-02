using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using YourNamespace.Models;

namespace YourNamespace.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IMongoCollection<Employee> _employees;
        private readonly IMongoCollection<Key> _keys;

        public EmployeesController(IMongoClient client)
        {
            var database = client.GetDatabase("restaurant");
            _employees = database.GetCollection<Employee>("Employees");
            _keys = database.GetCollection<Key>("Keys");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Views/Home/Register.cshtml");
        }

        [HttpPost]
        public IActionResult Register(Employee employee, string username, string password)
        {
            if (ModelState.IsValid)
            {
                var key = new Key { Username = username, Password = password };
                _keys.InsertOne(key);

                employee.KeyId = key.Id;
                _employees.InsertOne(employee);

                return RedirectToAction("Index", "Home");
            }
            return View("~/Views/Home/Register.cshtml", employee);
        }
    }
}
