using System;
using System.ComponentModel.DataAnnotations;

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
    }
}
