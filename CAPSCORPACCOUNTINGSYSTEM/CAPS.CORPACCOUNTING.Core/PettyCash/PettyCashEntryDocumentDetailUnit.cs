using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Masters;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.PettyCash
{
    /// <summary>
    /// CAPS_PettyCashEntryDocumentDetail is the new table
    /// </summary>
    [Table("CAPS_PettyCashEntryDocumentDetail")]
    public class PettyCashEntryDocumentDetailUnit : AccountingItemUnit
    {
        /// <summary>Gets or sets the PettyCashID field. </summary>   
        public int? PettyCashId { get; set; }

        /// <summary>Gets or sets the PurchaseOrderItemID field. </summary>   
        public long? PurchaseOrderItemId { get; set; }

        /// <summary>Gets or sets the PoHistoryItemId field. </summary>   
        public long? PoHistoryItemId { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>   
        public int? VendorId { get; set; }

        [ForeignKey("VendorId")]
        public VendorUnit Vendor { get; set; }
    }
}
