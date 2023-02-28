using AutoMapper;
using BookStore_WebAPI.DbOperations;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace BookStore_WebAPI.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        public readonly BookStore_DBContext _dbContext;
        public readonly IMapper _mapper;

        public GetGenresQuery(BookStore_DBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public List<GenresViewModel> Handle()
        {
            var genres = _dbContext.Genres.Where(x=>x.IsActive).OrderBy(x=>x.Id);
            List<GenresViewModel> returnObj = _mapper.Map <List<GenresViewModel>> (genres);

            return returnObj;
        }
    }

    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
