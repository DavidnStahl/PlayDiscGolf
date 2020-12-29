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

        public IQueryable<T> Include(params Expression<Func<T, object>>[] includeExpressions)
        {
            var dbSet = _context.Set<T>();

            IQueryable<T> query = null;
            foreach (var includeExpression in includeExpressions)
            {
                query = dbSet.Include(includeExpression);
            }

            return query ?? dbSet;
        }

        public virtual List<T> GetAll()
        {

            IQueryable<T> query = _context.Set<T>();
            return query.ToList();
        }

        public List<T> FindBy(Expression<Func<T, bool>> predicate)
        {

            IQueryable<T> query = _context.Set<T>().Where(predicate);
            return query.ToList();
        }
        public virtual void Attach(T entity)
        {
            _context.Set<T>().Attach(entity);
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

        public virtual bool SaveChanges(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Set<T>().Attach(entity);
            }
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }
        public virtual T FindById(int id)
        {
            return _context.Set<T>().Find(id);
        }

    }
}
