using System.ComponentModel.DataAnnotations;

namespace Test.Data.Entities
{
    public class BorrowingRequestDetail
    {
        [Required]
        public int BorrowingRequestId { get; set; }

        public virtual BorrowingRequest? BorrowingRequest { get; set; }

        [Required]
        public int BookId { get; set; }

        public virtual Book? Book { get; set; }
    }
}