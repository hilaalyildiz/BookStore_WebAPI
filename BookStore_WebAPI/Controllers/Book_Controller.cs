using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using BookStore_WebAPI.DbOperations;
using Microsoft.EntityFrameworkCore;
using BookStore_WebAPI.BookOperations.GetBooks;
using BookStore_WebAPI.BookOperations.CreateBook;
using static BookStore_WebAPI.BookOperations.CreateBook.CreateBookCommand;

namespace BookStore_WebAPI.Controllers
{
        [ApiController]
        [Route("[controller]s")]
        public class Book_Controller : ControllerBase
        {

        private readonly BookStore_DBContext _context;
        public Book_Controller(BookStore_DBContext context)
        {
            _context = context;
        }
    

        [HttpGet]
        public IActionResult GetBook()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = _context.Books.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);

            try
            {
                command.Model = newBook;
                command.Handle();
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updateBook)
        {
            var book = _context.Books.SingleOrDefault(book => book.Id == id);

            if (book is null)
                return BadRequest();

            book.GenreID = updateBook.GenreID != default ? updateBook.GenreID : book.GenreID;
            book.PageCount = updateBook.PageCount != default ? updateBook.PageCount : book.PageCount;
            book.Title = updateBook.Title != default ? updateBook.Title : book.Title;
            book.PublishDate = updateBook.PublishDate != default ? updateBook.PublishDate : book.PublishDate;
            book.Id = updateBook.Id != default ? updateBook.Id : book.Id;
            
            _context.SaveChanges();
            
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(book => book.Id == id);

            if (book is null)
                return BadRequest();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}
