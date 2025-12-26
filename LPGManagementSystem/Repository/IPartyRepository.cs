using LPGManagementSystem.Models;

namespace LPGManagementSystem.Repository
{
    public interface IPartyRepository : IGenericRepository<Party>
    {
        Task<IEnumerable<Party>> GetPartiesByTypeAsync(int partyTypeId);
        Task<IEnumerable<Party>> GetPartiesWithTypeAsync();
        Task<Party?> GetPartyWithTypeAsync(int id);
    }
}