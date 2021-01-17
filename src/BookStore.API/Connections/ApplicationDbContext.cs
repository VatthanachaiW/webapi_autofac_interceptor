using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Connections
{
  public class ApplicationDbContext : DbContext, IApplicationDbContext
  {
    public async Task<bool> SaveChangeAsync() => await this.SaveChangesAsync() > 0;
  }
}