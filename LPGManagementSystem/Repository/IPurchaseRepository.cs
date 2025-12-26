using LPGManagementSystem.Models;

namespace LPGManagementSystem.Repository
{
    public interface IPurchaseRepository : IGenericRepository<Purchase>
    {
        Task<IEnumerable<Purchase>> GetPurchasesWithSupplierAsync();
        Task<Purchase?> GetPurchaseWithSupplierAsync(int id);
    }
}