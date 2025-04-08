using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.persistance.Reposatrios.Departments;
using IKEA.DAL.persistance.Reposatrios.Employees;

namespace IKEA.DAL.persistance.UnitOfWork
{
    public interface IUnitOfWork// : IDisposable
    {
        public IDepartmentRepository DepartmentRepository { get;}
        public IEmployeeRepository EmployeeRepository { get;}
        int Complete();
       // void Dispose();
    }
}
