using Microsoft.AspNetCore.Mvc;
using TestWebApi.Models.Categories;
using TestWebApi.Services.Interfaces;

namespace TestWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public AddCateResponse? Add([FromBody] AddCateRequest addCategoryRequest)
        {
            Console.WriteLine("add");
            return _categoryService.Create(addCategoryRequest);
        }

        [HttpGet]
        public IEnumerable<GetCateResponse> GetAll()
        {
            return _categoryService.GetAll();
        }

        [HttpGet("{id}")]
        public GetCateResponse? GetById(int id)
        {
            return _categoryService.GetById(id);
        }

        [HttpPut("{id}")]
        public UpdateCateResponse? Update(int id, [FromBody] UpdateCateRequest updateCategoryRequest)
        {
            return _categoryService.Update(id, updateCategoryRequest);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _categoryService.Delete(id);
        }
    }
}