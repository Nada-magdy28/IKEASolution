using AutoMapper;
using IKEA.BLL.Dto_s.Departments;
using IKEA.BLL.Services.DepartmentServices;
using IKEA.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private IDepartmentServices departmentServices;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment environment;
        private readonly ILogger<DepartmentController> logger;

        public DepartmentController(IDepartmentServices _departmentServices,IMapper mapper,ILogger<DepartmentController>logger,IWebHostEnvironment environment)
        {
            departmentServices = _departmentServices;
            this.mapper = mapper;
            this.environment = environment;
            this.logger = logger;
        }
        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var Departments = departmentServices.GetAllDepartments();
           // ViewData["Massage"] = " Hello Departments";
             //ViewBag.Massage = "Hello from vb";

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
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentVM departmentVm)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentVm);
            }
            var Massage =string.Empty;
           

            try
            {
                var departmentDto= mapper.Map<DepartmentVM,CreatedDepartmentDto>(departmentVm);
                //var departmentDto = new CreatedDepartmentDto
                //{
                //    Name = departmentVm.Name,
                //    Code = departmentVm.Code,
                //    Description = departmentVm.Description,
                //    CreationDate = departmentVm.CreationDate
                //};
                var result = departmentServices.CreatedDepartment(departmentDto);
                if (result > 0)
                {
                    TempData["Massage"] = $"{departmentDto.Name}Department Created Successfully";
                    return RedirectToAction(nameof(Index));
                }

                else
                {
                    Massage = "Department is not created";
                   
                }
            }
            catch (Exception ex)
            {
               logger.LogError(ex, ex.Message);
                if (environment.IsDevelopment())
                {
                    Massage = ex.Message;
                  
                }
                else
                {
                    Massage = "An Error Effectf at the creation opration";
                   
                }

            }
            ModelState.AddModelError(string.Empty, Massage);
            return View(departmentVm);


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
            var MappedDepartment = mapper.Map<DepartmentDetailsDto, DepartmentVM>(Department);
            //var MappedDepartment = new DepartmentVM()
            //{
            //    Id = Department.Id,
            //    Name = Department.Name,
            //    Code = Department.Code,
            //    Description = Department.Description,
            //    CreationDate = Department.CreationDate
            //};
            return View(MappedDepartment);
        }
        [HttpPost]
        public IActionResult Edit(DepartmentVM departmentVM)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentVM);
            }
            var Massage = string.Empty;
            try
            {
                var departmentDto = mapper.Map<DepartmentVM, UpdatedDepartmentDto>(departmentVM);
                //var departmentDto = new UpdatedDepartmentDto
                //{
                //    Id = departmentVM.Id,
                //    Name = departmentVM.Name,
                //    Code = departmentVM.Code,
                //    Description = departmentVM.Description,
                //    CreationDate = departmentVM.CreationDate
                //};
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
            return View(departmentVM);
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
