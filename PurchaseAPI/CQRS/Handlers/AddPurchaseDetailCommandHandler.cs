using AutoMapper;

using CommonLibrary;
using CommonLibrary.Models;
using CommonLibrary.Models.DTOs;

using MediatR;

using PurchaseAPI.Contexts;
using PurchaseAPI.CQRS.Commands;

namespace PurchaseAPI.CQRS.Handlers
{
    public class AddPurchaseDetailCommandHandler : IRequestHandler<AddPurchaseDetailCommand, PurchaseDetailDTO>
    {
        IGenericRepo<PurchaseDetail, PurchaseDBContext> repo;
        IMapper mapper;

        public AddPurchaseDetailCommandHandler(IGenericRepo<PurchaseDetail, PurchaseDBContext> repo,IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public async Task<PurchaseDetailDTO> Handle(AddPurchaseDetailCommand request, CancellationToken cancellationToken)
        {
            var result = await repo.AddAsync(mapper.Map<PurchaseDetail>(request.purchaseDetailDTO));
            await repo.SaveChangesAsync();
            return mapper.Map<PurchaseDetailDTO>(result);
        }
    }
}
