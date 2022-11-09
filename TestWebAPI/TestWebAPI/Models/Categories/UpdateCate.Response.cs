using Test.Data.Entities;

namespace TestWebApi.Models.Categories
{
    public class UpdateCateResponse : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}