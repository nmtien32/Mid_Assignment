using TestWebApi.Models.Categories;

namespace TestWebApi.Services.Interfaces
{
    public interface ICategoryService
    {
        AddCateResponse Create(AddCateRequest createModel);
        IEnumerable<GetCateResponse> GetAll();
        GetCateResponse GetById(int id);
        UpdateCateResponse Update(int id, UpdateCateRequest updateModel);
        bool Delete(int id);
    }
}