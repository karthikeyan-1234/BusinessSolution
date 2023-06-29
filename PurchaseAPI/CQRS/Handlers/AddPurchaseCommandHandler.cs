using AutoMapper;

using CommonLibrary;
using CommonLibrary.Models;
using CommonLibrary.Models.DTOs;

using MediatR;

using PurchaseAPI.Contexts;
using PurchaseAPI.CQRS.Commands;


namespace PurchaseAPI.CQRS.Handlers
{
    public class AddPurchaseCommandHandler : IRequestHandler<AddPurchaseCommand, PurchaseDTO>
    {
        IGenericRepo<Purchase,PurchaseDBContext> repo;
        IMapper mapper;

        public AddPurchaseCommandHandler(IGenericRepo<Purchase, PurchaseDBContext> repo,IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public async Task<PurchaseDTO> Handle(AddPurchaseCommand request, CancellationToken cancellationToken)
        {
            var res = await repo.AddAsync(mapper.Map<Purchase>(request.newPurchase));
            await repo.SaveChangesAsync();
            return mapper.Map<PurchaseDTO>(res);
        }
    }
}
