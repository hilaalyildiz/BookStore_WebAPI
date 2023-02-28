using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using BookStore_WebAPI.DbOperations;
using Microsoft.EntityFrameworkCore;
using static BookStore_WebAPI.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using AutoMapper;
using FluentValidation.Results;
using FluentValidation;
using BookStore_WebAPI.Application.BookOperations.GetBookDetail;
using BookStore_WebAPI.Application.BookOperations.Commands.CreateBook;
using BookStore_WebAPI.Application.BookOperations.Commands.UpdateBook;
using BookStore_WebAPI.Application.BookOperations.Commands.DeleteBook;
using BookStore_WebAPI.Application.BookOperations.Queries.GetBooks;

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

            GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
            query.BookId = id;

            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            validator.ValidateAndThrow(query);

            result = query.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
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

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updateBook)
        {           
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = updateBook;

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }
    }
}
