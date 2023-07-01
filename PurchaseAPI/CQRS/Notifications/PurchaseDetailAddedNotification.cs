using MediatR;

namespace PurchaseAPI.CQRS.Notifications
{
    public class PurchaseDetailAddedNotification: INotification
    {
        public string? Message { get; set; }

        public PurchaseDetailAddedNotification(string? message)
        {
            Message = message;
        }
    }
}
