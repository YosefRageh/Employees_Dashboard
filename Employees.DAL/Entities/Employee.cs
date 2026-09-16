using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.DAL.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal? Salary { get; set; }
        public string PathImage { get; set; }
    }
}
