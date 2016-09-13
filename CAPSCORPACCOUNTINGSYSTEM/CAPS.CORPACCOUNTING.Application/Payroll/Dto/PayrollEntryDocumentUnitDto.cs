using System;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Payables;

namespace CAPS.CORPACCOUNTING.Payroll.Dto
{

    /// <summary>
    /// OutputDto for PayrollEntryDocumentUnit
    /// </summary>
    [AutoMapFrom(typeof(PayrollEntryDocumentUnit))]
    public class PayrollEntryDocumentUnitDto : AccountingHeaderTransactionUnitDto
    {

        /// <summary>Gets or sets the BatchId field. </summary>
        public int? BatchId { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>
        public int? VendorId { get; set; }


        /// <summary>Gets or sets the TypeOfInvoiceId field. </summary>
        public TypeOfInvoice TypeOfInvoiceId { get; set; }

        /// <summary>Gets or sets the IsEnterable field. </summary>
        public bool IsEnterable { get; set; }

        /// <summary>Gets or sets the APInvoiceAccountingDocId field. </summary>
        public long? APInvoiceAccountingDocId { get; set; }

        /// <summary>Gets or sets the UploadDocumentLogId field. </summary>
        public int? UploadDocumentLogId { get; set; }

        /// <summary>Gets or sets the IsReversed field. </summary>
        public bool? IsReversed { get; set; }

        /// <summary>Gets or sets the ReversedByUserId field. </summary>
        public int? ReversedByUserId { get; set; }

        /// <summary>Gets or sets the ReversalDate field. </summary>

        public DateTime? ReversalDate { get; set; }

        /// <summary>Gets or sets the IsVoid field. </summary>
        public bool? IsVoid { get; set; }

        /// <summary>Gets or sets the IsVoidDateOriginal field. </summary>
        public bool? IsVoidDateOriginal { get; set; }

        /// <summary>Gets or sets the LinkedAccountingDocumentId field. </summary>
        public long? LinkedAccountingDocumentId { get; set; }

        /// <summary>Gets or sets the VendorName field. </summary>
        public string VendorName { get; set; }
    }
}
