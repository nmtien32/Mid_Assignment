using System.ComponentModel.DataAnnotations;

namespace Test.Data.Entities
{
    public class Category : BaseEntity
    {
        [Required, MaxLength(50)]
        public string? Name { get; set; }

        [Required, MaxLength(1024)]
        public string? Description { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}