using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookStore.API.Connections;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Repositories
{
  public class GenericRepository<T> : IGenericRepository<T> where T : class
  {
    private bool _disposed;
    protected IApplicationDbContext Context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(IApplicationDbContext dbContext)
    {
      Context = dbContext;
      _dbSet = dbContext.Set<T>();
    }

    public IQueryable<T> ToList()
    {
      return _dbSet;
    }

    public IQueryable<T> ToList(Expression<Func<T, bool>> predicate)
    {
      return _dbSet.Where(predicate);
    }

    public async Task<List<T>> ToListAsync()
    {
      return await _dbSet.ToListAsync();
    }

    public async Task<List<T>> ToListAsync(Expression<Func<T, bool>> predicate)
    {
      return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task<T> FirstOfDefaultAsync()
    {
      return await _dbSet.FirstOrDefaultAsync();
    }

    public async Task<T> FirstOfDefaultAsync(Expression<Func<T, bool>> predicate)
    {
      return await _dbSet.FirstOrDefaultAsync(predicate);
    }

    public async Task<bool> AnyAsync()
    {
      return await _dbSet.AnyAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
      return await _dbSet.AnyAsync(predicate);
    }

    public async Task Add(T request)
    {
      await _dbSet.AddAsync(request);
    }

    public void Update(T request)
    {
      _dbSet.Attach(request);
      Context.Entry(request).State = EntityState.Modified;
    }

    public void Remove(T request)
    {
      if (Context.Entry(request).State == EntityState.Detached)
      {
        _dbSet.Attach(request);
      }

      _dbSet.Remove(request);
    }

    public void Dispose()
    {
      Dispose(true);

      GC.SuppressFinalize(this);
    }


    protected virtual void Dispose(bool disposing)
    {
      if (_disposed)
      {
        return;
      }

      _disposed = true;

      Context.Dispose();
      Context = null;
    }
  }
}