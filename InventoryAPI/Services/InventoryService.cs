using CommonLibrary.Models;

using InventoryAPI.CQRS.Commands;
using InventoryAPI.CQRS.Queries;

using MediatR;

namespace InventoryAPI.Services
{
    public class InventoryService : IInventoryService
    {
        IMediator mediator;

        public InventoryService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Inventory> AddNewInventory(Inventory newInventory)
        {
            var cmd = new AddInventoryCommand(newInventory);
            var result = await mediator.Send(cmd);
            return result;
        }

        public async Task<IEnumerable<Inventory>> GetAllInventory()
        {
            var qry = new GetAllInventoryQuery();
            var result = await mediator.Send(qry);
            return result;
        }
    }
}

