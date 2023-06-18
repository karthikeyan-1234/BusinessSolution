using AutoMapper;

using CommonLibrary;
using CommonLibrary.Models;
using CommonLibrary.Models.DTOs;

using MediatR;

using PurchaseAPI.CQRS.Queries;


namespace PurchaseAPI.CQRS.Handlers
{
    public class GetAllPurchasesHandler : IRequestHandler<GetAllPurchasesQuery, IEnumerable<PurchaseDTO>>
    {
        IGenericRepo<Purchase> repo;
        IMapper mapper;

        public GetAllPurchasesHandler(IGenericRepo<Purchase> repo,IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;

        }

        public async Task<IEnumerable<PurchaseDTO>> Handle(GetAllPurchasesQuery request, CancellationToken cancellationToken)
        {
            var res = await repo.GetAllAsync();
            return mapper.Map<IEnumerable<PurchaseDTO>>(res);
        }
    }
}
