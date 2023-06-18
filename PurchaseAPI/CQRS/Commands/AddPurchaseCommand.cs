using CommonLibrary.Models.DTOs;
using MediatR;



namespace PurchaseAPI.CQRS.Commands
{
    public class AddPurchaseCommand: IRequest<PurchaseDTO>
    {
        public PurchaseDTO newPurchase { get; set; }

        public AddPurchaseCommand(PurchaseDTO newPurchase)
        {
            this.newPurchase = newPurchase;
        }
    }
}
