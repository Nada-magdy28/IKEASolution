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
        IEnumerable<EmployeeDto> GetAllEmployees(string search);
        EmployeeDetailsDto? GetEmployeeById(int id);
        int CreatedEmployee(CreateEmployeeDto employeeDto);
        int UpdateEmployee(UpdateEmployeeDto employeeDto);
        bool DeleteEmployee(int id);
    }
}
