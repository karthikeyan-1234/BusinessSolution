using System;
using System.Collections.Generic;

namespace PurchaseAPI.Models
{
    public partial class Purchase
    {
        public Purchase()
        {
            PurchaseDetails = new HashSet<PurchaseDetail>();
            PurchaseReturns = new HashSet<PurchaseReturn>();
        }

        public int Id { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public int? VendorId { get; set; }

        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }
        public virtual ICollection<PurchaseReturn> PurchaseReturns { get; set; }
    }
}
