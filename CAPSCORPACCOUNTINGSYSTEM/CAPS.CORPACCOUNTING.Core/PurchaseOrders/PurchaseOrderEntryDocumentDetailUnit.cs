using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Masters;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.PurchaseOrders
{
    /// <summary>
    /// CAPS_PurchaseOrderEntryDocumentDetail is the new table
    /// </summary>
    [Table("CAPS_PurchaseOrderEntryDocumentDetail")]
    public class PurchaseOrderEntryDocumentDetailUnit : AccountingItemUnit
    {
        public const int MaxLength = 100;
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

        /// <summary>Gets or sets the OverRelieveAmount field. </summary>
        public virtual decimal? OverRelieveAmount { get; set; }

        /// <summary>Gets or sets the RemainingAmount field. </summary>
        public virtual decimal? RemainingAmount { get; set; }

        /// <summary>Gets or sets the PendingAmount field. </summary>
        public virtual decimal? PendingAmount { get; set; }

        /// <summary>Gets or sets the IsClose field. </summary> 
        public virtual bool? IsClose { get; set; }

        /// <summary>Gets or sets the CloseDate field. </summary> 
        public virtual DateTime? CloseDate { get; set; }

        /// <summary>Get Sets the SourceTypeId field.</summary>
        public virtual SourceType? SourceTypeId { get; set; }

        /// <summary>Get Sets the DocumentReference field.</summary>
        [StringLength(MaxLength)]
        public virtual string DocumentReference { get; set; }

        /// <summary>Get Sets the SourceId field.</summary>
        public virtual long? SourceId { get; set; }

        [ForeignKey("SourceId")]
        public virtual AccountingHeaderTransactionsUnit AccountingDocument { get; set; }
    }
}
