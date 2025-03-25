using IKEA.BLL.Dto_s.Departments;
using IKEA.BLL.Dto_s.Employees;
using IKEA.BLL.Services.DepartmentServices;
using IKEA.BLL.Services.EmployeeServices;
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
    }
}