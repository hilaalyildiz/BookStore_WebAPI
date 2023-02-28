using BookStore_WebAPI.DbOperations;
using BookStore_WebAPI.Entities;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace BookStore_WebAPI.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }
        private readonly BookStore_DBContext _dbContext;

        public CreateGenreCommand(BookStore_DBContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Name == Model.Name);
            if (genre is not null)
                throw new InvalidOperationException("Kitap türü zaten mevcut.");

            genre = new Genre();
            genre.Name = Model.Name;
            _dbContext.Genres.Add(genre);
            _dbContext.SaveChanges();
        }
    }

    public class CreateGenreModel
    {
        public string Name { get; set; }   
    }
}
