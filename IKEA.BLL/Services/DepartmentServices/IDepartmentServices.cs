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
       Task<IEnumerable<DepartmentDto>> GetAllDepartments();
       Task<DepartmentDetailsDto>? GetDepartmentById(int id);
        Task<int> CreatedDepartment(CreatedDepartmentDto department);
       Task<int> UpdateDepartment(UpdatedDepartmentDto department);
        Task<bool> DeleteDepartment(int id);
    }
}
