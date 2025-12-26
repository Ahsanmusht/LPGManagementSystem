using LPGManagementSystem.Models;

namespace LPGManagementSystem.Repository
{
    public interface IInvoiceRepository : IGenericRepository<Invoice>
    {
        Task<IEnumerable<Invoice>> GetInvoicesWithDetailsAsync();
        Task<Invoice?> GetInvoiceWithDetailsAsync(string id);
    }
}