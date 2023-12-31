﻿using System;
using System.Collections.Generic;

namespace PurchaseAPI.Models
{
    public partial class PurchaseReturn
    {
        public int Id { get; set; }
        public int? PurchaseId { get; set; }
        public int? ItemId { get; set; }
        public double? Qty { get; set; }
        public DateTime? ReturnDate { get; set; }

        public virtual Purchase? Purchase { get; set; }
    }
}
