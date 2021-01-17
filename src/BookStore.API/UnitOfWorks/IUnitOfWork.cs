using System;
using System.Threading.Tasks;
using BookStore.API.Repositories;

namespace BookStore.API.UnitOfWorks
{
  public interface IUnitOfWork : IDisposable
  {
    ICategoryRepository CategoryRepository { get; }
    IBookRepository BookRepository { get; }
    Task<bool> CommitAsync();
  }
}