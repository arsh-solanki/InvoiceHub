using RepositoryPatternDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace RepositoryPatternDemo.Repositories
{
    public interface IInventoryRepo
    {
        Task<List<Inventory>> GetAllInventorys();
        Task<int> InsertInventory(Inventory inventory);
        Task<int> UpdateInventory(Inventory inventory);

        Task<int> DeleteInventory(int id);
    }
}
