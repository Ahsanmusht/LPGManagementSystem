using LPGManagementSystem.Models;

namespace LPGManagementSystem.Repository
{
    public interface IItemRepository : IGenericRepository<Item>
    {
        Task<Item?> GetPrimaryItemAsync();
        Task UpdateStockAsync(int itemId, decimal quantity);
    }
}