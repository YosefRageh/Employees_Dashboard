using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Employees.BLL.Models.EmployeeVM
{
    public class CreateEmployeeVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "min length character less than 3")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(19, 60, ErrorMessage = "Age must be between 19 and 60")]
        public int Age { get; set; }

        public IFormFile Image { get; set; }
    }
}