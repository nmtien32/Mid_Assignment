using Test.Data.Entities;
using TestWebApi.Models.Books;
using TestWebApi.Repositories;
using TestWebApi.Repositories.Interfaces;
using TestWebApi.Services.Interfaces;

namespace TestWebApi.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BookService(IBookRepository bookRepository, ICategoryRepository categoryRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }

        public AddBookResponse Create(AddBookRequest createModel)
        {
            using (var transaction = _bookRepository.DatabaseTransaction())
            {
                try
                {
                    var category = _categoryRepository.Get(s => s.Id == createModel.CategoryId);
                    if (category != null)
                    {
                        var createBook = new Book
                        {
                            Name = createModel.Name,
                            Author = createModel.Author,
                            Publisher = createModel.Publisher,
                            Price = createModel.Price,
                            YearOfPublication = createModel.YearOfPublication,
                            CategoryId = category.Id,
                        };
                        var book = _bookRepository.Create(createBook);


                        _bookRepository.SaveChanges();
                        transaction.Commit();

                        return new AddBookResponse
                        {
                            Id = book.Id,
                            Name = book.Name,
                            Author = book.Author,
                            Publisher = book.Publisher,
                            Price = createModel.Price,
                            YearOfPublication = createModel.YearOfPublication,
                            CategoryId = book.CategoryId
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

        public bool Delete(int id)
        {
            using (var transaction = _bookRepository.DatabaseTransaction())
            {
                try
                {
                    var deleteBook = _bookRepository.Get(p => p.Id == id);

                    if (deleteBook != null)
                    {
                        bool result = _bookRepository.Delete(deleteBook);

                        _bookRepository.SaveChanges();

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

        public IEnumerable<GetBookResponse> GetAll()
        {
            var listBook = _bookRepository.GetAll(p => true)
            .Select(book => new GetBookResponse
            {
                Id = book.Id,
                Name = book.Name,
                Author = book.Author,
                Publisher = book.Publisher,
                Price = book.Price,
                YearOfPublication = book.YearOfPublication,
                CategoryId = book.CategoryId
            });

            return listBook;
        }

        public GetBookResponse GetById(int id)
        {
            var requestBook = _bookRepository.Get(p => p.Id == id);

            if (requestBook != null)
            {
                return new GetBookResponse
                {
                    Id = requestBook.Id,
                    Name = requestBook.Name,
                    Author = requestBook.Author,
                    Publisher = requestBook.Publisher,
                    Price = requestBook.Price,
                    YearOfPublication = requestBook.YearOfPublication,
                    CategoryId = requestBook.CategoryId
                };
            }

            return null;
        }

        public UpdateBookResponse Update(int id, UpdateBookRequest updateModel)
        {
            using (var transaction = _bookRepository.DatabaseTransaction())
            {
                try
                {
                    var book = _bookRepository.Get(p => p.Id == id);

                    if (book != null)
                    {
                        var category = _categoryRepository.Get(c => c.Id == updateModel.CategoryId);

                        if (category != null)
                        {
                            book.Name = updateModel.Name;
                            book.Author = updateModel.Author;
                            book.Publisher = updateModel.Publisher;
                            book.Price = updateModel.Price;
                            book.YearOfPublication = updateModel.YearOfPublication;
                            book.CategoryId = updateModel.CategoryId;

                            var updatedbook = _bookRepository.Update(book);

                            _bookRepository.SaveChanges();
                            transaction.Commit();

                            return new UpdateBookResponse
                            {
                                Id = updatedbook.Id,
                                Name = updatedbook.Name,
                                Author = updatedbook.Author,
                                Publisher = updatedbook.Publisher,
                                Price = updatedbook.Price,
                                YearOfPublication = updatedbook.YearOfPublication,
                                CategoryId = updatedbook.CategoryId
                            };
                        }

                        return null;
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