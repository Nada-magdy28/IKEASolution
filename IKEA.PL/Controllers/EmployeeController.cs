using IKEA.BLL.Dto_s.Departments;
using IKEA.BLL.Dto_s.Employees;
using IKEA.BLL.Services.DepartmentServices;
using IKEA.BLL.Services.EmployeeServices;
using IKEA.DAL.Models.Employees;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Services - DI
        private readonly IEmployeeServices employeeService;
        private readonly ILogger<EmployeeController> logger;
        private readonly IWebHostEnvironment environment;
        public EmployeeController(IEmployeeServices employeeService, ILogger<EmployeeController> logger, IWebHostEnvironment environment)
        {
            this.employeeService = employeeService;
            this.logger = logger;
            this.environment = environment;
        }
        #endregion
        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var Employees = employeeService.GetAllEmployees();
            return View(Employees);
        }
        #endregion
        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            var employee =employeeService.GetEmployeeById(id.Value);
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
        public IActionResult Create(CreateEmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeDto);
            }
            var Massage = string.Empty;


            try
            {
                var result = employeeService.CreatedEmployee(employeeDto);
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
            return View(employeeDto);

        }
        #endregion
        #region Update
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var Employee = employeeService.GetEmployeeById(id.Value);
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

            };
            return View(MappedEmployee);
        }
        [HttpPost]
        public IActionResult Edit(UpdateEmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeDto);
            }
            var Massage = string.Empty;
            try
            {
                var result = employeeService.UpdateEmployee(employeeDto);
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
        public IActionResult Delete(int? id)
        {
            var Employee = employeeService.GetEmployeeById(id.Value);
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
        public IActionResult Delete(int empId)
        {
            var Massage = string.Empty;
            try
            {
                var IsDeleted = employeeService.DeleteEmployee(empId);
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