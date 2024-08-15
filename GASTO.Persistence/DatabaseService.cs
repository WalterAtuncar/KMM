using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace GASTO.Persistence
{
    public class DatabaseService<T> : IDatabaseService<T> where T : class
    {
        private DbContextPGDT _context;

        public DatabaseService()
        {

        }

        public DatabaseService(DbContextPGDT context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            //return _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            //return _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            //return _context.SaveChanges();
        }

        public T GetById(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().Single(match);
        }

        public List<T> GetList()
        {
            return _context.Set<T>().ToList();
        }

        public List<T> GetListWhere(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().Where(match).ToList();
        }

        public IEnumerable<T> ListById(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().Where(match);
        }
    }
}
