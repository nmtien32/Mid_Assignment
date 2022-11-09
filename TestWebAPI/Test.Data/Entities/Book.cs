using System.ComponentModel.DataAnnotations;

namespace Test.Data.Entities
{
    public class Book : BaseEntity
    {
        [Required, MaxLength(50)]
        public string? Name { get; set; }

        [Required, MaxLength(50)]
        public string? Author { get; set; }

        [Required, MaxLength(50)]
        public string? Publisher { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int YearOfPublication { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category? Category { get; set; }
    }
}