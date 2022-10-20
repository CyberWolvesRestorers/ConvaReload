using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ConvaReload.Abstract;

public interface IRepository<T> where T : class
{
    T GetById(int id);
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Expression<Func<T, bool>> exp);
    T Add(T entity);
    T Update(T entity);
    T Remove(T entity);
    IEnumerable<T> AddRange(IEnumerable<T> entities);
    IEnumerable<T> RemoveRange(IEnumerable<T> entities);
    IEnumerable<T> UpdateRange(IEnumerable<T> entities);
}