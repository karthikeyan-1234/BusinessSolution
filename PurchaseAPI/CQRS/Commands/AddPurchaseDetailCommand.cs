using CommonLibrary.Models.DTOs;

using MediatR;

namespace PurchaseAPI.CQRS.Commands
{
    public class AddPurchaseDetailCommand : IRequest<PurchaseDetailDTO>
    {
        public PurchaseDetailDTO purchaseDetailDTO { get; set; }

        public AddPurchaseDetailCommand(PurchaseDetailDTO purchaseDetailDTO)
        {
            this.purchaseDetailDTO = purchaseDetailDTO;
        }
    }
}
