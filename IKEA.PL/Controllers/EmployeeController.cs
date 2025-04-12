using IKEA.BLL.Dto_s.Departments;
using IKEA.BLL.Dto_s.Employees;
using IKEA.BLL.Services.DepartmentServices;
using IKEA.BLL.Services.EmployeeServices;
using IKEA.DAL.Models.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IKEA.PL.Controllers
{
    
    public class EmployeeController : Controller
    {
        #region Services - DI
        private readonly IEmployeeServices employeeService;
        private readonly ILogger<EmployeeController> logger;
        private readonly IWebHostEnvironment environment;
        public EmployeeController(IEmployeeServices employeeService,IDepartmentServices departmentServices, ILogger<EmployeeController> logger, IWebHostEnvironment environment)
        {
            this.employeeService = employeeService;
            this.logger = logger;
            this.environment = environment;
        }
        #endregion
        #region Index
        [HttpGet]
        public async Task<IActionResult> Index(string? search)
        {
            var Employees =await employeeService.GetAllEmployees(search);
            //ViewData["Title"] = " Hello Employees";
            return View(Employees);
        }
        #endregion
        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            var employee =await employeeService.GetEmployeeById(id.Value);
            if (id is null)
            {
                return BadRequest();
            }
            if (employee is null)
            {
                return NotFound();
            }
            return View(employee);
        }


        #endregion
        #region Create
        [HttpGet]
        public IActionResult Create()
        {
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto employeeVM)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeVM);
            }
            var Massage = string.Empty;


            try
            {
                var employeeDto = new CreateEmployeeDto()
                {
                    Name = employeeVM.Name,
                    Age = employeeVM.Age,
                    Address = employeeVM.Address,
                    Salary = employeeVM.Salary,
                    IsActive = employeeVM.IsActive,
                    Email = employeeVM.Email,
                    PhoneNumber = employeeVM.PhoneNumber,
                    Gender = employeeVM.Gender,
                    EmployeeType = employeeVM.EmployeeType,
                    HiringDate = employeeVM.HiringDate,
                    DepartmentId = employeeVM.DepartmentId,
                    Image = employeeVM.Image
                };
                var result = await employeeService.CreatedEmployee(employeeDto);
                if (result > 0)
                    return RedirectToAction(nameof(Index));

                else
                
                    Massage = "Department is not created";
                
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                if (environment.IsDevelopment())
                    Massage = ex.Message;

                else
                    Massage = "An Error Effectf at the creation opration";

            }
            ModelState.AddModelError(string.Empty, Massage);
            return View(employeeVM);

        }
        #endregion
        #region Update
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var Employee =await employeeService.GetEmployeeById(id.Value);
            if (id is null)
            {
                return BadRequest();
            }
            if (Employee is null)
            {
                return NotFound();
            }
            var MappedEmployee = new UpdateEmployeeDto
            {
                Id = Employee.Id,
                Name = Employee.Name,
                Age = Employee.Age,
                Address = Employee.Address,
                HiringDate = Employee.HiringDate,
                Email = Employee.Email,
                PhoneNumber = Employee.PhoneNumber,
                Salary = Employee.Salary,
                IsActive = Employee.IsActive,
                Gender = Employee.Gender,
                EmployeeType = Employee.EmployeeType,
                ImageName = Employee.ImageName,

            };
            return View(MappedEmployee);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateEmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeDto);
            }
            var Massage = string.Empty;
            try
            {
                var result =await employeeService.UpdateEmployee(employeeDto);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Massage = "Employee is not Updated";
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                Massage = environment.IsDevelopment() ? ex.Message : "An Error Effectf at the Update opration";



            }
            
            ModelState.AddModelError(string.Empty, Massage);
            return View(employeeDto);
        }
        #endregion
        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var Employee = await employeeService.GetEmployeeById(id.Value);
            if (id is null)
            {
                return BadRequest();
            }
            if (Employee is null)
            {
                return NotFound();
            }
            return View(Employee);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int empId)
        {
            var Massage = string.Empty;
            try
            {
                var IsDeleted =await employeeService.DeleteEmployee(empId);
                if (IsDeleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Massage = "Employee is not Deleted";
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                Massage = environment.IsDevelopment() ? ex.Message : "An Error Effectf at the Delete opration";
            }
            ModelState.AddModelError(string.Empty, Massage);
            return RedirectToAction(nameof(Delete), new { id = empId });
        }
        #endregion
    }
}