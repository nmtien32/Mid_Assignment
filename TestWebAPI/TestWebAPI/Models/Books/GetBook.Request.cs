using Test.Data.Entities;

namespace TestWebApi.Models.Books
{
    public class GetBookRequest : BaseEntity
    {
        public string? Name { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public int YearOfPublication { get; set; }
        public int CategoryId { get; set; }
    }
}