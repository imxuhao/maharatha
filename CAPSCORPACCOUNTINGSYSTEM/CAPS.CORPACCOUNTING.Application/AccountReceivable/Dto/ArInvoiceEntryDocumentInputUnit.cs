using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.AccountReceivable.Dto
{

    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(ArInvoiceEntryDocumentUnit))]
    public class ArInvoiceEntryDocumentInputUnit : AccountingHeaderTransactionInputUnit
    {
        ///<summary>Get Sets the BatchId field.</summary>
        public  int? BatchId { get; set; }

        ///<summary>Get Sets the CustomerId field.</summary>
        [Required]
        [Range(Int32.MinValue, Int32.MaxValue)]
        public  int CustomerId { get; set; }

        ///<summary>Get Sets the ArytdInvoiceId field.</summary>
        public  int? ArytdInvoiceId { get; set; }

        ///<summary>Get Sets the InvoiceNotes field.</summary>
        public  string InvoiceNotes { get; set; }

        ///<summary>Get Sets the SalesRepId field.</summary>
        public  int? SalesRepId { get; set; }

        ///<summary>Get Sets the ArPaymentTermId field.</summary>
        public  int? ArPaymentTermId { get; set; }

        ///<summary>Get Sets the ContactAddressId field.</summary>
        public  int? ContactAddressId { get; set; }

        ///<summary>Get Sets the TypeOfInvoiceId field.</summary>
        public  int? TypeOfInvoiceId { get; set; }

        ///<summary>Get Sets the IsInvoiceHistory field.</summary>
        public  bool IsInvoiceHistory { get; set; }

        ///<summary>Get Sets the IsEnterable field.</summary>
        public  bool IsEnterable { get; set; }

        ///<summary>Get Sets the ReversedByUserId field.</summary>
        public  int? ReversedByUserId { get; set; }

        ///<summary>Get Sets the BatchId field.</summary>
        [Column(TypeName = "smalldatetime")]
        public  DateTime? ReversalDate { get; set; }

        ///<summary>Get Sets the BatchId field.</summary>
        public  long? GroupBillingAccountingDocumentId { get; set; }

        ///<summary>Get Sets the BatchId field.</summary>
        public  short? GroupBillingSequence { get; set; }

        ///<summary>Get Sets the BatchId field.</summary>
        public  bool IsProductionDetailsPrinted { get; set; }

        ///<summary>Get Sets the BatchId field.</summary>
        public  string BatchInfo { get; set; }

        ///<summary>Get Sets the BatchId field.</summary>
        public  string PurchaseOrderReference { get; set; }

        ///<summary>Get Sets the IsStartUp field.</summary>
        public bool IsStartUp { get; set; }

        ///<summary>Get Sets the InvoiceEntryDocumentDetailList field.</summary>
        public List<ArInvoiceEntryDetailInputUnit> InvoiceEntryDocumentDetailList { get; set; }
    }
}
