using CommonLibrary;
using CommonLibrary.Models;

using InventoryAPI.Contexts;
using InventoryAPI.CQRS.Queries;

using MediatR;

namespace InventoryAPI.CQRS.Handlers
{
    public class GetAllInventoryQueryHandler : IRequestHandler<GetAllInventoryQuery, IEnumerable<Inventory>>
    {
        IGenericRepo<Inventory, InventoryDBContext> repo;

        public GetAllInventoryQueryHandler(IGenericRepo<Inventory, InventoryDBContext> repo)
        {
            this.repo = repo;
        }

        public async Task<IEnumerable<Inventory>> Handle(GetAllInventoryQuery request, CancellationToken cancellationToken)
        {
            return await repo.GetAllAsync();
        }
    }
}
