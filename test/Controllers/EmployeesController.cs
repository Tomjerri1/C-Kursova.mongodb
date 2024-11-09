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
        public IActionResult RegisterWaiter()
        {
            return View("~/Views/Home/RegisterWaiter.cshtml");
        }

        [HttpPost]
        public IActionResult RegisterWaiter(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.Role = "Waiter";

                var existingEmployee = _employees.Find(e => e.Login == employee.Login).FirstOrDefault();
                if (existingEmployee != null)
                {
                    ModelState.AddModelError("Login", "Цей логін вже зайнятий, виберіть інший.");
                    return View("~/Views/Home/RegisterWaiter.cshtml", employee);
                }

                _employees.InsertOne(employee);
                return RedirectToAction("Index", "Home");
            }

            return View("~/Views/Home/RegisterWaiter.cshtml", employee);
        }
        [HttpGet]
        public IActionResult RegisterAdministrator()
        {
            return View("~/Views/Home/RegisterAdministrator.cshtml");
        }

        [HttpPost]
        public IActionResult RegisterAdministrator(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.Role = "Administrator";

                var existingEmployee = _employees.Find(e => e.Login == employee.Login).FirstOrDefault();
                if (existingEmployee != null)
                {
                    ModelState.AddModelError("Login", "Цей логін вже зайнятий, виберіть інший.");
                    return View("~/Views/Home/RegisterAdministrator.cshtml", employee);
                }

                _employees.InsertOne(employee);
                return RedirectToAction("Index", "Home");
            }

            return View("~/Views/Home/RegisterAdministrator.cshtml", employee);
        }
        public IActionResult ListEmployees()
        {
            var employees = _employees.Find(emp => true).ToList();
            return View("~/Views/Home/ListEmployees.cshtml", employees);
        }

        [HttpPost]
        public IActionResult DeleteEmployee(string id)
        {
            _employees.DeleteOne(emp => emp.Id == id);
            return RedirectToAction("ListEmployees");
        }
    }
}
