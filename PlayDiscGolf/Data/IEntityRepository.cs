using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PlayDiscGolf.Data
{
    public interface IEntityRepository<T> where T : class
    {
        IQueryable<T> Include(params Expression<Func<T, object>>[] includeExpressions);
        List<T> GetAll();
        List<T> FindBy(Expression<Func<T, bool>> predicate);
        bool Add(T entity);
        bool Delete(T entity);
        bool Edit(T entity);
        bool Save();
        bool SaveChanges(T entity);
        T FindById(int id);
    }
}
