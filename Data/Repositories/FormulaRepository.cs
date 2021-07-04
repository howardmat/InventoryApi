﻿using Data.Extensions;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class FormulaRepository : Repository<Formula>, IFormulaRepository
    {
        public InventoryDbContext _context => Context as InventoryDbContext;

        public FormulaRepository(InventoryDbContext context) : base(context) { }

        public async Task<IEnumerable<Formula>> ListAsync(int tenantId)
        {
            return await _context.Formula
                .WhereNotDeleted()
                .WhereBelongsToTenant(tenantId)
                .OrderBy(f => f.Name)
                .ToListAsync();
        }

        public async Task<Formula> GetAsync(int id, int tenantId)
        {
            return await _context.Formula
                .WhereNotDeleted()
                .WhereBelongsToTenant(tenantId)
                .Where(f => f.Id == id)
                .OrderBy(f => f.Name)
                .FirstOrDefaultAsync();
        }
    }
}