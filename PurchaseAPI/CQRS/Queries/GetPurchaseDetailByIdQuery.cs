using CommonLibrary.Models.DTOs;

using MediatR;

namespace PurchaseAPI.CQRS.Queries
{
    public class GetPurchaseDetailByIdQuery : IRequest<IEnumerable<PurchaseDetailDTO>>
    {
        public int purchaseId { get; set; }

        public GetPurchaseDetailByIdQuery(int purchaseId)
        {
            this.purchaseId = purchaseId;
        }
    }
}
