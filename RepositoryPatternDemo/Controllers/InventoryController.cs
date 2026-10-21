using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Services;

namespace RepositoryPatternDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : Controller
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInventorys()
        {
            List<Inventory> inventories = await _inventoryService.GetAllInventorys();
            return Ok(inventories);
        }

        [HttpPost]
        public async Task<IActionResult> InsertInventory([FromBody] Inventory inventory)
        {
            await _inventoryService.InsertInventory(inventory);
            return Ok(new {message="record insert successfully"});
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventory([FromBody] Inventory inventory)
        {
            await _inventoryService.UpdateInventory(inventory);
            return Ok(new { message="record updated successfully"});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            await _inventoryService.DeleteInventory(id);
            return Ok(new { message = "record deleted..." });
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
