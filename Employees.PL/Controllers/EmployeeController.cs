using Employees.BLL.Mapper;
using Employees.BLL.Models.EmployeeVM;
using Employees.DAL.DataBase;
using Employees.DAL.Entities;
using Employees.PL.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Authorization;

namespace SecondDayMVC.Controllers
{
    [Authorize]
   
    public class EmployeeController : Controller
    {
        private const string EmployeeSessionKey = "SelectedEmployee";

        // Field
        private readonly EmployeesDbContext _db;

        // Injected so we always get the real wwwroot folder
        private readonly IWebHostEnvironment _env;

        public EmployeeController(EmployeesDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        // =========================
        // Dashboard
        // =========================
        // =========================
        // Dashboard
        // =========================
        public IActionResult Dashboard()
        {
            var employees = _db.Employees.ToList();

            var totalEmployees = employees.Count;

            var employeesWithImages = employees
                .Count(emp => !string.IsNullOrEmpty(emp.PathImage));

            var employeesWithoutImages =
                totalEmployees - employeesWithImages;

            var averageAge = employees.Count == 0
                ? 0
                : Math.Round(
                    employees.Average(emp => emp.Age),
                    1
                );

            var recentEmployees = employees
                .OrderByDescending(emp => emp.Id)
                .Take(5)
                .Select(emp => EmployeeMapper.MapToGetAllEmployeeVM(emp))
                .ToList();

            ViewBag.TotalEmployees = totalEmployees;

            ViewBag.AverageAge = averageAge;

            ViewBag.EmployeesWithImages = employeesWithImages;

            ViewBag.EmployeesWithoutImages =
                employeesWithoutImages;

            ViewBag.RecentEmployees = recentEmployees;

            return View();
        }
        // =========================
        // Get All Employees
        // =========================
        public IActionResult GetAll()
        {
            //Get All Employees from Database 
            var employees = _db.Employees.ToList();

            var result = employees
                .Select(a => EmployeeMapper.MapToGetAllEmployeeVM(a))
                .ToList();

            //Send the result to the View
            ViewBag.Employees = result;

            return View(result);
        }


        // =========================
        // Create Employee
        // =========================

        // First Action --> Show the form
        public IActionResult Create()
        {
            return View();
        }


        // Second Action --> Save Employee in Database
        public IActionResult SaveData(CreateEmployeeVM emp)
        {
            if (ModelState.IsValid)
            {
                string Image = string.Empty;

                // Upload Image
                if (emp.Image != null && emp.Image.Length > 0)
                {
                    Image = Upload.UploadFile(
                        _env.WebRootPath,
                        "Files",
                        emp.Image
                    ) ?? string.Empty;
                }

                // Custom Validation
                // Check if Name is null or empty
                if (string.IsNullOrEmpty(emp.Name))
                {
                    ViewBag.ErrorMessage = "Name is required !!";
                    return View("Create", emp);
                }

                // Check Age
                if (emp.Age == 0 || emp.Age < 0)
                {
                    ViewBag.ErrorMessage = "Age is required";
                    return View("Create", emp);
                }

                // Mapping ViewModel --> Entity
                var mapp = new Employee()
                {
                    Name = emp.Name,
                    Age = emp.Age,
                    PathImage = Image
                };

                // Add Employee
                _db.Employees.Add(mapp);

                // Save Changes in Database
                _db.SaveChanges();

                // Redirect after successful creation
                return RedirectToAction("GetAll", "Employee");
            }

            return View("Create", emp);
        }


        // =========================
        // Edit Employee
        // =========================

        // First Action --> Get Employee from Database
        public IActionResult Edit(int id)
        {
            // Search about Employee
            var user = _db.Employees
                         .Where(emp => emp.Id == id)
                         .FirstOrDefault();

            // Employee not found
            if (user is null)
            {
                ViewBag.HomeMessage = "User Not Found in DB 🤨";
                return View();
            }

            // Send Employee to Edit View
            return View(user);
        }


        // Second Action --> Save Edited Employee
        public IActionResult SaveEditData(CreateEmployeeVM newemp)
        {
            // Search for old employee
            var oldEmployee = _db.Employees
                                .Where(emp => emp.Id == newemp.Id)
                                .FirstOrDefault();

            // Employee not found
            if (oldEmployee is null)
            {
                ViewBag.HomeMessage = "User Not Found in DB 🤨";
                return View(newemp);
            }

            // Update Employee Data
            oldEmployee.Name = newemp.Name;
            oldEmployee.Age = newemp.Age;

            // Save Changes
            _db.SaveChanges();

            // Return to Employees List
            return RedirectToAction("GetAll", "Employee");
        }


        // =========================
        // Download Employee Image
        // =========================
        public IActionResult DownloadImage(int id)
        {
            // Search for Employee
            var employee = _db.Employees
                             .Where(emp => emp.Id == id)
                             .FirstOrDefault();

            // Employee or Image not found
            if (employee is null || string.IsNullOrEmpty(employee.PathImage))
            {
                return NotFound();
            }

            // Get image physical path
            string filePath = Path.Combine(
                _env.WebRootPath,
                "Files",
                employee.PathImage
            );

            // Check if physical file exists
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            // Detect file content type
            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(
                filePath,
                out string contentType))
            {
                contentType = "application/octet-stream";
            }

            // Get original file extension
            string extension = Path.GetExtension(
                employee.PathImage
            );

            // Give downloaded file the employee's name
            string downloadFileName =
                employee.Name + extension;

            // Return file for download
            return PhysicalFile(
                filePath,
                contentType,
                downloadFileName
            );
        }


        // =========================
        // Delete Employee
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            // Search for Employee
            var employee = _db.Employees
                             .Where(emp => emp.Id == id)
                             .FirstOrDefault();

            // Employee not found
            if (employee is null)
            {
                return RedirectToAction(
                    "GetAll",
                    "Employee"
                );
            }

            // Delete Employee Image
            if (!string.IsNullOrEmpty(employee.PathImage))
            {
                Upload.RemoveFile(
                    _env.WebRootPath,
                    "Files",
                    employee.PathImage
                );
            }

            // Delete Employee from Database
            _db.Employees.Remove(employee);

            // Save Changes
            _db.SaveChanges();

            // Return to Employees List
            return RedirectToAction(
                "GetAll",
                "Employee"
            );
        }


        // =========================
        // Session
        // =========================
        public IActionResult SetEmployeeSession(int id)
        {
            var employee = _db.Employees
                             .Where(emp => emp.Id == id)
                             .FirstOrDefault();

            if (employee is null)
            {
                return NotFound();
            }

            var vm = EmployeeMapper.MapToGetAllEmployeeVM(employee);

            HttpContext.Session.SetObject(EmployeeSessionKey, vm);

            return RedirectToAction("GetEmployeeSession");
        }

        public IActionResult GetEmployeeSession()
        {
            var vm = HttpContext.Session.GetObject<GetAllEmployeeVM>(
                EmployeeSessionKey
            );

            if (vm is null)
            {
                ViewBag.SessionMessage = "No Employee currently stored in Session.";
            }

            return View("SessionEmployee", vm);
        }
    }
}