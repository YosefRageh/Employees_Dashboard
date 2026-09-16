using Employees.BLL.Models.EmployeeVM;

namespace Employees.BLL.Services.Abstractions
{
    public interface IEmployeeServices
    {
        bool CreateEmployee(CreateEmployeeVM employeeVM);
    }
}