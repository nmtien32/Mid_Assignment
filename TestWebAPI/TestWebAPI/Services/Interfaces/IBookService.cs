using TestWebApi.Models.Books;

namespace TestWebApi.Services.Interfaces
{
    public interface IBookService
    {
        AddBookResponse Create(AddBookRequest createModel);
        IEnumerable<GetBookResponse> GetAll();
        GetBookResponse GetById(int id);
        UpdateBookResponse Update(int id, UpdateBookRequest updateModel);
        bool Delete(int id);
    }
}