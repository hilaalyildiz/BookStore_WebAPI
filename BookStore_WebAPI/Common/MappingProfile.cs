using AutoMapper;
using BookStore_WebAPI.Application.BookOperations.Queries.GetBooks;
using BookStore_WebAPI.Application.GenreOperations.Queries.GetGenres;
using BookStore_WebAPI.Entities;
using BookStore_WebAPI.GetBookDetail;
using static BookStore_WebAPI.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace BookStore_WebAPI.Common
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreID).ToString()));
            CreateMap<Book, BooksViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreID).ToString()));
            CreateMap<Genre, GenresViewModel>();
        }
    }
}
