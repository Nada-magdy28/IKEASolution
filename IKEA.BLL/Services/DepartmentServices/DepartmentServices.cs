using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.DepartmentServices
{
     public class DepartmentServices : IDepartmentServices
    {
        private DepartmentRepository Repository;
        public DepartmentServices(DepartmentRepository repository)
        {
            Repository = repository;
        }
    }
}
