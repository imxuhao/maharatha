using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Banking;
using System.ComponentModel.DataAnnotations;
using System;
using CAPS.CORPACCOUNTING.Masters;
using Abp.AutoMapper;

namespace CAPS.CORPACCOUNTING.Accounting
{
    [AutoMapFrom(typeof(AccountingHeaderTransactionsUnit))]
    public class AccountingHeaderTransactionUnitDto : IOutputDto
    {
        /// <summary>Gets or sets the AHTID field.</summary>
        public long AccountingDocumentId { get; set; }

        /// <summary>Get Sets the Description Date </summary>
        public virtual string Description { get; set; }

        /// <summary>Get Sets the TypeOfAccountingDocumentID  </summary>
        public virtual TypeOfAccountingDocument TypeOfAccountingDocumentId { get; set; }

        /// <summary>Get Sets the TypeOfAccountingDocument  </summary>
        public virtual string TypeOfAccountingDocument { get; set; }

        /// <summary>Get Sets the TypeOfObjectId field. </summary>
        public virtual TypeofObject? TypeOfObjectId { get; set; }

        /// <summary>Get Sets the TypeOfObject field. </summary>
        public virtual string TypeOfObject { get; set; }

        /////// <summary>Get Sets the DocumentLinkId field. </summary>
        ////public virtual long? DocumentLinkId { get; set; }

        /////// <summary>Get Sets the PostingCycleId field. </summary>
        ////public virtual int? PostingCycleId { get; set; }

        /// <summary>Get Sets the RecurDocId field.</summary>
        public virtual long? RecurDocId { get; set; }

        /// <summary>Get Sets the RecurDoc field.</summary>
        public virtual string RecurDoc { get; set; }

        /// <summary>Get Sets the ReverseDocId field.</summary>
        public virtual long? ReverseDocId { get; set; }

        /// <summary>Get Sets the ReverseDoc field.</summary>
        public virtual string ReverseDoc { get; set; }

        /// <summary>Get Sets the DocumentDate field.</summary>        
        public virtual DateTime? DocumentDate { get; set; }

        /// <summary>Get Sets the TransactionDate field.</summary>      
        public virtual DateTime TransactionDate { get; set; }

        // [Column(TypeName = "smalldatetime")]
        //  public virtual DateTime? DateCreated { get; set; }

        /// <summary>Get Sets the DatePosted field.</summary>

        public virtual DateTime? DatePosted { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public virtual DateTime? DateChanged { get; set; }

        /// <summary>Get Sets the OriginalDocumentId field.</summary>
        public virtual long? OriginalDocumentId { get; set; }

        /// <summary>Get Sets the OriginalDocument field.</summary>
        public virtual long? OriginalDocument { get; set; }

        /// <summary>Get Sets the ControlTotal field.</summary>
        public virtual decimal? ControlTotal { get; set; }

        /// <summary>Get Sets the DocumentReference field.</summary>
        [StringLength(AccountingHeaderTransactionsUnit.MaxLength)]
        public virtual string DocumentReference { get; set; }

        /// <summary>Get Sets the VoucherReference field.</summary>
        [StringLength(AccountingHeaderTransactionsUnit.MaxLength)]
        public virtual string VoucherReference { get; set; }

        /// <summary>Get Sets the TypeOfCurrencyId field.</summary>
        public virtual short? TypeOfCurrencyId { get; set; }

        /// <summary>Get Sets the TypeOfCurrency field.</summary>
        public virtual string TypeOfCurrency { get; set; }

        /// <summary>Get Sets the CurrencyAdjustmentId field.</summary>
        public virtual int? CurrencyAdjustmentId { get; set; }

        /// <summary>Get Sets the CurrencyAdjustment field.</summary>
        public virtual string CurrencyAdjustment { get; set; }

        /// <summary>Get Sets the PostBatchDescription field.</summary>
        public virtual string PostBatchDescription { get; set; }

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

        // <summary>Get Sets the BankRecControl field.</summary>
        public virtual string BankRecControl { get; set; }

        // <summary>Get Sets the IsSelected field.</summary>
        public virtual bool IsSelected { get; set; }

        // <summary>Get Sets the IsActive field.</summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Get Sets the IsApproved field.</summary>
        public virtual bool IsApproved { get; set; }

        /// <summary>Get Sets the TypeOfInactiveStatusId field.</summary>
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Get Sets the TypeOfInactiveStatus field.</summary>
        public virtual string TypeOfInactiveStatus { get; set; }

        /// <summary>Get Sets the IsBankRecOmitted field.</summary>
        public virtual bool? IsBankRecOmitted { get; set; }

        /// <summary>Get Sets the IsICTJournal field.</summary>
        public virtual bool? IsICTJournal { get; set; }

        ///<summary>Get Sets the ICTCompanyId field.</summary>
        public virtual int? ICTCompanyId { get; set; }

        ///<summary>Get Sets the ICTCompany field.</summary>
        public virtual string ICTCompany { get; set; }

        /// <summary>Get Sets the ICTAccountingDocumentId field.</summary>
        public virtual long? ICTAccountingDocumentId { get; set; }

        /// <summary>Get Sets the ICTAccountingDocument field.</summary>
        public virtual string ICTAccountingDocument { get; set; }

        /// <summary>Get Sets the CurrencyOverrideRate field.</summary>
        public virtual double? CurrencyOverrideRate { get; set; }

        /// <summary>Get Sets the FunctionalCurrencyControlTotal field.</summary>
        public virtual decimal? FunctionalCurrencyControlTotal { get; set; }

        /// <summary>Get Sets the TypeOfCurrencyRateId field.</summary>
        public virtual short? TypeOfCurrencyRateId { get; set; }

        /// <summary>Get Sets the TypeOfCurrencyRate field.</summary>
        public virtual string TypeOfCurrencyRate { get; set; }

        /// <summary>Get Sets the MemoLine field.</summary>
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
        public virtual long? OrganizationUnitId { get; set; }
    }
}
