using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Accounting;
using System;


namespace CAPS.CORPACCOUNTING.AccountReceivable.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(ArInvoiceEntryDocumentUnit))]
    public class ArInvoiceEntryDocumentUnitDto : AccountingHeaderTransactionUnitDto
    {
        ///<summary>Get Sets the BatchId field.</summary>
        public  int? BatchId { get; set; }

        ///<summary>Get Sets the BatchDescription field.</summary>
        public string BatchDescription { get; set; }
        

        ///<summary>Get Sets the CustomerId field.</summary>
        public  int CustomerId { get; set; }


        ///<summary>Get Sets the CustomerName field.</summary>
        public int CustomerName { get; set; }

        ///<summary>Get Sets the ArytdInvoiceId field.</summary>
        public  int? ArytdInvoiceId { get; set; }

        ///<summary>Get Sets the InvoiceNotes field.</summary>
        public  string InvoiceNotes { get; set; }

        ///<summary>Get Sets the SalesRepId field.</summary>
        public  int? SalesRepId { get; set; }

        /// <summary>Gets or sets the SalesRepName field. </summary>
        public string SalesRepName { get; set; }

        ///<summary>Get Sets the ArPaymentTermId field.</summary>
        public  int? ArPaymentTermId { get; set; }

        ///<summary>Get Sets the ArPaymentTerm field.</summary>
        public string ArPaymentTerm { get; set; }

        ///<summary>Get Sets the ContactAddressId field.</summary>
        public  int? ContactAddressId { get; set; }

        ///<summary>Get Sets the TypeOfInvoiceId field.</summary>
        public  int? TypeOfInvoiceId { get; set; }

        ///<summary>Get Sets the TypeOfInvoiceId field.</summary>
        public string TypeOfInvoice { get; set; }

        ///<summary>Get Sets the IsInvoiceHistory field.</summary>
        public  bool IsInvoiceHistory { get; set; }

        ///<summary>Get Sets the IsEnterable field.</summary>
        public  bool IsEnterable { get; set; }

        ///<summary>Get Sets the ReversedByUserId field.</summary>
        public  int? ReversedByUserId { get; set; }

        ///<summary>Get Sets the ReversalDate field.</summary>
        public DateTime ReversalDate { get; set; }

        ///<summary>Get Sets the GroupBillingAccountingDocumentId field.</summary>
        public long? GroupBillingAccountingDocumentId { get; set; }

        ///<summary>Get Sets the GroupBillingSequence field.</summary>
        public short? GroupBillingSequence { get; set; }

        ///<summary>Get Sets the IsProductionDetailsPrinted field.</summary>
        public bool IsProductionDetailsPrinted { get; set; }

        ///<summary>Get Sets the BatchInfo field.</summary>
        public string BatchInfo { get; set; }

        ///<summary>Get Sets the PurchaseOrderReference field.</summary>
        public string PurchaseOrderReference { get; set; }

        ///<summary>Get Sets the jobNumber field.</summary>
        public string JobNumber { get; set; }

        ///<summary>Get Sets the jobName field.</summary>
        public string JobName { get; set; }

    }
}
