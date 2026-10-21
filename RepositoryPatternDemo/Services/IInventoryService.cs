using RepositoryPatternDemo.Models;

namespace RepositoryPatternDemo.Services
{
    public interface IInventoryService
    {
        Task<List<Inventory>> GetAllInventorys();
        Task<int> InsertInventory(Inventory inventory);
        Task<int> UpdateInventory(Inventory inventory);

        Task<int> DeleteInventory(int id);
    }
}
