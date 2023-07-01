using CommonLibrary.Models;

using MediatR;

namespace InventoryAPI.CQRS.Queries
{
    public class GetAllInventoryQuery: IRequest<IEnumerable<Inventory>>
    {
    }
}
