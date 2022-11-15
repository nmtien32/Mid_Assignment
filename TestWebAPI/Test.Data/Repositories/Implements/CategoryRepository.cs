using Test.Data;
using Test.Data.Entities;

namespace TestWebApi.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(BookContext context) : base(context)
        {
        }
    }
}