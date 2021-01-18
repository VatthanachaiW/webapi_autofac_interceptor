using System.Threading.Tasks;
using BookStore.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.API.Connections
{
  public class ApplicationDbContext : DbContext, IApplicationDbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public async Task<bool> SaveChangeAsync() => await this.SaveChangesAsync() > 0;

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<BookCategory>(CategoryModelConfig);
      builder.Entity<Book>(BookModelConfig);
      base.OnModelCreating(builder);
    }


    public void CategoryModelConfig(EntityTypeBuilder<BookCategory> builder)
    {
      builder.ToTable("tb_categories");
      builder.HasKey(k => k.Id);
      builder.Property(p => p.Id).ValueGeneratedOnAdd().HasColumnName("category_id");
      builder.Property(p => p.Name).HasColumnName("cagegory_name");
    }

    public void BookModelConfig(EntityTypeBuilder<Book> builder)
    {
      builder.ToTable("tb_books");
      builder.HasKey(k => k.Id);
      builder.Property(p => p.Id).ValueGeneratedOnAdd().HasColumnName("book_id");
      builder.Property(p => p.Name).HasColumnName("book_name");
      builder.Property(p => p.CategoryId).HasColumnName("category_id");
      builder.HasOne(o => o.BookCategory).WithMany(m => m.Books).HasForeignKey(fk => fk.CategoryId).IsRequired(true);
    }
  }
}