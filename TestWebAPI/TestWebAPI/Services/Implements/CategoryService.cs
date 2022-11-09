using Test.Data.Entities;
using TestWebApi.Repositories;
using TestWebApi.Services.Interfaces;
using TestWebApi.Models.Categories;

namespace TestWebApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public AddCateResponse Create(AddCateRequest createModel)
        {
            using (var transaction = _categoryRepository.DatabaseTransaction())
            {
                try
                {
                    var createCategory = new Category
                    {
                        Name = createModel.Name,
                        Description = createModel.Description
                    };

                    var category = _categoryRepository.Create(createCategory);

                    _categoryRepository.SaveChanges();

                    transaction.Commit();

                    return new AddCateResponse
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Description = category.Description
                    };
                }
                catch
                {
                    transaction.RollBack();

                    return null;
                }
            }
        }

        public bool Delete(int id)
        {
            using (var transaction = _categoryRepository.DatabaseTransaction())
            {
                try
                {
                    var deleteCategory = _categoryRepository.Get(c => c.Id == id);

                    if (deleteCategory != null)
                    {
                        bool result = _categoryRepository.Delete(deleteCategory);

                        _categoryRepository.SaveChanges();

                        transaction.Commit();

                        return result;
                    }

                    return false;
                }
                catch
                {
                    transaction.RollBack();

                    return false;
                }
            }
        }

        public IEnumerable<GetCateResponse> GetAll()
        {
            var listBook = _categoryRepository.GetAll(c => true).Select(category => new GetCateResponse
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            });

            return listBook;
        }

        public GetCateResponse GetById(int id)
        {
            var requestCategory = _categoryRepository.Get(p => p.Id == id);

            if (requestCategory != null)
            {
                return new GetCateResponse
                {
                    Id = requestCategory.Id,
                    Name = requestCategory.Name,
                    Description = requestCategory.Description
                };
            }

            return null;
        }

        public UpdateCateResponse Update(int id, UpdateCateRequest updateModel)
        {
            using (var transaction = _categoryRepository.DatabaseTransaction())
            {
                try
                {
                    var category = _categoryRepository.Get(c => c.Id == id);

                    if (category != null)
                    {
                        category.Name = updateModel.Name;
                        category.Description = updateModel.Description;

                        var updatedCategory = _categoryRepository.Update(category);

                        _categoryRepository.SaveChanges();
                        transaction.Commit();

                        return new UpdateCateResponse
                        {
                            Id = updatedCategory.Id,
                            Name = updatedCategory.Name,
                            Description = updatedCategory.Description
                        };
                    }

                    return null;
                }
                catch
                {
                    transaction.RollBack();

                    return null;
                }
            }
        }
    }
}