using AutoMapper;
using BookStore_WebAPI.Common;
using BookStore_WebAPI.DbOperations;
using BookStore_WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore_WebAPI.Application.BookOperations.Queries.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStore_DBContext _dbcontext;
        private readonly IMapper _mapper;
        public GetBooksQuery(BookStore_DBContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _dbcontext.Books.OrderBy(x => x.Id).ToList();

            List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(bookList);
            //new List <BooksViewModel>();

            /*foreach(var book in bookList)
            {
                vm.Add(new BooksViewModel()
                {
                    Title = book.Title,
                    Genre = ((GenreEnum)book.GenreID).ToString(),
                    PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy"),
                    PageCount = book.PageCount
                }) ;
            }*/

            return vm;
        }
    }

    public class BooksViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }

    }
}
