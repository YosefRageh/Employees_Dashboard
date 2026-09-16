using Employees.DAL.DataBase;
using Employees.DAL.Entities;
using Employees.DAL.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace Employees.DAL.Repository.Implementations
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly EmployeesDbContext db;

        public EmployeeRepo()
        {
        }

        public EmployeeRepo(EmployeesDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

            public void Create(Employee emp)
        {
            try
            {                
                var result = db.Employees.Add(emp);
                db.SaveChanges();
                if(result.Entity.Id>0)
                   Console.WriteLine("Employee added successfully");
                else
                    throw new Exception("Employee not added");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Employee> GetAll(Expression<Func<Employee, bool>> filter)
        {
            var result = db.Employees.Where(filter).ToList();
            return result;
        }

        public Employee GetbyId(int id)
        {
            var result = db.Employees.Find(id);
            return result;
        }

        public void Update(Employee newEmployee)
        {
            var existingEmployee = db.Employees.Find(newEmployee.Id);
            if (existingEmployee != null)
            {
                db.Employees.Update(newEmployee);
                db.SaveChanges();
            }
        }
    }
}
