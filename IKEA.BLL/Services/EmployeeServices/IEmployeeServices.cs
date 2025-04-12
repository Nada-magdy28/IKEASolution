using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.Dto_s.Departments;
using IKEA.BLL.Dto_s.Employees;

namespace IKEA.BLL.Services.EmployeeServices
{
    public interface IEmployeeServices
    {
       Task<IEnumerable<EmployeeDto>> GetAllEmployees(string? search);
       Task<EmployeeDetailsDto>? GetEmployeeById(int id);
       Task<int> CreatedEmployee(CreateEmployeeDto employeeDto);
       Task<int> UpdateEmployee(UpdateEmployeeDto employeeDto);
        Task<bool> DeleteEmployee(int id);
    }
}
