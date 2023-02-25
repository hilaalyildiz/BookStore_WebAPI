﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using BookStore_WebAPI.DbOperations;
using Microsoft.EntityFrameworkCore;
using BookStore_WebAPI.BookOperations.GetBooks;
using BookStore_WebAPI.BookOperations.CreateBook;
using static BookStore_WebAPI.BookOperations.CreateBook.CreateBookCommand;
using BookStore_WebAPI.GetBookDetail;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using BookStore_WebAPI.UpdateBook;
using BookStore_WebAPI.DeleteBook;

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
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context);
                query.BookId = id;
                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            
            try
            {
                CreateBookCommand command = new CreateBookCommand(_context);
                command.Model = newBook;
                command.Handle();
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updateBook)
        {
            
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updateBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
