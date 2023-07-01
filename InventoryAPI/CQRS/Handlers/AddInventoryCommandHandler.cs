using CommonLibrary;
using CommonLibrary.Models;

using InventoryAPI.Contexts;
using InventoryAPI.CQRS.Commands;

using MediatR;

namespace InventoryAPI.CQRS.Handlers
{
    public class AddInventoryCommandHandler : IRequestHandler<AddInventoryCommand, Inventory>
    {
        IGenericRepo<Inventory, InventoryDBContext> repo;

        public AddInventoryCommandHandler(IGenericRepo<Inventory, InventoryDBContext> repo)
        {
            this.repo = repo;
        }

        public async Task<Inventory> Handle(AddInventoryCommand request, CancellationToken cancellationToken)
        {
            var inventory = await repo.AddAsync(request.newInventory);
            await repo.SaveChangesAsync();
            return inventory;
        }
    }
}
