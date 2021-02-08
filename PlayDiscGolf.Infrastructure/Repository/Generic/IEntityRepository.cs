using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PlayDiscGolf.Infrastructure.Repository.Generic
{
    public interface IEntityRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        List<T> FindBy(Expression<Func<T, bool>> predicate);
        bool Add(T entity);

        bool AddRange(List<T> entities);
        bool Delete(T entity);
        bool Edit(T entity);
        bool Save();
        T FindById(Guid id);
    }
}
