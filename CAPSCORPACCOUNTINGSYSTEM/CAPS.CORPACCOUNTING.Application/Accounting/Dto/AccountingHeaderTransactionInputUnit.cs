using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Masters;
using System;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Accounting
{
    /// <summary>
    /// Updates UpdateAccountingHeaderTransactionInputUnit
    /// </summary>
    public class AccountingHeaderTransactionInputUnit : IInputDto
    {

        /// <summary>Gets or sets the AHTID field.</summary>
        public int AccountingDocumentId { get; set; }

        /// <summary>Get Sets the Description Date </summary>
        [Required]
        public virtual string Description { get; set; }

        /// <summary>Get Sets the TypeOfAccountingDocumentID Date </summary>
        [EnumDataType(typeof(TypeOfAccountingDocument))]
        public virtual TypeOfAccountingDocument TypeOfAccountingDocumentId { get; set; }

        /// <summary>Get Sets the TypeOfObjectId field. </summary>
        public virtual TypeofObject? TypeOfObjectId { get; set; }

        /////// <summary>Get Sets the DocumentLinkId field. </summary>
        ////public virtual long? DocumentLinkId { get; set; }

        /////// <summary>Get Sets the PostingCycleId field. </summary>
        ////public virtual int? PostingCycleId { get; set; }

        /// <summary>Get Sets the RecurDocId field.</summary>
        public virtual long? RecurDocId { get; set; }

        /// <summary>Get Sets the ReverseDocId field.</summary>
        public virtual long? ReverseDocId { get; set; }

        /// <summary>Get Sets the DocumentDate field.</summary>        
        public virtual DateTime? DocumentDate { get; set; }

        /// <summary>Get Sets the TransactionDate field.</summary>      
        [Required(ErrorMessage = "TransactionDate is Required.")]
        [DataType(DataType.Date)]
        public virtual DateTime? TransactionDate { get; set; }

        // [Column(TypeName = "smalldatetime")]
        //  public virtual DateTime? DateCreated { get; set; }

        /// <summary>Get Sets the DatePosted field.</summary>

        public virtual DateTime? DatePosted { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public virtual DateTime? DateChanged { get; set; }

        /// <summary>Get Sets the OriginalDocumentId field.</summary>
        public virtual long? OriginalDocumentId { get; set; }

        /// <summary>Get Sets the ControlTotal field.</summary>
        public virtual decimal? ControlTotal { get; set; }

        /// <summary>Get Sets the DocumentReference field.</summary>
        [Required]
        [StringLength(AccountingHeaderTransactionsUnit.MaxLength)]
        public virtual string DocumentReference { get; set; }

        /// <summary>Get Sets the VoucherReference field.</summary>
        [StringLength(AccountingHeaderTransactionsUnit.MaxLength)]
        public virtual string VoucherReference { get; set; }

        /// <summary>Get Sets the TypeOfCurrencyId field.</summary>
        [Range(1,Int16.MaxValue,ErrorMessage = "Currency is Required.")]
        public virtual short? TypeOfCurrencyId { get; set; }

        /// <summary>Get Sets the CurrencyAdjustmentId field.</summary>
        public virtual int? CurrencyAdjustmentId { get; set; }

        /// <summary>Get Sets the PostBatchDescription field.</summary>
        [StringLength(AccountingHeaderTransactionsUnit.MaxLength)]
        public virtual string PostBatchDescription { get; set; }

        ////public virtual bool IsNewActivityPrinted { get; set; }

        /// <summary>Get Sets the IsPosted field.</summary>
        public bool IsPosted { get; set; }

        /// <summary>Get Sets the IsAutoPosted field.</summary>
        public virtual bool IsAutoPosted { get; set; }

        /// <summary>Get Sets the IsChanged field.</summary>
        public virtual bool IsChanged { get; set; }

        /// <summary>Get Sets the PostedByUserId field.</summary>
        public virtual int? PostedByUserId { get; set; }


        // <summary>Get Sets the BankRecControlId field.</summary>
        public virtual int? BankRecControlId { get; set; }

        // <summary>Get Sets the IsSelected field.</summary>
        public virtual bool IsSelected { get; set; }

        // <summary>Get Sets the IsActive field.</summary>
        public virtual bool IsActive { get; set; }

        // <summary>Get Sets the IsApproved field.</summary>
        public virtual bool IsApproved { get; set; }

        // <summary>Get Sets the TypeOfInactiveStatusId field.</summary>
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Get Sets the IsBankRecOmitted field.</summary>
        public virtual bool? IsBankRecOmitted { get; set; }

        /// <summary>Get Sets the IsICTJournal field.</summary>
        public virtual bool? IsICTJournal { get; set; }

        ///<summary>Get Sets the ICTCompanyId field.</summary>
        public virtual int? ICTCompanyId { get; set; }

        /// <summary>Get Sets the ICTAccountingDocumentId field.</summary>
        public virtual long? ICTAccountingDocumentId { get; set; }

        /// <summary>Get Sets the CurrencyOverrideRate field.</summary>
        public virtual double? CurrencyOverrideRate { get; set; }

        /// <summary>Get Sets the FunctionalCurrencyControlTotal field.</summary>
        public virtual decimal? FunctionalCurrencyControlTotal { get; set; }

        /// <summary>Get Sets the TypeOfCurrencyRateId field.</summary>
        public virtual short? TypeOfCurrencyRateId { get; set; }

        /// <summary>Get Sets the MemoLine field.</summary>
        [StringLength(AccountingHeaderTransactionsUnit.MaxLength)]
        public virtual string MemoLine { get; set; }

        /// <summary>Get Sets the Is13Period field.</summary>
        public virtual bool? Is13Period { get; set; }

        /// <summary>Get Sets the HomeCurrencyAmount field.</summary>
        public virtual decimal? HomeCurrencyAmount { get; set; }

        /// <summary>Get Sets the CustomForexRate field.</summary>       
        public virtual decimal? CustomForexRate { get; set; }

        /// <summary>Get Sets the IsPOSubmitForApproval field.</summary>
        public virtual bool IsPOSubmitForApproval { get; set; }

        /// <summary>Get Sets the IsCPASTran field.</summary>
        public virtual bool? IsCPASTran { get; set; }

        /// <summary>Get Sets the CPASProjCloseId field.</summary>
        public virtual int? CPASProjCloseId { get; set; }

        /// <summary>Get Sets the CPASProjId field.</summary>
        public virtual int? CPASProjId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        [Range(1,Int64.MaxValue,ErrorMessage = "Please setup the Organization.")]
        public virtual long OrganizationUnitId { get; set; }
    }
}
