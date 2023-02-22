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
    }
}
