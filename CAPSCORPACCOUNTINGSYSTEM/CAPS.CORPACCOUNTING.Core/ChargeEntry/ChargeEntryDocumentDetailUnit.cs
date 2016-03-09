using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Masters;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.ChargeEntry
{
    /// <summary>
    /// CAPS_ChargeEntryDocumentDetail is the new table
    /// </summary>
    [Table("CAPS_ChargeEntryDocumentDetail")]
    public class ChargeEntryDocumentDetailUnit : AccountingItemUnit
    {
        /// <summary>Gets or sets the PurchaseOrderItemId field. </summary>   
        public virtual long? PurchaseOrderItemId { get; set; }

        /// <summary>Gets or sets the POHistoryItemID field. </summary>   
        public virtual long? PoHistoryItemId { get; set; }

        /// <summary>Gets or sets the VendorID field. </summary>   
        public virtual int? VendorId { get; set; }

        [ForeignKey("VendorId")]
        public virtual VendorUnit Vendor { get; set; }

        /// <summary>Gets or sets the IsPrePaid field. </summary>   
        public virtual bool IsPrePaid { get; set; }

        /// <summary>Gets or sets the BankAccountID field. </summary>   
        public virtual long? BankAccountId { get; set; }

        [ForeignKey("BankAccountId")]
        public virtual BankAccountUnit BankAccount { get; set; }

        /// <summary>Gets or sets the TypeOfChargeID field. </summary>   
        public virtual short? TypeOfChargeId { get; set; }

        /// <summary>Gets or sets the ChargeReferenceNumber field. </summary>   
        public virtual string ChargeReferenceNumber { get; set; }

        /// <summary>Gets or sets the ChargeDate field. </summary>   
        public virtual DateTime? ChargeDate { get; set; }

        /// <summary>Gets or sets the ChargeSICCode field. </summary>   
        public virtual string ChargeSicCode { get; set; }

        /// <summary>Gets or sets the ChargeSENumber field. </summary>   
        public virtual string ChargeSeNumber { get; set; }

        /// <summary>Gets or sets the ChargeOtherInfo field. </summary>   
        public virtual string ChargeOtherInfo { get; set; }

        /// <summary>Gets or sets the ChargeOriginalAmount field. </summary>   
        public virtual decimal? ChargeOriginalAmount { get; set; }

        /// <summary>Gets or sets the ChargeRetiredAmount field. </summary>   
        public virtual decimal? ChargeRetiredAmount { get; set; } 

        /// <summary>Gets or sets the CustomerID field. </summary>   
        public virtual bool IsChargeDisputed { get; set; } // IsChargeDisputed
    }
}
