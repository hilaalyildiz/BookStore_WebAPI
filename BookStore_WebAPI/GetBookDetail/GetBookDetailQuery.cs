﻿using BookStore_WebAPI.Common;
using BookStore_WebAPI.DbOperations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookStore_WebAPI.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStore_DBContext _dbContext;
        public int BookId { get; set; }

        public GetBookDetailQuery(BookStore_DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Where(book => book.Id == BookId).SingleOrDefault();
            BookDetailViewModel vm = new BookDetailViewModel();

            if (book is not null)
                throw new InvalidOperationException("Kitap bulunamadı");

            vm.Title = book.Title;
            vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
            vm.PageCount = book.PageCount;
            vm.Genre = ((GenreEnum)book.GenreID).ToString();


            return vm;
        }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}
