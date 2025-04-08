using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.Dto_s.Departments;
using IKEA.BLL.Services.DepartmentServices;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.persistance.Reposatrios.Departments;
using IKEA.DAL.persistance.UnitOfWork;

namespace IKEA.BLL.Services.DepartmentServices
{
     public class DepartmentServices : IDepartmentServices
    {
       
        private readonly IUnitOfWork unitOfWork;

        public DepartmentServices(IUnitOfWork unitOfWork)
        {
            
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var Departments =unitOfWork.DepartmentRepository.GetALL().Where(D=>!D.IsDeleted).Select(dept =>new  DepartmentDto() 
            {
                Id = dept.Id,
                Name = dept.Name,
                Code = dept.Code,
                CreationDate = dept.CreationDate,
            }).ToList();
            return Departments;

            //List<DepartmentDto> departmentDtos = new List<DepartmentDto>();
            //foreach (var dept in Departments)
            //{
            //    DepartmentDto departmentDto = new DepartmentDto
            //    {
            //        Id = dept.Id,
            //        Name = dept.Name,
            //        Code = dept.Code,
            //        CreationDate = dept.CreationDate
            //    };
            //    departmentDtos.Add(departmentDto);
            //}

        }
        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var Department = unitOfWork.DepartmentRepository.GetById(id);
            if (Department is not null)
                return new DepartmentDetailsDto()
                {
                    Id = Department.Id,
                    Name = Department.Name,
                    Code = Department.Code,
                    Description = Department.Description,
                    CreationDate = Department.CreationDate,
                    CreatedBy = Department.CreatedBy,
                    CreatedOn = Department.CreatedOn,
                    LastModifiedBy = Department.LastModifiedBy,
                    LastModifiedOn = Department.LastModifiedOn,
                    IsDeleted = Department.IsDeleted,

                };
            return null;
        }

        public int CreatedDepartment(CreatedDepartmentDto department)
        {
           var CreatedDepartment = new Department()
           {
               Name = department.Name,
               Code = department.Code,
               Description = department.Description,
               CreationDate = department.CreationDate,
               CreatedBy =1,
               CreatedOn = DateTime.Now,
               LastModifiedBy = 1,
               LastModifiedOn = DateTime.Now,

           };
             unitOfWork.DepartmentRepository.Add(CreatedDepartment);
            return unitOfWork.Complete();
        }
        public int UpdateDepartment(UpdatedDepartmentDto department)
        {
           var UpdatedDepartment = new Department()
           {
               Id = department.Id,
               Name = department.Name,
               Code = department.Code,
               Description = department.Description,
               CreationDate = department.CreationDate,
               CreatedBy = 1,
               CreatedOn = DateTime.Now,
               LastModifiedBy = 1,
               LastModifiedOn = DateTime.Now,

           };
            unitOfWork.DepartmentRepository.Update(UpdatedDepartment);
            return unitOfWork.Complete();
        }
        public bool DeleteDepartment(int id)
        {
            var Department = unitOfWork.DepartmentRepository.GetById(id);
           // int result = 0;
            if (Department is not null)

                unitOfWork.DepartmentRepository.Delete(Department);
            var result = unitOfWork.Complete();
            if (result > 0)
                return true;



            else
            return false;
        }

       

      
       
    }
}
