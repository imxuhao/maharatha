using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Masters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.ChargeEntry
{
    [Table("CAPS_ChargeEntryDocumentUnit")]
    public class ChargeEntryDocumentUnit : AccountingHeaderTransactionsUnit
    {
        ///<summary>Get Sets the BatchId field.</summary>
        public virtual int? BatchId { get; set; }
        [ForeignKey("BatchId")]
        public virtual BatchUnit Batch { get; set; }

        ///<summary>Get Sets the VendorId field.</summary>
        public virtual int? VendorId { get; set; }
        [ForeignKey("VendorId")]
        public virtual VendorUnit Vendor { get; set; }

        ///<summary>Get Sets the TypeOfInvoiceId field.</summary>
        [EnumDataType(typeof(TypeOfInvoice))]
        public virtual TypeOfInvoice TypeOfInvoiceId { get; set; }

        ///<summary>Get Sets the BankAccountId field.</summary>
        public virtual int? BankAccountId { get; set; }
        [ForeignKey("BankAccountId")]
        public virtual BankAccountUnit BankAccount { get; set; }
        
        ///<summary>Get Sets the IsEnterable field.</summary>
        public virtual bool IsEnterable { get; set; }

        ///<summary>Get Sets the ApInvoiceAccountingDocId field.</summary>
        public virtual long? ApInvoiceAccountingDocId { get; set; }

        ///<summary>Get Sets the UploadDocumentLogId field.</summary>}
        public virtual int? UploadDocumentLogId { get; set; }

        ///<summary>Get Sets the IsApInvoiceGenSelected field.</summary>
        public virtual bool? IsApInvoiceGenSelected { get; set; }

        public ChargeEntryDocumentUnit() { }
    }
}
