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

        public async Task<T?> GetById(int id)
        {
            var item =await dbContext.Set<T>().FindAsync(id);
            //var Department = dbContext.Departments.Local.FirstOrDefault(x => x.Id == id);
            //if (Department == null)
            //    Department = dbContext.Departments.FirstOrDefault(x => x.Id == id);
            return item;
        }
        public void Add(T item)
        {
            dbContext.Set<T>().Add(item);
            
        }
        public void Update(T item)
        {
            dbContext.Set<T>().Update(item);
            
        }

        public void Delete(T item)
        {
            item.IsDeleted = true;
            dbContext.Set<T>().Update(item);
            
        }
    }
}
