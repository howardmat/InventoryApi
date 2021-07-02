namespace Data.Models
{
    public abstract class TenantInventoryBaseModel : InventoryBaseModel
    {
        public int TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }
    }
}
