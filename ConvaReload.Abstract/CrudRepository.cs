using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ConvaReload.Abstract;

public abstract class CrudRepository<T> : IRepository<T> where T : class
{
    public abstract Task<T> GetByIdAsync(int id);
    public abstract Task<IEnumerable<T>> GetAllAsync();
    public abstract Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> exp);
    public abstract Task<T> AddAsync(T entity);
    public abstract Task<T> UpdateAsync(T entity);
    public abstract Task<T> RemoveAsync(T entity);
    public abstract Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
    public abstract Task<IEnumerable<T>> RemoveRangeAsync(IEnumerable<T> entities);
    public abstract Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities);

    public virtual T GetById(int id) => GetByIdAsync(id).GetAwaiter().GetResult();
    public virtual IEnumerable<T> GetAll() => GetAllAsync().GetAwaiter().GetResult();
    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> exp) => FindAsync(exp).GetAwaiter().GetResult();
    public virtual T Add(T entity) => AddAsync(entity).GetAwaiter().GetResult();
    public virtual T Update(T entity) => UpdateAsync(entity).GetAwaiter().GetResult();
    public virtual T Remove(T entity) => RemoveAsync(entity).GetAwaiter().GetResult();
    public virtual IEnumerable<T> AddRange(IEnumerable<T> entities) => AddRangeAsync(entities).GetAwaiter().GetResult();
    public virtual IEnumerable<T> RemoveRange(IEnumerable<T> entities) => RemoveRangeAsync(entities).GetAwaiter().GetResult();
    public virtual IEnumerable<T> UpdateRange(IEnumerable<T> entities) => UpdateRangeAsync(entities).GetAwaiter().GetResult();
}