using System;
using System.Collections.Generic;

namespace PurchaseAPI.Models
{
    public partial class PurchaseDetail
    {
        public int Id { get; set; }
        public int? PurchaseId { get; set; }
        public int? ItemId { get; set; }
        public double? Qty { get; set; }
        public double? Rate { get; set; }

        public virtual Purchase? Purchase { get; set; }
    }
}
