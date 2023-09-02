using System;
using System.Collections.Generic;

namespace CommonLibrary.Models
{
    public partial class Purchase : BaseModel
    {
        //public Purchase()
        //{
        //    PurchaseDetails = new HashSet<PurchaseDetail>();
        //    PurchaseReturns = new HashSet<PurchaseReturn>();
        //}

        public int Id { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public int? VendorId { get; set; }

        public ICollection<PurchaseDetail> PurchaseDetails { get; set; }
        public ICollection<PurchaseReturn> PurchaseReturns { get; set; }
    }
}
