using AutoMapper;
using BookStore_WebAPI.DbOperations;
using BookStore_WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Linq;

namespace BookStore_WebAPI.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly BookStore_DBContext _dbcontext;
        private readonly IMapper _mapper;
        public CreateBookCommand(BookStore_DBContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _dbcontext.Books.SingleOrDefault(x => x.Title == Model.Title);

            if (book is not null)
                throw new InvalidOperationException("Kitap zaten mevcut");

            book = _mapper.Map<Book>(Model);

            //book = new Book();
            //book.Title = Model.Title;
            //book.PageCount = Model.PageCount;
            //book.PublishDate = Model.PublishDate;
            //book.GenreID = Model.GenreId;


            _dbcontext.Books.Add(book);
            _dbcontext.SaveChanges();
        }

        public class CreateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}
