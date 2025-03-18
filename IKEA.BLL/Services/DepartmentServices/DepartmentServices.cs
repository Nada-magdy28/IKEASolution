using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.Dto_s.Departments;
using IKEA.BLL.Services.DepartmentServices;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.persistance.Reposatrios.Departments;

namespace IKEA.BLL.Services.DepartmentServices
{
     public class DepartmentServices : IDepartmentServices
    {
        private IDepartmentRepository Repository;
       

        public DepartmentServices(IDepartmentRepository repository)
        {
            Repository = repository;
        }
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var Departments = Repository.GetALL().Select(dept =>new  DepartmentDto() 
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
            var Department = Repository.GetById(id);
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
            return Repository.Add(CreatedDepartment);
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
            return Repository.Update(UpdatedDepartment);
        }
        public bool DeleteDepartment(int id)
        {
            var Department = Repository.GetById(id);
           // int result = 0;
            if (Department is not null)
            
                return Repository.Delete(Department)>0;
           
                
            else
            return false;
        }

       

      
       
    }
}
