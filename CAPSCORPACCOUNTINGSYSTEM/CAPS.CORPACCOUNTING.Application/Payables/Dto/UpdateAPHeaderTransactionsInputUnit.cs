using CAPS.CORPACCOUNTING.Accounting;
using System;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Payables.Dto
{
    public class UpdateAPHeaderTransactionsInputUnit:AccountingHeaderTransactionInputUnit
    {
        ///<summary>Get Sets the BatchId dfield.</summary>
        public virtual int? BatchId { get; set; }

        ///<summary>Get Sets the VendorId field.</summary>
        public virtual int? VendorId { get; set; }

        ///<summary>Get Sets the TypeOfInvoiceId field.</summary>
        [EnumDataType(typeof(TypeOfInvoice))]
        public virtual TypeOfInvoice TypeOfInvoiceId { get; set; }

        ///<summary>Get Sets the PettyCashAccountId field.</summary>
        public virtual long? PettyCashAccountId { get; set; }


        ///<summary>Get Sets the PaymentTermId field.</summary>
        public virtual int? PaymentTermId { get; set; }


        ///<summary>Get Sets the TypeOfCheckGroupId field.</summary>
        public virtual TypeOfCheckGroup? TypeOfCheckGroupId { get; set; }

        ///<summary>Get Sets the BankAccountId field.</summary>
        public virtual int? BankAccountId { get; set; }

        ///<summary>Get Sets the PaymentDate field.</summary>

        public virtual DateTime? PaymentDate { get; set; }

        ///<summary>Get Sets the PaymentNumber field.</summary>
        public virtual string PaymentNumber { get; set; }

        ///<summary>Get Sets the PurchaseOrderReference field.</summary>
        [StringLength(100)]
        public virtual string PurchaseOrderReference { get; set; }

        ///<summary>Get Sets the ReversedByUserId field.</summary>
        public virtual int? ReversedByUserId { get; set; }

        ///<summary>Get Sets the ReversalDate field.</summary>

        public virtual DateTime? ReversalDate { get; set; }

        ///<summary>Get Sets the IsInvoiceHistory field.</summary>
        public virtual bool IsInvoiceHistory { get; set; }

        ///<summary>Get Sets the IsEnterable field.</summary>
        public virtual bool IsEnterable { get; set; }

        ///<summary>Get Sets the GeneratedAccountingDocumentId field.</summary>
        public virtual long? GeneratedAccountingDocumentId { get; set; }

        ///<summary>Get Sets the UploadDocumentLogID field.</summary>
        public virtual int? UploadDocumentLogID { get; set; }

        ///<summary>Get Sets the BatchInfo field.</summary>
        public virtual string BatchInfo { get; set; }

        ///<summary>Get Sets the PaymentSelectedByUserId field.</summary>
        public virtual int? PaymentSelectedByUserId { get; set; }
    }
}
