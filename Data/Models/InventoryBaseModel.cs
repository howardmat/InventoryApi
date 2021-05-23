using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public abstract class InventoryBaseModel
    {
        [Key]
        public int Id { get; set; }

        public int? CreatedUserId { get; set; }
        public DateTime CreatedUtc { get; set; }

        public int? LastModifiedUserId { get; set; }
        public DateTime LastModifiedUtc { get; set; }

        public int? DeletedUserId { get; set; }
        public DateTime? DeletedUtc { get; set; }
    }
}
