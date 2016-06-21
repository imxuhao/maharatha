using CAPS.CORPACCOUNTING.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace CAPS.CORPACCOUNTING.Payables.Dto
{
    /// <summary>
    /// InvoiceEntryDocument InputDto
    /// </summary>
    [AutoMapTo(typeof(ApHeaderTransactions))]
    public class APHeaderTransactionsInputUnit :AccountingHeaderTransactionInputUnit
    {
        ///<summary>Get Sets the BatchId dfield.</summary>
        public int? BatchId { get; set; }

        ///<summary>Get Sets the VendorId field.</summary>
        [Range(1,Int32.MaxValue)]
        public int? VendorId { get; set; }

        ///<summary>Get Sets the TypeOfInvoiceId field.</summary>
        [EnumDataType(typeof(TypeOfInvoice))]
        public TypeOfInvoice TypeOfInvoiceId { get; set; }

        ///<summary>Get Sets the PettyCashAccountId field.</summary>
        public long? PettyCashAccountId { get; set; }


        ///<summary>Get Sets the PaymentTermId field.</summary>
        public int? PaymentTermId { get; set; }


        ///<summary>Get Sets the TypeOfCheckGroupId field.</summary>
        public TypeOfCheckGroup? TypeOfCheckGroupId { get; set; }

        ///<summary>Get Sets the BankAccountId field.</summary>
        public int? BankAccountId { get; set; }

        ///<summary>Get Sets the PaymentDate field.</summary>

        public DateTime? PaymentDate { get; set; }

        ///<summary>Get Sets the PaymentNumber field.</summary>
        public string PaymentNumber { get; set; }

        ///<summary>Get Sets the PurchaseOrderReference field.</summary>
        [StringLength(100)]
        public string PurchaseOrderReference { get; set; }

        ///<summary>Get Sets the ReversedByUserId field.</summary>
        public int? ReversedByUserId { get; set; }

        ///<summary>Get Sets the ReversalDate field.</summary>

        public DateTime? ReversalDate { get; set; }

        ///<summary>Get Sets the IsInvoiceHistory field.</summary>
        public bool IsInvoiceHistory { get; set; }

        ///<summary>Get Sets the IsEnterable field.</summary>
        public bool IsEnterable { get; set; }

        ///<summary>Get Sets the GeneratedAccountingDocumentId field.</summary>
        public long? GeneratedAccountingDocumentId { get; set; }

        ///<summary>Get Sets the UploadDocumentLogID field.</summary>
        public int? UploadDocumentLogID { get; set; }

        ///<summary>Get Sets the BatchInfo field.</summary>
        public string BatchInfo { get; set; }

        ///<summary>Get Sets the PaymentSelectedByUserId field.</summary>
        public int? PaymentSelectedByUserId { get; set; }

        ///<summary>Get Sets the InvoiceEntryDocumentDetailList field.</summary>
        public List<InvoiceEntryDocumentDetailInputUnit> InvoiceEntryDocumentDetailList { get; set; }
    }
}
