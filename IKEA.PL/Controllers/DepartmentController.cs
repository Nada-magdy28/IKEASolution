using IKEA.BLL.Dto_s.Departments;
using IKEA.BLL.Services.DepartmentServices;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private IDepartmentServices departmentServices;
        private readonly IWebHostEnvironment environment;
        private readonly ILogger<DepartmentController> logger;

        public DepartmentController(IDepartmentServices _departmentServices,ILogger<DepartmentController>logger,IWebHostEnvironment environment)
        {
            departmentServices = _departmentServices;
            this.environment = environment;
            this.logger = logger;
        }
        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var Departments = departmentServices.GetAllDepartments();
            return View(Departments);
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            var Department = departmentServices.GetDepartmentById(id.Value);
            if (id is null )
            {
                return BadRequest();
            }
           if (Department is null)
            {
                return NotFound();
            }
            return View(Department);
        }


        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentDto);
            }
            var Massage =string.Empty;
           

            try
            {
                var result = departmentServices.CreatedDepartment(departmentDto);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

                else
                {
                    Massage = "Department is not created";
                    ModelState.AddModelError(string.Empty, Massage);
                    return View(departmentDto);
                }
            }
            catch (Exception ex)
            {
               logger.LogError(ex, ex.Message);
                if (environment.IsDevelopment())
                {
                    Massage = ex.Message;
                    ModelState.AddModelError(string.Empty, Massage);
                    return View(departmentDto);
                }
                else
                {
                    Massage = "An Error Effectf at the creation opration";
                    ModelState.AddModelError(string.Empty, Massage);
                    return View(departmentDto);
                }

            }
            
           
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var Department = departmentServices.GetDepartmentById(id.Value);
            if (id is null)
            {
                return BadRequest();
            }
            if (Department is null)
            {
                return NotFound();
            }
            var MappedDepartment = new UpdatedDepartmentDto
            {
                Id = Department.Id,
                Name = Department.Name,
                Code = Department.Code,
                Description = Department.Description,
                CreationDate = Department.CreationDate
            };
            return View(MappedDepartment);
        }
        [HttpPost]
        public IActionResult Edit(UpdatedDepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentDto);
            }
            var Massage = string.Empty;
            try
            {
                var result = departmentServices.UpdateDepartment(departmentDto);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Massage = "Department is not Updated";
                }
            }
            catch (Exception ex)
            {
               logger.LogError(ex, ex.Message);
                Massage=environment.IsDevelopment() ? ex.Message : "An Error Effectf at the Update opration";
               


            }
            ModelState.AddModelError(string.Empty, Massage);
            return View(departmentDto);
        }
        #endregion
        #region Delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var Department = departmentServices.GetDepartmentById(id.Value);
            if (id is null)
            {
                return BadRequest();
            }
            if (Department is null)
            {
                return NotFound();
            }
            return View(Department);
        }
        [HttpPost]
        public IActionResult Delete(int DeptId)
        {
            var Massage = string.Empty;
            try
            {
                var IsDeleted = departmentServices.DeleteDepartment(DeptId);
                if (IsDeleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Massage = "Department is not Deleted";
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                Massage = environment.IsDevelopment() ? ex.Message : "An Error Effectf at the Delete opration";
            }
            ModelState.AddModelError(string.Empty, Massage);
            return RedirectToAction(nameof(Delete), new {id= DeptId });
        }
        #endregion
    }
}
