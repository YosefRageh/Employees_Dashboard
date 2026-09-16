using Employees.DAL.DataBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employees.PL.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly EmployeesDbContext _db;

        public DashboardController(EmployeesDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var employees = _db.Employees.ToList();

            ViewBag.TotalEmployees = employees.Count;

            ViewBag.AverageAge =
                employees.Count == 0
                    ? 0
                    : Math.Round(employees.Average(x => x.Age), 1);

            ViewBag.WithImages =
                employees.Count(x =>
                    !string.IsNullOrWhiteSpace(x.PathImage));

            ViewBag.WithoutImages =
                employees.Count(x =>
                    string.IsNullOrWhiteSpace(x.PathImage));

            ViewBag.RecentEmployees =
                employees
                    .OrderByDescending(x => x.Id)
                    .Take(5)
                    .ToList();

            return View();
        }
    }
}