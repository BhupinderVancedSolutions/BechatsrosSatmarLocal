using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ItemType:BaseEntity
    {
        [Key]
        public int ItemTypeId { get; set; }

        public string ItemTypeName { get; set; }

        public string Description { get; set; }
    }
}
