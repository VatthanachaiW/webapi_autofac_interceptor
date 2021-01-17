using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BookStore.API.Connections
{
  public interface IApplicationDbContext : IDisposable
  {
    DatabaseFacade Database { get; }
    DbSet<T> Set<T>() where T : class;
    EntityEntry Entry(object entity);
    ChangeTracker ChangeTracker { get; }
    Task<bool> SaveChangeAsync();
  }
}