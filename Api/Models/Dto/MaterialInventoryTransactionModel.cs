using System.ComponentModel.DataAnnotations;

namespace Api.Models.Dto
{
    public class MaterialInventoryTransactionModel
    {
        public int? MaterialId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "{0} must have a positive value and cannot exceed {2}")]
        public decimal Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "{0} must have a positive value and cannot exceed {2}")]
        public decimal AmountPaid { get; set; }

        public string Description { get; set; }
    }
}
