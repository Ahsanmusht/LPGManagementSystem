using Microsoft.EntityFrameworkCore;
using LPGManagementSystem.Data;
using LPGManagementSystem.Models;

namespace LPGManagementSystem.Repository
{
    public class PartyRepository : GenericRepository<Party>, IPartyRepository
    {
        public PartyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Party>> GetPartiesByTypeAsync(int partyTypeId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(p => p.PartyTypeId == partyTypeId)
                .OrderBy(p => p.PartyName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Party>> GetPartiesWithTypeAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Include(p => p.PartyType)
                .OrderBy(p => p.PartyName)
                .ToListAsync();
        }

        public async Task<Party?> GetPartyWithTypeAsync(int id)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(p => p.PartyType)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}