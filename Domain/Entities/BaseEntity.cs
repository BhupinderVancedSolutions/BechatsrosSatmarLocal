using System;

namespace Domain.Entities
{
    public class BaseEntity
    {
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; init; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; init; }
    }
}
