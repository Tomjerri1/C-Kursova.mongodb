using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using YourNamespace.Models;
using System.Linq;
using MongoDB.Bson;

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
        public IActionResult ListWaiters(string searchQuery)
        {
            var query = _employees.AsQueryable()
                .Where(e => e.Role == "Waiter");

            if (!string.IsNullOrEmpty(searchQuery))
            {
                string normalizedSearchQuery = searchQuery.ToLower();
                query = query.Where(e => e.FirstName.ToLower().Contains(normalizedSearchQuery) || 
                                        e.LastName.ToLower().Contains(normalizedSearchQuery));
            }

            var waiters = query.ToList();
            return View("~/Views/Home/ListWaiters.cshtml", waiters);
        }

        public IActionResult ListAdmins(string searchQuery)
        {
            var query = _employees.AsQueryable()
                .Where(e => e.Role == "Administrator");

            if (!string.IsNullOrEmpty(searchQuery))
            {
                string normalizedSearchQuery = searchQuery.ToLower();
                query = query.Where(e => e.FirstName.ToLower().Contains(normalizedSearchQuery) || 
                                        e.LastName.ToLower().Contains(normalizedSearchQuery));
            }

            var admins = query.ToList();
            return View("~/Views/Home/ListAdmins.cshtml",admins);
        }

        public IActionResult DeleteEmployee(string id)
        {
            if (ObjectId.TryParse(id, out ObjectId objectId))
            {
                var filter = Builders<Employee>.Filter.Eq("_id", objectId);
                _employees.DeleteOne(filter);
            }
            else
            {
                TempData["Error"] = "Невірний ідентифікатор працівника.";
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult WaitersWorkingToday()
        {
            string today = DateTime.Now.DayOfWeek.ToString();

            var waitersWorkingToday = _employees
                .Find(emp => emp.Role == "Waiter" && emp.WorkingDays.Contains(today))
                .ToList();

            return View("~/Views/Home/WaitersWorkingToday.cshtml", waitersWorkingToday);
        }

        [HttpGet]
        public IActionResult GetPasswordByLogin()
        {
            return View("~/Views/Home/GetPasswordByLogin.cshtml");
        }

        [HttpPost]
        public IActionResult GetPasswordByLogin(string login)
        {
            var employee = _employees.Find(emp => emp.Login == login).FirstOrDefault();

            if (employee != null)
            {
                ViewBag.Password = employee.Password;
            }
            else
            {
                ViewBag.Error = "Працівника з таким логіном не знайдено.";
            }

            return View("~/Views/Home/GetPasswordByLogin.cshtml");
        }
    }
}
