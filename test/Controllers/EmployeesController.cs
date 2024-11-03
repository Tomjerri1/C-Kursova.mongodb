using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using YourNamespace.Models;
using System.Linq;

namespace YourNamespace.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IMongoCollection<Employee> _employees;

        public EmployeesController(IMongoClient client)
        {
            var database = client.GetDatabase("restaurant");
            _employees = database.GetCollection<Employee>("Employees");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Views/Home/Register.cshtml");
        }

        [HttpPost]
        public IActionResult Register(Employee employee)
        {
            if (_employees.AsQueryable().Any(e => e.Login == employee.Login))
            {
                ModelState.AddModelError("Login", "Цей логін вже зайнятий. Придумайте інший.");
            }

            if (ModelState.IsValid)
            {
                 _employees.InsertOne(employee);
                return RedirectToAction("Index", "Home");
            }

            return View("~/Views/Home/Register.cshtml", employee);
        }
    }
}
