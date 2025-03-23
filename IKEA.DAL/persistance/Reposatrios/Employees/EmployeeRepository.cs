using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.persistance.Data;
using IKEA.DAL.persistance.Reposatrios._Generic;
using Microsoft.EntityFrameworkCore;

namespace IKEA.DAL.persistance.Reposatrios.Employees
{
    public class EmployeeRepository : GenericRepository<Employee>,IEmployeeRepository
    {
        private readonly ApplicationDbContext dbContext;
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
            dbContext = context;
        }

      
    }
}
