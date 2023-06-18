namespace PurchaseAPI.Models.DTOs
{
    public class PurchaseDTO
    {
        public int Id { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public int? VendorId { get; set; }
    }
}
