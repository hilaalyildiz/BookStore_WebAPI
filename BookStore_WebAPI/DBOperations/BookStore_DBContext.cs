using Microsoft.EntityFrameworkCore;

namespace BookStore_WebAPI.DbOperations
{
    public class BookStore_DBContext : DbContext
    {
        public BookStore_DBContext(DbContextOptions<BookStore_DBContext> options) : base(options)
        { }

        public DbSet<Book> Books { get; set; }
    }
}
