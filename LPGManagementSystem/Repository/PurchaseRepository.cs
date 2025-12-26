using Microsoft.EntityFrameworkCore;
using LPGManagementSystem.Data;
using LPGManagementSystem.Models;

namespace LPGManagementSystem.Repository
{
    public class PurchaseRepository : GenericRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Purchase>> GetPurchasesWithSupplierAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Include(p => p.Supplier)
                .OrderByDescending(p => p.TrDate)
                .ToListAsync();
        }

        public async Task<Purchase?> GetPurchaseWithSupplierAsync(int id)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.PurchaseId == id);
        }
    }
}