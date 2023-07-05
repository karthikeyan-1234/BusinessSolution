using CommonLibrary.Models;

using InventoryAPI.CQRS.Commands;
using InventoryAPI.CQRS.Queries;

using MediatR;

namespace InventoryAPI.Services
{
    public class InventoryService : IInventoryService
    {
        IMediator mediator;
        ILogger<InventoryService> logger;

        public InventoryService(IMediator mediator, ILogger<InventoryService> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
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
            logger.LogInformation("Retrieved all inventories..");
            return result;
        }
    }
}

