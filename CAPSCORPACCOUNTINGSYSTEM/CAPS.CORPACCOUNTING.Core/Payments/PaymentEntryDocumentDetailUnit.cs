using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Masters;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Payments
{
    /// <summary>
    /// CAPS_PaymentEntryDocumentDetail is the new table 
    /// </summary>
    [Table("CAPS_PaymentEntryDocumentDetail")]
    public class PaymentEntryDocumentDetailUnit: AccountingItemUnit
    {
        /// <summary>Gets or sets the PurchaseOrderItemID field. </summary>   
        public virtual long? PurchaseOrderItemId { get; set; }

        /// <summary>Gets or sets the PoHistoryItemId field. </summary>   
        public virtual long? PoHistoryItemId { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>   
        public virtual int? VendorId { get; set; }

        [ForeignKey("VendorId")]
        public virtual VendorUnit Vendor { get; set; }
    }
}
