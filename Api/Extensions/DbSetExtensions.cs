using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Api.Extensions
{
    public static class DbSetExtensions
    {
        public static IQueryable<T> WhereNotDeleted<T> (this DbSet<T> query, T entity) where T : InventoryBaseModel
        {
            return query.Where(entity => !entity.DeletedUtc.HasValue);
        }

        public static IQueryable<T> WhereBelongsToTenant<T>(this DbSet<T> query, T entity, int tenantId) where T : TenantInventoryBaseModel
        {
            return query.Where(entity => entity.TenantId == tenantId);
        }
    }
}
