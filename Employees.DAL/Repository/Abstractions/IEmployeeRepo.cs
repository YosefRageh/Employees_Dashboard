

using Employees.DAL.Entities;
using System.Linq.Expressions;

namespace Employees.DAL.Repository.Abstractions
{
    public interface IEmployeeRepo
    {
        void Create(Employee emp);
        void Update(Employee newEmployee);
        List<Employee> GetAll(Expression<Func<Employee,bool>> filter);
        Employee GetbyId(int id);
     

    }
}
