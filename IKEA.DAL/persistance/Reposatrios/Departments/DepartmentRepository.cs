using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Departments;

namespace IKEA.DAL.persistance.Reposatrios.Departments
{
    internal class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext dbContext;
        public DepartmentRepository(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public IEnumerable<Department> GetALL(bool WithNoTarcking = true)
        {
            if (WithNoTarcking)
                return dbContext.Departments.AsNoTracking().ToList();

            return dbContext.Departments.ToList();
        }

        public Department? GetById(int id)
        {
            var Department = dbContext.Departments.Find(id);
            //var Department = dbContext.Departments.Local.FirstOrDefault(x => x.Id == id);
            //if (Department == null)
            //    Department = dbContext.Departments.FirstOrDefault(x => x.Id == id);
            return Department;
        }
        public int Add(Department department)
        {
            dbContext.Departments.Add(department);
            return dbContext.SaveChanges();
        }
        public int Update(Department department)
        {
            dbContext.Departments.Update(department);
            return dbContext.SaveChanges();
        }

        public int Delete(Department department)
        {
            dbContext.Departments.Remove(department);
            return dbContext.SaveChanges();
        }
    }
}
