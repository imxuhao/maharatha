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
        public bool IsPrePaid { get; set; } 

        /// <summary>Gets or sets the IsPOPurchase field. </summary>   
        public bool? IsPoPurchase { get; set; }

        /// <summary>Gets or sets the IsPORental field. </summary>   
        public bool? IsPoRental { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>   
        public int? VendorId { get; set; }

        [ForeignKey("VendorId")]
        public VendorUnit Vendor { get; set; }
    }
}
