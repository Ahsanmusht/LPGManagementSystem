using Microsoft.EntityFrameworkCore;
using LPGManagementSystem.Data;
using LPGManagementSystem.Models;

namespace LPGManagementSystem.Repository
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesWithDetailsAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Include(i => i.Customer)
                .Include(i => i.InvoiceItems)
                    .ThenInclude(ii => ii.Item)
                .OrderByDescending(i => i.Date)
                .ToListAsync();
        }

        public async Task<Invoice?> GetInvoiceWithDetailsAsync(string id)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(i => i.Customer)
                .Include(i => i.InvoiceItems)
                    .ThenInclude(ii => ii.Item)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}