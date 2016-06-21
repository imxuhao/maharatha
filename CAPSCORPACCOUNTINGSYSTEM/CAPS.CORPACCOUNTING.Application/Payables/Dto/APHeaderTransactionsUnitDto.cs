using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Accounting;
using System;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Payables
{
    /// <summary>
    /// /// <summary>
    /// InvoiceEntryDocument OutputDto
    /// </summary>
    /// </summary>
    [AutoMapFrom(typeof(ApHeaderTransactions))]
    public class APHeaderTransactionsUnitDto : AccountingHeaderTransactionUnitDto
    {
        ///<summary>Get Sets the BatchId dfield.</summary>
        public int? BatchId { get; set; }

        ///<summary>Get Sets the VendorId field.</summary>
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
      
        ///<summary>Get Sets the BatchName field.</summary>
        public string BatchName { get; set; }
        ///<summary>Get Sets the VendorName field.</summary>
        public string VendorName { get; set; }

        ///<summary>Get Sets the VendorName field.</summary>
        public string CreatedUser { get; set; }

        public string PettyCashAccount { get; set; }

        public string PaymentTerm { get; set; }

        public string TypeOfCheckGroup { get; set; }

        public string BankAccount { get; set; }

        public string TypeOfInvoice { get; set; }

    }
}
