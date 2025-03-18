using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.Dto_s.Departments;


namespace IKEA.BLL.Services.DepartmentServices
{
    public interface IDepartmentServices
    {
       IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailsDto? GetDepartmentById(int id);
        int CreatedDepartment(CreatedDepartmentDto department);
        int UpdateDepartment(UpdatedDepartmentDto department);
        bool DeleteDepartment(int id);
    }
}
