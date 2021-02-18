using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PlayDiscGolf.Infrastructure.Repository.Generic
{
    public interface IEntityRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        List<T> FindAllBy(Expression<Func<T, bool>> predicate);
        T FindSingleBy(Expression<Func<T, bool>> predicate);
        bool Add(T entity);
        bool AddRange(List<T> entities);
        bool Delete(T entity);
        bool Edit(T entity);
        bool Save();
        T FindById(Guid id);
    }
}
