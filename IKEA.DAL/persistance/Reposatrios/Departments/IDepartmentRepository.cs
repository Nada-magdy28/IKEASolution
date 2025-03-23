﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.persistance.Reposatrios._Generic;

namespace IKEA.DAL.persistance.Reposatrios.Departments
{
     public interface IDepartmentRepository : IGenericRepository<Department>
    {
       
    }
}
