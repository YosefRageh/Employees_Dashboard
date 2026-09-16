using Employees.BLL.Models.EmployeeVM;
using Employees.BLL.Services.Abstractions;
using Employees.DAL.Entities;
using Employees.DAL.Repository.Abstractions;
using Employees.DAL.Repository.Implementations;

namespace Employees.BLL.Services.Implementations
{
    internal class EmployeeServices : IEmployeeServices
    {
        IEmployeeRepo employeerepo = new EmployeeRepo();

        public bool CreateEmployee(CreateEmployeeVM employeeVM)
        {
            //validation logic for creating an employee
            if (employeeVM == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(employeeVM.Name))
            {
                return false;
            }

            if (employeeVM.Age < 19 || employeeVM.Age > 60)
            {
                return false;
            }

            //upload image
            string ImageName = null;

            if (employeeVM.Image != null)
            {
                string FolderPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Files"
                );

                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }

                ImageName = Guid.NewGuid() + Path.GetFileName(employeeVM.Image.FileName);

                string FinalPath = Path.Combine(FolderPath, ImageName);

                using (var Stream = new FileStream(FinalPath, FileMode.Create))
                {
                    employeeVM.Image.CopyTo(Stream);
                }
            }

            //Custom Mapp
            var employee = new Employee()
            {
                Id = employeeVM.Id,
                Name = employeeVM.Name,
                Age = employeeVM.Age,
                PathImage = ImageName
            };

            //save to database
            employeerepo.Create(employee);

            return true;
        }
    }
}