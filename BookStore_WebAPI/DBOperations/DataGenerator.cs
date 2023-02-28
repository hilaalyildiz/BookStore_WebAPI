using BookStore_WebAPI.DbOperations;
using BookStore_WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BookStore_WebAPI.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new BookStore_DBContext(serviceProvider.GetRequiredService<DbContextOptions<BookStore_DBContext>>()))
            {
                if (context.Books is null)
                    return;
                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personal Growth"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Romance"
                    });

                context.Books.AddRange(
                    new Book
                    {
                        //Id = 1,
                        Title = "Lean Startup",
                        GenreID = 1,
                        PageCount = 200,
                        PublishDate = new DateTime(2001, 06, 26)
                    },
                    new Book
                    {
                        //Id = 2,
                        Title = "Hearland",
                        GenreID = 2,
                        PageCount = 250,
                        PublishDate = new DateTime(2011, 06, 16)
                    },
                    new Book
                    {
                        //Id = 3,
                        Title = "Dune",
                        GenreID = 3,
                        PageCount = 450,
                        PublishDate = new DateTime(2014, 12, 16)
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
