using Microsoft.EntityFrameworkCore;
using LPGManagementSystem.Data;
using LPGManagementSystem.Models;

namespace LPGManagementSystem.Repository
{
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        public ItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Item?> GetPrimaryItemAsync()
        {
            return await _dbSet
                .FirstOrDefaultAsync(i => i.IsPrimary);
        }

        public async Task UpdateStockAsync(int itemId, decimal quantity)
        {
            var item = await _dbSet.FindAsync(itemId);
            if (item != null)
            {
                item.AvailableStock += quantity;
                await _context.SaveChangesAsync();
            }
        }
    }
}