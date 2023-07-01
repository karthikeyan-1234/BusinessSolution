using CommonLibrary.Models;

namespace InventoryAPI.Services
{
    public interface IInventoryService
    {
        Task<Inventory> AddNewInventory(Inventory newInventory);
        Task<IEnumerable<Inventory>> GetAllInventory();
    }
}