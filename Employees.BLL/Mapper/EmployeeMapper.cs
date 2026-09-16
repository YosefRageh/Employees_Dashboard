using Employees.BLL.Models.EmployeeVM;
using Employees.DAL.Entities;

namespace Employees.BLL.Mapper
{
    public static class EmployeeMapper
    {
        public static GetAllEmployeeVM MapToGetAllEmployeeVM(Employee employee)
        {
            return new GetAllEmployeeVM
            {
                ID = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                PathImage = employee.PathImage
            };
        }
    }
}