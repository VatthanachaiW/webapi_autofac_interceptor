using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookStore.API.Repositories
{
  public interface IGenericRepository<T> : IDisposable
  {
    IQueryable<T> ToList();
    IQueryable<T> ToList(Expression<Func<T, bool>> predicate);
    Task<List<T>> ToListAsync();
    Task<List<T>> ToListAsync(Expression<Func<T, bool>> predicate);
    Task<T> FirstOfDefaultAsync();
    Task<T> FirstOfDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<bool> AnyAsync();
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    Task Add(T request);
    void Update(T request);
    void Remove(T request);
  }
}