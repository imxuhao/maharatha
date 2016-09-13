﻿using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.PurchaseOrders;
using System;


namespace CAPS.CORPACCOUNTING.PurchaseOrders.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(PurchaseOrderEntryDocumentUnit))]
    public class PurchaseOrderEntryDocumentUnitDto : AccountingHeaderTransactionUnitDto
    {
        /// <summary>Gets or sets the VendorID field. </summary>   
        public  int? VendorId { get; set; }

        /// <summary>Gets or sets the VendorName field. </summary>   
        public  string VendorName { get; set; }

        /// <summary>Gets or sets the PaymentTermID field. </summary>  
        public  int? PaymentTermId { get; set; }

        /// <summary>Gets or sets the PaymentTermID field. </summary>  
        public  string PaymentTerms { get; set; }

        /// <summary>Gets or sets the BankAccountID field. </summary>  
        public  long? BankAccountId { get; set; }

        /// <summary>Gets or sets the BankAccountNumber field. </summary>  
        public  string BankAccountNumber { get; set; }

        /// <summary>Gets or sets the IsCreditCard field. </summary>  
        public  bool IsCreditCard { get; set; }

        /// <summary>Gets or sets the IsShipping field. </summary>  
        public  bool IsShipping { get; set; }

        /// <summary>Gets or sets the IsPrintRequired field. </summary>  
        public  bool IsPrintRequired { get; set; }
        /// <summary>Gets or sets the IsEnterable field. </summary> 
        public  bool IsEnterable { get; set; }
        /// <summary>Gets or sets the IsHistory field. </summary>
        public  bool IsHistory { get; set; }

        /// <summary>Gets or sets the IsRetired field. </summary>
        public  bool IsRetired { get; set; }

        /// <summary>Gets or sets the SourcePOAccountingDocumentID field. </summary>
        public  long? SourcePoAccountingDocumentId { get; set; }

        /// <summary>Gets or sets the InvoiceAccountingDocumentID field. </summary> 
        public  long? InvoiceAccountingDocumentId { get; set; }

        /// <summary>Gets or sets the UploadDocumentLogID field. </summary> 
        public  long? UploadDocumentLogId { get; set; }

        /// <summary>Gets or sets the UploadDocumentLogID field. </summary> 
        public  string UploadDocumentLogDesc { get; set; }

        /// <summary>Gets or sets the IsWillBill field. </summary> 
        public  bool? IsWillBill { get; set; }

        /// <summary>Gets or sets the IsAdditionalBill field. </summary> 
        public  bool? IsAdditionalBill { get; set; }

        /// <summary>Gets or sets the IsPettyCash field. </summary> 
        public  bool? IsPettyCash { get; set; }

        /// <summary>Gets or sets the IsPaymentCheck field. </summary> 
        public  bool? IsPaymentCheck { get; set; }

        /// <summary>Gets or sets the IsDepositCheck field. </summary> 
        public  bool? IsDepositCheck { get; set; }

        /// <summary>Gets or sets the DateNeededBy field. </summary> 
        public  DateTime? DateNeededBy { get; set; }

        /// <summary>Gets or sets the TimeNeededBy field. </summary> 
        public  string TimeNeededBy { get; set; }

        /// <summary>Gets or sets the IsPartial field. </summary> 
        public  bool? IsPartial { get; set; }

        /// <summary>Gets or sets the IsOverage field. </summary> 
        public  bool? IsOverage { get; set; }

        /// <summary>Gets or sets the IsReimbursement field. </summary> 
        public  bool? IsReimbursement { get; set; } // 

        /// <summary>Gets or sets the IsReinstated field. </summary> 
        public  bool? IsReinstated { get; set; }
        /// <summary>Gets or sets the ReinstatedPODocumentID field. </summary> 
        public  long? ReinstatedPoDocumentId { get; set; }

        /// <summary>Gets or sets the ControllingBankAccountID field. </summary> 
        public  long? ControllingBankAccountId { get; set; }

        /// <summary>Gets or sets the ControllingBankAccountNumber field. </summary> 
        public  string ControllingBankAccountNumber { get; set; }

        /// <summary>Gets or sets the IsApproveEmail field. </summary> 
        public  bool? IsApproveEmail { get; set; } // IsApproveEmail

        /// <summary>Gets or sets the POOriginalAmount field. </summary> 
        public  decimal? PoOriginalAmount { get; set; }

        /// <summary>Gets or sets the JobNumber field. </summary> 
        public string JobNumber { get; set; }

        /// <summary>Gets or sets the AccountNumber field. </summary> 
        public string AccountNumber { get; set; }
   
        /// <summary>
        /// Gets or sets CreatedUser
        /// </summary>
        public string CreatedUser { get; set; }

        /// <summary>
        /// Gets or sets ApprovedBy
        /// </summary>
        public string ApprovedBy { get; set; }

        /// <summary>Get Sets the RemainingBalance field.</summary>
        public virtual decimal? RemainingBalance { get; set; }

        /// <summary>Get Sets the PendingTrans field.</summary>
        public virtual decimal? PendingTrans { get; set; }

        /// <summary>Gets or sets the IsClose field. </summary> 
        public bool? IsClose { get; set; }

        /// <summary>Gets or sets the CloseDate field. </summary> 
        public DateTime? CloseDate { get; set; }


    }
}
