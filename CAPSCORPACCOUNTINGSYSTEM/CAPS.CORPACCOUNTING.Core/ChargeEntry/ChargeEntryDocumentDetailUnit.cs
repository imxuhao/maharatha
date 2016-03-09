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
        public long? PurchaseOrderItemId { get; set; }

        /// <summary>Gets or sets the POHistoryItemID field. </summary>   
        public long? PoHistoryItemId { get; set; }

        /// <summary>Gets or sets the VendorID field. </summary>   
        public int? VendorId { get; set; }

        [ForeignKey("VendorId")]
        public VendorUnit Vendor { get; set; }

        /// <summary>Gets or sets the IsPrePaid field. </summary>   
        public bool IsPrePaid { get; set; }

        /// <summary>Gets or sets the BankAccountID field. </summary>   
        public long? BankAccountId { get; set; }

        [ForeignKey("BankAccountId")]
        public BankAccountUnit BankAccount { get; set; }

        /// <summary>Gets or sets the TypeOfChargeID field. </summary>   
        public short? TypeOfChargeId { get; set; }

        /// <summary>Gets or sets the ChargeReferenceNumber field. </summary>   
        public string ChargeReferenceNumber { get; set; }

        /// <summary>Gets or sets the ChargeDate field. </summary>   
        public DateTime? ChargeDate { get; set; }

        /// <summary>Gets or sets the ChargeSICCode field. </summary>   
        public string ChargeSicCode { get; set; }

        /// <summary>Gets or sets the ChargeSENumber field. </summary>   
        public string ChargeSeNumber { get; set; }

        /// <summary>Gets or sets the ChargeOtherInfo field. </summary>   
        public string ChargeOtherInfo { get; set; }

        /// <summary>Gets or sets the ChargeOriginalAmount field. </summary>   
        public decimal? ChargeOriginalAmount { get; set; }

        /// <summary>Gets or sets the ChargeRetiredAmount field. </summary>   
        public decimal? ChargeRetiredAmount { get; set; } 

        /// <summary>Gets or sets the CustomerID field. </summary>   
        public bool IsChargeDisputed { get; set; } // IsChargeDisputed
    }
}
