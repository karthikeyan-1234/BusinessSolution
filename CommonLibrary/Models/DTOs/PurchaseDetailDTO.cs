namespace CommonLibrary.Models.DTOs
{
    public class PurchaseDetailDTO
    {
        public int Id { get; set; }
        public int? PurchaseId { get; set; }
        public int? ItemId { get; set; }
        public double? Qty { get; set; }
        public double? Rate { get; set; }
    }
}
