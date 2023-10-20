using Microsoft.EntityFrameworkCore;
using Nowadays.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nowadays.Repository.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly NowadaysContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(NowadaysContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.AnyAsync(expression);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public IQueryable<T> GetAll()
    {
        return _dbSet.AsNoTracking().AsQueryable();
    }

    public async Task<T> GetByIdAsync(object id)
    {
        return await _dbSet.FindAsync(id);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> expression)
    {
        return _dbSet.Where(expression);
    }

    public int Count(Expression<Func<T, bool>> expression)
    {
        return _dbSet.Count(expression);
    }

    public IQueryable<T> GetAllWithIncludeAll()
    {
        var queryable = _dbSet.AsNoTracking().AsQueryable();

        var entityType = typeof(T);
        var properties = entityType.GetProperties()
                                   .Where(p => p.PropertyType.IsClass && p.PropertyType != typeof(string));

        foreach (var property in properties)
        {
            queryable = queryable.Include(property.Name);
        }

        return queryable;
    }

    public IQueryable<T> GetAllWithInclude(List<Expression<Func<T, object>>> includeProperties)
    {
        var queryable = _dbSet.AsNoTracking().AsQueryable();

        foreach (var includeProperty in includeProperties)
        {
            queryable = queryable.Include(includeProperty);
        }

        return queryable;
    }
}
