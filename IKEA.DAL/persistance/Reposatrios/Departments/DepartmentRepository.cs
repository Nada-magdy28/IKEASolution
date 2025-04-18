﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Departments;
using Microsoft.EntityFrameworkCore;
using IKEA.DAL.persistance.Data;
using IKEA.DAL.persistance.Reposatrios.Departments;
using IKEA.DAL.persistance.Reposatrios._Generic;

namespace IKEA.DAL.persistance.Reposatrios.Departments
{
    public class DepartmentRepository :GenericRepository<Department>,IDepartmentRepository
    {
        private readonly ApplicationDbContext dbContext;
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
            dbContext = context;
        }
    }
}
