using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Test.Data.Entities
{
    public class BorrowingRequest : BaseEntity
    {
        [Required]
        public int RequestByUserId { get; set; }
        public virtual User? RequestedBy { get; set; }
        public DateTime RequestedAt { get; set; }

        [Required]
        public int ProcessedByUserId { get; set; }
        public virtual User? ProcessedBy { get; set; }
        public DateTime ProcessedAt { get; set; }

        [Required, DefaultValue(RequestStatus.Waiting)]
        public RequestStatus Status { get; set; }

        public ICollection<BorrowingRequestDetail>? Details { get; set; }
    }
}