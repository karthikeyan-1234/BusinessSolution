using AutoMapper;

using CommonLibrary;
using CommonLibrary.Models;
using CommonLibrary.Models.DTOs;

using MediatR;

using PurchaseAPI.Contexts;
using PurchaseAPI.CQRS.Queries;

namespace PurchaseAPI.CQRS.Handlers
{
    public class GetPurchaseDetailByIdQueryHandler : IRequestHandler<GetPurchaseDetailByIdQuery, IEnumerable<PurchaseDetailDTO>>
    {
        IGenericRepo<PurchaseDetail,PurchaseDBContext> purchDetRepo;
        IGenericRepo<Purchase, PurchaseDBContext> purchRepo;
        IMapper mapper;

        public GetPurchaseDetailByIdQueryHandler(IGenericRepo<PurchaseDetail, PurchaseDBContext> purchDetRepo, IGenericRepo<Purchase, PurchaseDBContext> purchRepo,IMapper mapper)
        {
            this.purchDetRepo = purchDetRepo;
            this.purchRepo = purchRepo;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<PurchaseDetailDTO>> Handle(GetPurchaseDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var purchase = await purchRepo.GetById(request.purchaseId);
            return mapper.Map<IEnumerable<PurchaseDetailDTO>>(purchase.PurchaseDetails);
        }
    }
}
