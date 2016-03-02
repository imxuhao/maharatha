using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Payables;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace CAPS.CORPACCOUNTING.Payroll
{
    [Table("CAPS_PayrollEntryDocument")]
    public class PayrollEntryDocumentUnit : AccountingHeaderTransactionsUnit
    {
        /// <summary>Gets or sets the BatchId field. </summary>
        public virtual int? BatchId { get; set; }

        [ForeignKey("BatchId")]
        public virtual BatchUnit Batch { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>
        public virtual int? VendorId { get; set; }

        [ForeignKey("VendorId")]
        public virtual VendorUnit Vendor { get; set; }

        /// <summary>Gets or sets the TypeOfInvoiceId field. </summary>
        [EnumDataType(typeof(TypeOfInvoice))]
        public virtual TypeOfInvoice TypeOfInvoiceId { get; set; }

        /// <summary>Gets or sets the IsEnterable field. </summary>
        public virtual bool IsEnterable { get; set; }

        /// <summary>Gets or sets the APInvoiceAccountingDocId field. </summary>
        public virtual long? APInvoiceAccountingDocId { get; set; }

        /// <summary>Gets or sets the UploadDocumentLogId field. </summary>
        public virtual int? UploadDocumentLogId { get; set; }

        /// <summary>Gets or sets the IsReversed field. </summary>
        public virtual bool? IsReversed { get; set; }

        /// <summary>Gets or sets the ReversedByUserId field. </summary>
        public virtual int? ReversedByUserId { get; set; }

        /// <summary>Gets or sets the ReversalDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual SqlDateTime? ReversalDate { get; set; }

        /// <summary>Gets or sets the IsVoid field. </summary>
        public virtual bool? IsVoid { get; set; }

        /// <summary>Gets or sets the IsVoidDateOriginal field. </summary>
        public virtual bool? IsVoidDateOriginal { get; set; }

        /// <summary>Gets or sets the LinkedAccountingDocumentId field. </summary>
        public virtual long? LinkedAccountingDocumentId { get; set; }

    }
}
