using BookStore_WebAPI.DbOperations;
using System;
using System.Linq;

namespace BookStore_WebAPI.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStore_DBContext _dbContext;
        public int BookId { get; set; }

        public DeleteBookCommand(BookStore_DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(book => book.Id == BookId);

            if (book is null)
                throw new InvalidOperationException("Silinecek kitap bulunamadı");

            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}
