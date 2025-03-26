using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models;
using IKEA.DAL.Models.Departments;

namespace IKEA.DAL.persistance.Reposatrios._Generic
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        IQueryable<T> GetALL(bool WithNoTarcking = true);
        T GetById(int id);
        int Add(T entity);
        int Update(T entity);
        int Delete(T entity);
    }
}
