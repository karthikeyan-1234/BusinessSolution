using Microsoft.AspNetCore.Mvc;
using InventoryAPI.Contexts;
using CommonLibrary.Models;
using CommonLibrary;
using InventoryAPI.Services;

namespace InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        IInventoryService service;

        public InventoryController(IInventoryService service)
        {
            this.service = service;
        }

        [HttpPost("AddInventory",Name = "AddInventory")]
        public async Task<IActionResult> AddInventoryAsync(Inventory newInventory)
        {
            var res =  await service.AddNewInventory(newInventory);
            return Ok(res);
        }

        [HttpGet("GetAllInventory",Name = "GetAllInventory")]
        public async Task<IActionResult> GetAllInventoriesAsync()
        {
            var res = await service.GetAllInventory();
            return Ok(res);
        }
    }
}
