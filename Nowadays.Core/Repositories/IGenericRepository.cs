using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nowadays.Core.Repositories;

public interface IGenericRepository<T>
{
    Task<T> GetByIdAsync(object id);

    IQueryable<T> GetAll();
    IQueryable<T> GetAllWithIncludeAll();
    IQueryable<T> GetAllWithInclude(List<Expression<Func<T, object>>> includeProperties);

    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);

    void Update(T entity);

    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);

    IQueryable<T> Where(Expression<Func<T, bool>> expression);
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
    int Count(Expression<Func<T, bool>> expression);
}
