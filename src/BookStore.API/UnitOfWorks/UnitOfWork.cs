using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using BookStore.API.Connections;
using BookStore.API.Repositories;

namespace BookStore.API.UnitOfWorks
{
  public class UnitOfWork : IUnitOfWork
  {
    private bool _disposed;
    private ICategoryRepository _categoryRepository;
    private IBookRepository _bookRepository;

    public UnitOfWork(IApplicationDbContext context)
    {
      Context = context;
    }

    protected virtual void Dispose(bool disposing)
    {
      if (_disposed)
      {
        Context.Dispose();
      }

      _disposed = true;
    }

    [ExcludeFromCodeCoverage]
    public virtual void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    private IApplicationDbContext Context { get; }

    public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(Context);

    public IBookRepository BookRepository => _bookRepository ??= new BookRepository(Context);

    public async Task<bool> CommitAsync() => await Context.SaveChangeAsync();
  }
}