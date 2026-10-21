    using RepositoryPatternDemo.Models;
    using RepositoryPatternDemo.Repositories;
namespace RepositoryPatternDemo.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepo _inventoryRepo;

        public InventoryService(IInventoryRepo inventoryRepo)
        {
            _inventoryRepo = inventoryRepo;
        }
        public async Task<List<Inventory>> GetAllInventorys()
        {
            return await _inventoryRepo.GetAllInventorys();
            //throw new NotImplementedException();
        }

        public async Task<int> InsertInventory(Inventory inventory)
        {
            return await _inventoryRepo.InsertInventory(inventory);
            //throw new NotImplementedException();
        }

        public async Task<int> UpdateInventory(Inventory inventory)
        {
            return await _inventoryRepo.UpdateInventory(inventory);
            //throw new NotImplementedException();
        }

        public async Task<int> DeleteInventory(int id)
        {
            return await _inventoryRepo.DeleteInventory(id);
            //throw new NotImplementedException();
        }
    }
}

