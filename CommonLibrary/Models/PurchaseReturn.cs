using System;
using System.Collections.Generic;

namespace CommonLibrary.Models
{
    public partial class PurchaseReturn : BaseModel
    {
        public int Id { get; set; }
        public int? PurchaseId { get; set; }
        public int? ItemId { get; set; }
        public double? Qty { get; set; }
        public DateTime? ReturnDate { get; set; }

        public virtual Purchase? Purchase { get; set; }
    }
}
