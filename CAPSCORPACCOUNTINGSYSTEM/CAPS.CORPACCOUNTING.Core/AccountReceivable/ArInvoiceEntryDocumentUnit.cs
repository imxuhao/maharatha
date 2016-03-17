using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Banking;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace CAPS.CORPACCOUNTING.AccountReceivable
{
    /// <summary>
    /// ArInvoiceEntryDocument is the table name in lajit
    /// </summary>
    [Table("CAPS_ARInvoiceEntryDocument")]
   public class ArInvoiceEntryDocumentUnit: AccountingHeaderTransactionsUnit
    {
        ///<summary>Get Sets the BatchId field.</summary>
        public virtual int? BatchId { get; set; }

        [ForeignKey("BatchId")]
        public virtual BatchUnit Batch { get; set; }
        ///<summary>Get Sets the CustomerId field.</summary>
        public virtual int CustomerId { get; set; }

        ///<summary>Get Sets the ArytdInvoiceId field.</summary>
        public virtual int? ArytdInvoiceId { get; set; }

        ///<summary>Get Sets the InvoiceNotes field.</summary>
        public virtual string InvoiceNotes { get; set; }

        ///<summary>Get Sets the SalesRepId field.</summary>
        public virtual int? SalesRepId { get; set; }

        ///<summary>Get Sets the ArPaymentTermId field.</summary>
        public virtual int? ArPaymentTermId { get; set; }

        ///<summary>Get Sets the ContactAddressId field.</summary>
        public virtual int? ContactAddressId { get; set; }

        ///<summary>Get Sets the TypeOfInvoiceId field.</summary>
        public virtual int? TypeOfInvoiceId { get; set; }

        ///<summary>Get Sets the IsInvoiceHistory field.</summary>
        public virtual bool IsInvoiceHistory { get; set; }

        ///<summary>Get Sets the IsEnterable field.</summary>
        public virtual bool IsEnterable { get; set; }

        ///<summary>Get Sets the ReversedByUserId field.</summary>
        public virtual int? ReversedByUserId { get; set; }

        ///<summary>Get Sets the BatchId field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime ReversalDate { get; set; }
       
        ///<summary>Get Sets the BatchId field.</summary>
        public virtual long? GroupBillingAccountingDocumentId { get; set; }
       
        ///<summary>Get Sets the BatchId field.</summary>
        public virtual short? GroupBillingSequence { get; set; }
        
        ///<summary>Get Sets the BatchId field.</summary>
        public virtual bool IsProductionDetailsPrinted { get; set; }
        
        ///<summary>Get Sets the BatchId field.</summary>
        public virtual string BatchInfo { get; set; }
       
        ///<summary>Get Sets the BatchId field.</summary>
        public virtual string PurchaseOrderReference { get; set; }


        public ArInvoiceEntryDocumentUnit()
        {
        }

    }
}
