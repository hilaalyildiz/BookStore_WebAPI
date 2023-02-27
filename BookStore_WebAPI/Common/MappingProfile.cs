using AutoMapper;
using BookStore_WebAPI.BookOperations.GetBooks;
using BookStore_WebAPI.GetBookDetail;
using static BookStore_WebAPI.BookOperations.CreateBook.CreateBookCommand;

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
        }
    }
}
