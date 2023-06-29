using Microsoft.AspNetCore.Mvc;
using InventoryAPI.Contexts;
using CommonLibrary.Models;
using CommonLibrary;

namespace InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        IGenericRepo<Inventory, InventoryDBContext> repo;

        public InventoryController(IGenericRepo<Inventory, InventoryDBContext> repo)
        {
            this.repo = repo;
        }

        [HttpPost("AddInventory",Name = "AddInventory")]
        public async Task<IActionResult> AddInventoryAsync(Inventory newInventory)
        {
            var res = await repo.AddAsync(newInventory);
            await repo.SaveChangesAsync();
            return Ok(res);
        }
    }
}
