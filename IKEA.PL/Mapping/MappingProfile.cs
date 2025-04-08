using System.CodeDom;
using AutoMapper;
using IKEA.BLL.Dto_s.Departments;
using IKEA.DAL.persistance.Data.Migrations;
using IKEA.PL.Models;

namespace IKEA.PL.Mapping
{
    public class MappingProfile :Profile
    {
       public MappingProfile()
        {
            CreateMap<DepartmentVM, CreatedDepartmentDto>().ReverseMap();
            // .ForMember(dest => dest.Name, config => config.MapFrom(src => src.Name))
            CreateMap<DepartmentDetailsDto ,DepartmentVM>().ReverseMap();
            CreateMap<DepartmentVM,UpdatedDepartmentDto>().ReverseMap();
        }
    }
}
