using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Masters;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.PurchaseOrders
{
    /// <summary>
    /// PurchaseOrderEntryDocument is the table name in Lajit
    /// </summary>
    [Table("CAPS_PurchaseOrderEntryDocument")]
    public class PurchaseOrderEntryDocumentUnit : AccountingHeaderTransactionsUnit
    {
        #region Declaration of Properties
        /// <summary>Gets or sets the VendorID field. </summary>   
        public virtual int? VendorId { get; set; }

        [ForeignKey("VendorId")]
        public virtual VendorUnit Vendor { get; set; }

        /// <summary>Gets or sets the PaymentTermID field. </summary>  
        public virtual int? PaymentTermId { get; set; }

        [ForeignKey("PaymentTermId")]
        public virtual VendorPaymentTermUnit VendorPaymentTerms { get; set; }

        /// <summary>Gets or sets the BankAccountID field. </summary>  
        public virtual long? BankAccountId { get; set; }

        [ForeignKey("BankAccountId")]
        public virtual BankAccountUnit BankAccount{ get; set; }

        /// <summary>Gets or sets the IsCreditCard field. </summary>  
        public virtual bool IsCreditCard { get; set; }

        /// <summary>Gets or sets the IsShipping field. </summary>  
        public virtual bool IsShipping { get; set; }

        /// <summary>Gets or sets the IsPrintRequired field. </summary>  
        public virtual bool IsPrintRequired { get; set; }
        /// <summary>Gets or sets the IsEnterable field. </summary> 
        public virtual bool IsEnterable { get; set; }
        /// <summary>Gets or sets the IsHistory field. </summary>
        public virtual bool IsHistory { get; set; }

        /// <summary>Gets or sets the IsRetired field. </summary>
        public virtual bool IsRetired { get; set; }

        /// <summary>Gets or sets the SourcePOAccountingDocumentID field. </summary>
        public virtual long? SourcePoAccountingDocumentId { get; set; }

        /// <summary>Gets or sets the InvoiceAccountingDocumentID field. </summary> 
        public virtual long? InvoiceAccountingDocumentId { get; set; }

        /// <summary>Gets or sets the UploadDocumentLogID field. </summary> 
        public virtual long? UploadDocumentLogId { get; set; }

        public virtual UploadDocumentLogUnit UploadDocumentLog { get; set; }

        /// <summary>Gets or sets the IsWillBill field. </summary> 
        public virtual bool? IsWillBill { get; set; }

        /// <summary>Gets or sets the IsAdditionalBill field. </summary> 
        public virtual bool? IsAdditionalBill { get; set; }

        /// <summary>Gets or sets the IsPettyCash field. </summary> 
        public virtual bool? IsPettyCash { get; set; }
       
        /// <summary>Gets or sets the IsPaymentCheck field. </summary> 
        public virtual bool? IsPaymentCheck { get; set; }

        /// <summary>Gets or sets the IsDepositCheck field. </summary> 
        public virtual bool? IsDepositCheck { get; set; }

        /// <summary>Gets or sets the DateNeededBy field. </summary> 
        public virtual DateTime? DateNeededBy { get; set; }

        /// <summary>Gets or sets the TimeNeededBy field. </summary> 
        public virtual string TimeNeededBy { get; set; }

        /// <summary>Gets or sets the IsPartial field. </summary> 
        public virtual bool? IsPartial { get; set; }

        /// <summary>Gets or sets the IsOverage field. </summary> 
        public virtual bool? IsOverage { get; set; }
       
        /// <summary>Gets or sets the IsReimbursement field. </summary> 
        public virtual bool? IsReimbursement { get; set; } // 

        /// <summary>Gets or sets the IsReinstated field. </summary> 
        public virtual bool? IsReinstated { get; set; }
        /// <summary>Gets or sets the ReinstatedPODocumentID field. </summary> 
        public virtual long? ReinstatedPoDocumentId { get; set; }
        
        /// <summary>Gets or sets the ControllingBankAccountID field. </summary> 
        public virtual long? ControllingBankAccountId { get; set; }

        [ForeignKey("ControllingBankAccountId")]
        public virtual BankAccountUnit ControllingBankAccount { get; set; }

        /// <summary>Gets or sets the IsApproveEmail field. </summary> 
        public virtual bool? IsApproveEmail { get; set; } // IsApproveEmail
       
        /// <summary>Gets or sets the POOriginalAmount field. </summary> 
        public virtual decimal? PoOriginalAmount { get; set; }


        /// <summary>Gets or sets the IsClose field. </summary> 
        public virtual bool? IsClose { get; set; }

        /// <summary>Gets or sets the CloseDate field. </summary> 
        public virtual DateTime? CloseDate { get; set; }

        #endregion
        public PurchaseOrderEntryDocumentUnit()
        {
            IsCreditCard = false;
            IsShipping = false;
            IsPrintRequired = false;
            IsEnterable = false;
            IsHistory = false;
            IsRetired = false;
            IsWillBill = false;
            IsAdditionalBill = false;
            IsPettyCash = false;
            IsPaymentCheck = false;
            IsDepositCheck = false;
        }
    }
}
