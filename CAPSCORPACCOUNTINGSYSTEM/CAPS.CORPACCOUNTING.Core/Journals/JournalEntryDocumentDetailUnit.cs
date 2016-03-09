using CAPS.CORPACCOUNTING.Accounting;
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
        public int? VendorId { get; set; }

        [ForeignKey("VendorId")]
        public VendorUnit Vendor { get; set; }

        /// <summary>Gets or sets the PurchaseOrderItemID field. </summary>   
        public long? PurchaseOrderItemId { get; set; }
    }
}
