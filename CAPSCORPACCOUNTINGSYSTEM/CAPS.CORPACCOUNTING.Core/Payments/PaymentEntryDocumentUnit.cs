using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.PettyCash;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Payments
{
    /// <summary>
    /// PaymentEntryDocument is the table name in Lajit
    /// </summary>
    [Table("CAPS_PaymentEntryDocument")]
    public class PaymentEntryDocumentUnit : AccountingHeaderTransactionsUnit
    {
        /// <summary>
        ///     Maximum length 
        /// </summary>
        public const int MaxNameLength = 200;
       
        /// <summary>
        ///     Maximum length 
        /// </summary>       
        public const int MaxRefLength = 400;

        #region Class Property Declarations
        /// <summary>Gets or sets the PaymentRequestID field. </summary>   
        public virtual int? PaymentRequestId { get; set; }

        [ForeignKey("PaymentRequestId")]
        public PaymentRequestHistotyUnit PaymentHistoryRequest { get; set; }

        /// <summary>Gets or sets the PayToName field. </summary>   
        [StringLength(MaxNameLength)]
        public virtual string PayToName { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>   
        public virtual int? VendorId { get; set; }

        [ForeignKey("VendorId")]
        public VendorUnit Vendor { get; set; }

        /// <summary>Gets or sets the BatchID field. </summary>   
        public virtual int? BatchId { get; set; }
        [ForeignKey("BatchId")]
        public BatchUnit Batch { get; set; }

        /// <summary>Gets or sets the PettyCashAccountID field. </summary>   
        public virtual long? PettyCashAccountId { get; set; }

        [ForeignKey("PettyCashAccountId")]
        public PettyCashAccountUnit PettyCashAccount { get; set; }

        /// <summary>Gets or sets the BankAccountID field. </summary>   
        public virtual long? BankAccountId { get; set; }

        [ForeignKey("BankAccountId")]
        public BankAccountUnit BankAccount { get; set; }

        /// <summary>Gets or sets the TypeOfPaymentMethodID field. </summary>  

        public virtual TypeofPaymentMethod TypeOfPaymentMethodId { get; set; }

        /// <summary>Gets or sets the PaymentNumber field. </summary>   
        [StringLength(MaxNameLength)]
        public virtual string PaymentNumber { get; set; } 

        /// <summary>Gets or sets the PaymentAmount field. </summary>   
        public virtual decimal? PaymentAmount { get; set; }

        /// <summary>Gets or sets the PaymentDate field. </summary>   
        public virtual DateTime? PaymentDate { get; set; }

        /// <summary>Gets or sets the PurchaseOrderReference field. </summary>   
        [StringLength(MaxRefLength)]
        public virtual string PurchaseOrderReference { get; set; } 

        /// <summary>Gets or sets the IsCheckPrintRequired field. </summary>   
        public virtual bool IsCheckPrintRequired { get; set; } 

        /// <summary>Gets or sets the IsRegisterPrinted field. </summary>   
        public virtual bool IsRegisterPrinted { get; set; }

        /// <summary>Gets or sets the IsReversed field. </summary>   
        public virtual bool IsReversed { get; set; }

        /// <summary>Gets or sets the ReversedByUserID field. </summary>   
        public virtual int? ReversedByUserId { get; set; }

        /// <summary>Gets or sets the ReversalDate field. </summary> 
        public virtual DateTime? ReversalDate { get; set; }
        
        /// <summary>Gets or sets the IsVoid field. </summary> 
        public virtual bool IsVoid { get; set; }

        /// <summary>Gets or sets the IsVoidDateOriginal field. </summary> 
        public virtual bool IsVoidDateOriginal { get; set; }

        /// <summary>Gets or sets the LinkedAccountingDocumentId field. </summary> 
        public virtual long? LinkedAccountingDocumentId { get; set; }

        /// <summary>Gets or sets the ReconciliationID field. </summary> 
        public virtual int? ReconciliationId { get; set; }

        public virtual BankRecControlUnit BankRecControl { get; set; }

        /// <summary>Gets or sets the ReissueBatchID field. </summary> 
        public virtual int? ReissueBatchId { get; set; }

        /// <summary>Gets or sets the ReissueVoidDate field. </summary> 
        public virtual DateTime? ReissueVoidDate { get; set; }

        /// <summary>Gets or sets the IsEnterable field. </summary> 
        public virtual bool IsEnterable { get; set; }

        /// <summary>Gets or sets the UploadDocumentLogId field. </summary> 
        public virtual long? UploadDocumentLogId { get; set; } 

        [ForeignKey("UploadDocumentLogId")]
        public UploadDocumentLogUnit UploadDocumentLog { get; set; }


        /// <summary>Gets or sets the ARAccountingDocID field. </summary> 
        public virtual long? ArAccountingDocId { get; set; }
        #endregion

        public PaymentEntryDocumentUnit()
        {
            IsCheckPrintRequired = false;
            IsRegisterPrinted = false;
            IsReversed = false;
            IsVoid = false;
            IsVoidDateOriginal = false;
            IsEnterable = false;
        }
    }
}
