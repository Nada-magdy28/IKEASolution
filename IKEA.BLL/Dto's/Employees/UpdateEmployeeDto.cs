using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Dto_s.Employees
{
    public class UpdateEmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal? Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly? HiringDate { get; set; }
        public string Gender { get; set; } = null!;
        public string EmployeeType { get; set; } = null!;
        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
    }
}
