using BookStore.API.Connections;
using BookStore.API.Models;

namespace BookStore.API.Repositories
{
  public class BookRepository : GenericRepository<Book>, IBookRepository
  {
    public BookRepository(IApplicationDbContext dbContext) : base(dbContext)
    {
    }
  }
}