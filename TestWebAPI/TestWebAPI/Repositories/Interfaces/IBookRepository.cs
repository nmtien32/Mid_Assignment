using Test.Data.Entities;
using TestWebApi.Repositories;

namespace TestWebApi.Repositories.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book>
    {
    }
}