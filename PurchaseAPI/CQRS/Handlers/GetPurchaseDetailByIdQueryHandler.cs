using CommonLibrary.Models.DTOs;

using MediatR;

using PurchaseAPI.CQRS.Queries;

namespace PurchaseAPI.CQRS.Handlers
{
    public class GetPurchaseDetailByIdQueryHandler : IRequestHandler<GetPurchaseDetailByIdQuery, IEnumerable<PurchaseDetailDTO>>
    {
        public Task<IEnumerable<PurchaseDetailDTO>> Handle(GetPurchaseDetailByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
