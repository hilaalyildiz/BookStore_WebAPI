using AutoMapper;
using BookStore_WebAPI.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore_WebAPI.Application.GenreOperations.Queries.GetGenresDetail
{
    public class GetGenreDetailQuery
    {
        public readonly BookStore_DBContext _dbContext;
        public readonly IMapper _mapper;
        public int GenreId { get; set; }

        public GetGenreDetailQuery(BookStore_DBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.IsActive && x.Id == GenreId);

            if (genre is null)
                throw new InvalidOperationException("Kitap türü bulunamadı!");
            return _mapper.Map<GenreDetailViewModel>(genre);

        }
    }

    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
