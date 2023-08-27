using CommonLibrary.Models.DTOs;

namespace PurchaseAPI.Services
{
    public interface IPurchaseService
    {
        Task<PurchaseDTO>? AddNewPurchaseAsync(PurchaseDTO newPurchase);
        Task<IEnumerable<PurchaseDTO>> GetAllPurchases();
        Task<PurchaseDetailDTO> AddNewPurchaseDetail(PurchaseDetailDTO newPurchaseDetailDTO);
        Task<IEnumerable<PurchaseDetailDTO>> GetPurchaseDetailDTO(int purchaseID);
    }
}