﻿using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Masters;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Journals
{
    /// <summary>
    /// CAPS_JournalEntryDocumentDetail is the new table
    /// </summary>
    [Table("CAPS_JournalEntryDocumentDetail")]
    public class JournalEntryDocumentDetailUnit : AccountingItemUnit
    {
        /// <summary>Gets or sets the VendorId field. </summary>   
        public virtual int? VendorId { get; set; }

        [ForeignKey("VendorId")]
        public virtual VendorUnit Vendor { get; set; }

        /// <summary>Gets or sets the PurchaseOrderItemID field. </summary>   
        public virtual long? PurchaseOrderItemId { get; set; }

        /// <summary>Gets or sets the DebitAccountingItemId field. </summary>   
        public virtual long? DebitAccountingItemId { get; set; }

        [ForeignKey("DebitAccountingItemId")]
        public virtual AccountingItemUnit AccountingItem { get; set; }
    }
}
