using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Accounting;

namespace CAPS.CORPACCOUNTING.Payroll.Dto
{
    /// <summary>
    /// Create InputDto for PayrollEntryDocumentUnit
    /// </summary>
    [AutoMapTo(typeof(PayrollEntryDocumentUnit))]
    public class PayrollEntryDocumentInputUnit : AccountingHeaderTransactionInputUnit
    {
        /// <summary>Gets or sets the BatchId field. </summary>
        public  int? BatchId { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>
        public  int? VendorId { get; set; }


        /// <summary>Gets or sets the TypeOfInvoiceId field. </summary>

        [EnumDataType(typeof(TypeOfInvoice))]
        public  TypeOfInvoice TypeOfInvoiceId { get; set; }

        /// <summary>Gets or sets the IsEnterable field. </summary>
        public  bool IsEnterable { get; set; }

        /// <summary>Gets or sets the APInvoiceAccountingDocId field. </summary>
        public  long? APInvoiceAccountingDocId { get; set; }

        /// <summary>Gets or sets the UploadDocumentLogId field. </summary>
        public  int? UploadDocumentLogId { get; set; }

        /// <summary>Gets or sets the IsReversed field. </summary>
        public  bool? IsReversed { get; set; }

        /// <summary>Gets or sets the ReversedByUserId field. </summary>
        public  int? ReversedByUserId { get; set; }

        /// <summary>Gets or sets the ReversalDate field. </summary>
       
        public  DateTime? ReversalDate { get; set; }

        /// <summary>Gets or sets the IsVoid field. </summary>
        public  bool? IsVoid { get; set; }

        /// <summary>Gets or sets the IsVoidDateOriginal field. </summary>
        public  bool? IsVoidDateOriginal { get; set; }

        /// <summary>Gets or sets the LinkedAccountingDocumentId field. </summary>
        public  long? LinkedAccountingDocumentId { get; set; }

        /// <summary>Gets or sets the PayrollEntryDocumentDetailInputList field. </summary>
        public List<PayrollEntryDocumentDetailInputUnit> PayrollEntryDocumentDetailInputList { get; set; }
    }
}
