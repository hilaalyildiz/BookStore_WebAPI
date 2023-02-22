using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;

namespace BookStore_WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private static List<Book> BookList = new List<Book>()
        {
            new Book{
                    Id =1,
                    Title="Lean Startup",
                    GenreID=1,
                    PageCount=200,
                    PublishDate=new DateTime(2001,06,26)
                },
            new Book{
                    Id =2,
                    Title="Hearland",
                    GenreID=2,
                    PageCount=250,
                    PublishDate=new DateTime(2011,06,16)
                },
            new Book{
                    Id =3,
                    Title="Dune",
                    GenreID=3,
                    PageCount=450,
                    PublishDate=new DateTime(2014,12,16)
                }

        };

        [HttpGet]
        public List<Book> GetBook()
        {
            var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = BookList.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);

            if (book is not null)
                return BadRequest();

            BookList.Add(newBook);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updateBook)
        {
            var book = BookList.SingleOrDefault(book => book.Id == id);

            if (book is null)
                return BadRequest();

            book.GenreID = updateBook.GenreID != default ? updateBook.GenreID : book.GenreID;
            book.PageCount = updateBook.PageCount != default ? updateBook.PageCount : book.PageCount;
            book.Title = updateBook.Title != default ? updateBook.Title : book.Title;
            book.PublishDate = updateBook.PublishDate != default ? updateBook.PublishDate : book.PublishDate;
            book.Id = updateBook.Id != default ? updateBook.Id : book.Id;

            return Ok();
        }
    }
}
