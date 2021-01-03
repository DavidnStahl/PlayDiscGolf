using Microsoft.EntityFrameworkCore;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PlayDiscGolf.Data
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class
    {
        private readonly DataBaseContext _context;
        public EntityRepository(DataBaseContext context)
        {
            _context = context;
        }

        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public List<T> FindBy(Expression<Func<T, bool>> predicate)
        {

            IQueryable<T> query = _context.Set<T>().Where(predicate);
            return query.ToList();
        }

        public virtual bool Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return true;
        }

        public virtual bool Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return true;
        }

        public virtual bool Edit(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return true;
        }

        public virtual bool Save()
        {
            _context.SaveChanges();
            return true;
        }

        public virtual T FindById(Guid id)
        {
            return _context.Set<T>().Find(id);
        }
    }
}
