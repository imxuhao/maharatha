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
        public virtual int? VendorId { get; set; }

        [ForeignKey("VendorId")]
        public virtual VendorUnit Vendor { get; set; }

        /// <summary>Gets or sets the BankAccountID field. </summary>   
        public virtual long? BankAccountId { get; set; }

        [ForeignKey("BankAccountId")]
        public virtual BankAccountUnit BankAccount { get; set; }
    }
}
