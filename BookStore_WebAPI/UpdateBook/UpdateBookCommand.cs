using BookStore_WebAPI.DbOperations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookStore_WebAPI.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStore_DBContext _dbContext;
        public int BookId { get; set; }
        public UpdateBookModel Model { get; set; }
        public UpdateBookCommand(BookStore_DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {

            var book = _dbContext.Books.SingleOrDefault(book => book.Id == BookId);

            if (book is null)
                throw new InvalidOperationException("Güncellenecek kitap bulunamadı");

            book.GenreID = Model.GenreId != default ? Model.GenreId : book.GenreID;
            book.Title = Model.Title != default ? Model.Title : book.Title;
           
            _dbContext.SaveChanges();

        }
    }
    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
    }
}
