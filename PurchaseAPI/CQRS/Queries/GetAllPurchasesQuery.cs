using CommonLibrary.Models.DTOs;

using MediatR;


namespace PurchaseAPI.CQRS.Queries
{
    public class GetAllPurchasesQuery : IRequest<IEnumerable<PurchaseDTO>>
    {

    }
}
