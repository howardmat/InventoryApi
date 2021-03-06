using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models.RequestModels
{
    public class MaterialInventoryTransactionRequest
    {
        [Required]
        public int? MaterialId { get; set; }

        [Required]
        [Range(double.MinValue, double.MaxValue, ErrorMessage = "{0} must be greater than {1} but cannot exceed {2}")]
        public decimal Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "{0} must be greater than {1} but cannot exceed {2}")]
        public decimal AmountPaid { get; set; }

        public string Description { get; set; }
    }
}
