using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Masters;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.CashEntry
{
    /// <summary>
    /// CAPS_CashEntryDocumentDetail is the new table
    /// </summary>
    [Table("CAPS_CashEntryDocumentDetail")]
    public class CashEntryDocumentDetailUnit : AccountingItemUnit
    {
        /// <summary>Gets or sets the VendorId field. </summary>   
        public int? VendorId { get; set; }

        [ForeignKey("VendorId")]
        public VendorUnit Vendor { get; set; }

        /// <summary>Gets or sets the BankAccountID field. </summary>   
        public long? BankAccountId { get; set; }

        [ForeignKey("BankAccountId")]
        public BankAccountUnit BankAccount { get; set; }
    }
}
