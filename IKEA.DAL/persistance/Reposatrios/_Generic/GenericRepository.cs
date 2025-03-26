using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace IKEA.DAL.persistance.Reposatrios._Generic
{
    public class GenericRepository<T>: IGenericRepository<T> where T :ModelBase
    {
        private readonly ApplicationDbContext dbContext;
        public GenericRepository(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public IQueryable<T> GetALL(bool WithNoTarcking = true)
        {
            if (WithNoTarcking)
                return dbContext.Set<T>().AsNoTracking();

            return dbContext.Set<T>();
        }

        public T? GetById(int id)
        {
            var item = dbContext.Set<T>().Find(id);
            //var Department = dbContext.Departments.Local.FirstOrDefault(x => x.Id == id);
            //if (Department == null)
            //    Department = dbContext.Departments.FirstOrDefault(x => x.Id == id);
            return item;
        }
        public int Add(T item)
        {
            dbContext.Set<T>().Add(item);
            return dbContext.SaveChanges();
        }
        public int Update(T item)
        {
            dbContext.Set<T>().Update(item);
            return dbContext.SaveChanges();
        }

        public int Delete(T item)
        {
            item.IsDeleted = true;
            dbContext.Set<T>().Update(item);
            return dbContext.SaveChanges();
        }
    }
}
