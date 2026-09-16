    using System.ComponentModel.DataAnnotations;

    namespace Employees.BLL.ModelVM.EmployeeVm
    {
        public class EditEmployeeVM
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "Name is required")]
            [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Age is required")]
            [Range(19, 60, ErrorMessage = "Age must be between 19 and 60")]
            public int Age { get; set; }
        }
    }