using BookStore.API.Connections;
using BookStore.API.Models;

namespace BookStore.API.Repositories
{
  public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
  {
    public CategoryRepository(IApplicationDbContext dbContext) : base(dbContext)
    {
    }
  }
}