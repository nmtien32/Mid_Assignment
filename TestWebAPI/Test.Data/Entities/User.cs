using System.ComponentModel.DataAnnotations;
using Common.Enums;

namespace Test.Data.Entities
{
    public class User : BaseEntity
    {
        [Required, MaxLength(50)]
        public string? UserName { get; set; }

        [Required, MaxLength(50)]
        public string? Password { get; set; }

        [Required, MaxLength(50)]
        public string? FirstName { get; set; }

        [Required, MaxLength(50)]
        public string? LastName { get; set; }

        public UserRoleEnum? Role { get; set; }

        public ICollection<BorrowingRequest>? BorrowingRequests { get; set; } 
        public ICollection<BorrowingRequest>? ProcessedRequests { get; set; }    
    }
}