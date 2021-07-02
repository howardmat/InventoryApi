using Data.Models;
using System.Linq;

namespace Data.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> WhereNotDeleted<T> (this IQueryable<T> source) where T : InventoryBaseModel
        {
            return source.Where(entity => !entity.DeletedUtc.HasValue);
        }

        public static IQueryable<T> WhereBelongsToTenant<T>(this IQueryable<T> source, int tenantId) where T : TenantInventoryBaseModel
        {
            return source.Where(entity => entity.TenantId == tenantId);
        }
    }
}
