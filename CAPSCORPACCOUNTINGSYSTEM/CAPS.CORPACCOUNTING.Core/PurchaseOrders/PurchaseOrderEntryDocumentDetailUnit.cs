using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Masters;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.PurchaseOrders
{
    /// <summary>
    /// CAPS_PurchaseOrderEntryDocumentDetail is the new table
    /// </summary>
    [Table("CAPS_PurchaseOrderEntryDocumentDetail")]
    public class PurchaseOrderEntryDocumentDetailUnit : AccountingItemUnit
    {
        /// <summary>Gets or sets the IsPrePaid field. </summary>   
        public virtual bool IsPrePaid { get; set; } 

        /// <summary>Gets or sets the IsPOPurchase field. </summary>   
        public virtual bool? IsPoPurchase { get; set; }

        /// <summary>Gets or sets the IsPORental field. </summary>   
        public virtual bool? IsPoRental { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>   
        public virtual int? VendorId { get; set; }

        [ForeignKey("VendorId")]
        public virtual VendorUnit Vendor { get; set; }
    }
}
