using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models.RequestModels
{
    public class ProductInventoryTransactionRequest
    {
        [Required]
        public int? ProductId { get; set; }

        [Required]
        [Range(double.MinValue, double.MaxValue, ErrorMessage = "{0} must be greater than {1} but cannot exceed {2}")]
        public decimal Quantity { get; set; }

        public int? OrderDetailId { get; set; }

        public string Description { get; set; }
    }
}
