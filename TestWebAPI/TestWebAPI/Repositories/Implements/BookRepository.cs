using Test.Data;
using Test.Data.Entities;
using TestWebApi.Repositories;
using TestWebApi.Repositories.Interfaces;

namespace TestWebApi.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(BookContext context) : base(context)
        {
        }
    }
}