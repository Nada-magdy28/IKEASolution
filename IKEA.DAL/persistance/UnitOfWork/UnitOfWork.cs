using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.persistance.Data;
using IKEA.DAL.persistance.Reposatrios.Departments;
using IKEA.DAL.persistance.Reposatrios.Employees;

namespace IKEA.DAL.persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;

        public IDepartmentRepository DepartmentRepository { get;}
        public IEmployeeRepository EmployeeRepository { get;}
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            DepartmentRepository = new DepartmentRepository(this.dbContext);
            EmployeeRepository = new EmployeeRepository(this.dbContext);
        }
        public async Task<int> Complete()
        {
          return await dbContext.SaveChangesAsync();
        }
        //public void Dispose()
        //{
        //    dbContext.Dispose();
        //}
    }
}
