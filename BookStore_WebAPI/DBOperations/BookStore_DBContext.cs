using BookStore_WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore_WebAPI.DbOperations
{
    public class BookStore_DBContext : DbContext
    {
        public BookStore_DBContext(DbContextOptions<BookStore_DBContext> options) : base(options)
        { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
