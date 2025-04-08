using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.Dto_s.Employees;
using IKEA.DAL.Common.Enums;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.persistance.Reposatrios.Employees;
using IKEA.DAL.persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace IKEA.BLL.Services.EmployeeServices
{
    public class EmployeeServices : IEmployeeServices
    {
        
        private readonly IUnitOfWork unitOfWork;

        public EmployeeServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<EmployeeDto> GetAllEmployees(string search)
        {
            var Employees =unitOfWork.EmployeeRepository.GetALL();

            var FilterdEmployee = Employees.Where(E => E.IsDeleted && (string.IsNullOrEmpty(search) ||E.Name.ToLower().Contains(search.ToLower()))).Include(E=>E.Department);
            var AfterFilteration=FilterdEmployee.Select(static E => new EmployeeDto()
            {
                Id = E.Id,
                Name = E.Name,
                Age = E.Age,
                Salary = E.Salary,
                IsActive = E.IsActive,
                Email = E.Email,
                Gender = E.Gender,
                EmployeeType =E.EmployeeType,
                Department = E.Department.Name ?? "N/A",
            });
            return AfterFilteration.ToList();
        }
        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee =unitOfWork.EmployeeRepository.GetById(id);
            if (employee is not null)
            {
                return new EmployeeDetailsDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Salary = employee.Salary,
                    Address = employee.Address,
                    IsActive = employee.IsActive,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    HiringDate = employee.HiringDate,
                    Gender = employee.Gender.ToString(),
                    EmployeeType = employee.EmployeeType.ToString(),
                    LastModifiedBy = employee.LastModifiedBy,
                    LastModifiedOn = employee.LastModifiedOn,
                    CreatedBy = employee.CreatedBy,
                    CreatedOn = employee.CreatedOn,
                    Department = employee.Department.Name ?? "N/A"


                };
            }
            return null;
        }
        public int CreatedEmployee(CreateEmployeeDto employeeDto)
        {
            var Employee = new Employee()
            {
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                Salary = employeeDto.Salary,
                IsActive = employeeDto.IsActive,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender=Enum.Parse<Gender>(employeeDto.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employeeDto.EmployeeType),
                DepartmentId = employeeDto.DepartmentId,
                CreatedBy = 1,
                CreatedOn = DateTime.Now,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,

            };
            unitOfWork.EmployeeRepository.Add(Employee);
            return unitOfWork.Complete();
        }
        public int UpdateEmployee(UpdateEmployeeDto employeeDto)
        {
            var Employee = new Employee()
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                Salary = employeeDto.Salary,
                IsActive = employeeDto.IsActive,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = Enum.Parse<Gender>(employeeDto.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employeeDto.EmployeeType),
                DepartmentId = employeeDto.DepartmentId,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,

            };
               unitOfWork.EmployeeRepository.Update(Employee);
            return unitOfWork.Complete();
        }

        public bool DeleteEmployee(int id)
        {
            var employee =unitOfWork.EmployeeRepository.GetById(id);
            // int result = 0;
            if (employee is not null)

                unitOfWork.EmployeeRepository.Delete(employee) ;
            var result = unitOfWork.Complete();
            if (result > 0)
                return true;


            else
                return false;
        }

      

      

       
    }
}
