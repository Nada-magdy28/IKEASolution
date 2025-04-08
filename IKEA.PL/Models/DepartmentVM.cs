using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.Models
{
    public class DepartmentVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required !!")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Code is Required !!")]
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        [Display(Name = "Date of CreationDate")]
        public DateOnly CreationDate { get; set; }
    }
}
