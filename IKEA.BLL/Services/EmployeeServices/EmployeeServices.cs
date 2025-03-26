﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.Dto_s.Employees;
using IKEA.DAL.Common.Enums;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.persistance.Reposatrios.Employees;

namespace IKEA.BLL.Services.EmployeeServices
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IEmployeeRepository repository;
        public EmployeeServices(IEmployeeRepository employeeRepository)
        {
            repository = employeeRepository;
        }
        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            var Employees = repository.GetALL();
            var FilterdEmployee = Employees.Where(E => E.IsDeleted == false);
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
            });
            return AfterFilteration.ToList();
        }
        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = repository.GetById(id);
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
                    Gender =employee.Gender.ToString(),
                    EmployeeType = employee.EmployeeType.ToString(),
                    LastModifiedBy = employee.LastModifiedBy,
                    LastModifiedOn = employee.LastModifiedOn,
                    CreatedBy = employee.CreatedBy,
                    CreatedOn = employee.CreatedOn



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
                CreatedBy = 1,
                CreatedOn = DateTime.Now,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,

            };
            return repository.Add(Employee);
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
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,

            };
            return repository.Update(Employee);
        }

        public bool DeleteEmployee(int id)
        {
            var employee = repository.GetById(id);
            // int result = 0;
            if (employee is not null)

                return repository.Delete(employee) > 0;


            else
                return false;
        }

      

      

       
    }
}
