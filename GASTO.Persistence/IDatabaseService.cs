using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GASTO.Persistence
{
    public interface IDatabaseService<T>
    {
        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        List<T> GetList();

        List<T> GetListWhere(Expression<Func<T, bool>> match);

        T GetById(Expression<Func<T, bool>> match);

        //IEnumerable<T> OrderedListByDateAndSize(Expression<Func<T, DateTime>> match, int size);
        //IEnumerable<T> PaginatedList(Expression<Func<T, DateTime>> match, int page, int size);

        IEnumerable<T> ListById(Expression<Func<T, bool>> match);


    }
}
