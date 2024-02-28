using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Domain.Entities
{
    public class CCProcessorType : BaseEntity
    {
        [Key]
        public int CCProcessorTypeId { get; set; }

        [MaxLength(100)]
        public string CCProcessorName { get; set; }

        public ICollection<Transactions> Transactions { get; set; }
    }
}
