using Microsoft.AspNetCore.Mvc;
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
using AutoMapper;
using FluentValidation.Results;
using FluentValidation;

namespace BookStore_WebAPI.Controllers
{
        [ApiController]
        [Route("[controller]s")]
        public class Book_Controller : ControllerBase
        {

        private readonly BookStore_DBContext _context;
        private readonly IMapper _mapper;
        public Book_Controller(BookStore_DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    

        [HttpGet]
        public IActionResult GetBook()
        {
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
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
                CreateBookCommand command = new CreateBookCommand(_context,_mapper);
                command.Model = newBook;

                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();

                //Son kullanıcıya hata göstermez. Bunun için ValidateAndThrow metodu kullanılır.
                /*if (!result.IsValid)
                {
                    foreach (var item in result.Errors)
                    {
                        Console.WriteLine("Özellik " + item.PropertyName + "- Error Message:" + item.ErrorMessage);
                    }
                }else
                {
                    command.Handle();
                }*/
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
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
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
