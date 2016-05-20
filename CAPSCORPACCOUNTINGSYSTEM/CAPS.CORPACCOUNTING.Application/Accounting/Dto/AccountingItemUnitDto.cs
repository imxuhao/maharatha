using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Banking;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using CAPS.CORPACCOUNTING.Masters;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.JobCosting;

namespace CAPS.CORPACCOUNTING.Accounting.Dto
{
    [AutoMapFrom(typeof(AccountingItemUnit))]
    public class AccountingItemUnitDto : IOutputDto
    {
        /// <summary>Overriding the Id column with AccountingItemId </summary>
        public long AccountingItemId { get; set; }

        /// <summary>Gets or sets the AccountingDocumentID field. </summary>
        public long? AccountingDocumentId { get; set; }

        /// <summary>Gets or sets the TypeOfTransactionID field. </summary>
        public TypeofTransaction TypeOfTransactionId { get; set; }

        /// <summary>Gets or sets the TypeOfTransaction field. </summary>
        public string TypeOfTransaction { get; set; }

        /// <summary>Gets or sets the TypeOfAmountID field. </summary>
        public TypeOfAmount TypeOfAmountId { get; set; }

        /// <summary>Gets or sets the TypeOfAmount  field. </summary>
        public string TypeOfAmount { get; set; }

        /// <summary>Gets or sets the ItemLinkID field. </summary>
        public long? ItemLinkId { get; set; }

        /// <summary>Gets or sets the ItemLink field. </summary>
        public string ItemLink { get; set; }

        /// <summary>Gets or sets the LedgerReference field. </summary>       
        public string LedgerReference { get; set; }
       
        [Column(TypeName = "smalldatetime")]
        public DateTime? LedgerDate { get; set; }

        /// <summary>Gets or sets the LedgerYYYYMM field. </summary>
        public int? LedgerYyyymm { get; set; }

        /// <summary>Gets or sets the AccountID field. </summary>
        public long AccountId { get; set; }

        /// <summary>Gets or sets the Account field. </summary>
        public string Account { get; set; }

        /// <summary>Gets or sets the JobID field. </summary>
        public int JobId { get; set; }

        /// <summary>Gets or sets the Job field. </summary>
        public string Job { get; set; }

        /// <summary>Gets or sets the ItemMemo field. </summary>   
        public string ItemMemo { get; set; }

        /// <summary>Gets or sets the IsAsset field. </summary>
        public bool IsAsset { get; set; }

        /// <summary>Gets or sets the AccountRef1 field. </summary>        
        public string AccountRef1 { get; set; }

        /// <summary>Gets or sets the AccountRef2 field. </summary>      
        public string AccountRef2 { get; set; }

        /// <summary>Gets or sets the AccountRef3 field. </summary>
        public string AccountRef3 { get; set; }

        /// <summary>Gets or sets the AccountRef4 field. </summary>       
        public string AccountRef4 { get; set; }

        /// <summary>Gets or sets the AccountRef5 field. </summary>       
        public string AccountRef5 { get; set; }

        /// <summary>Gets or sets the AccountRef6 field. </summary>      
        public string AccountRef6 { get; set; }

        /// <summary>Gets or sets the AccountRef7 field. </summary>       
        public string AccountRef7 { get; set; }

        /// <summary>Gets or sets the AccountRef8 field. </summary>      
        public string AccountRef8 { get; set; }

        /// <summary>Gets or sets the AccountRef9 field. </summary>        
        public string AccountRef9 { get; set; }

        /// <summary>Gets or sets the AccountRef10 field. </summary>        
        public string AccountRef10 { get; set; }

        /// <summary>Gets or sets the SubAccountID1 field. </summary>
        public long? SubAccountId1 { get; set; }

        /// <summary>Gets or sets the SubAccount1 field. </summary>
        public string SubAccount1 { get; set; }

        /// <summary>Gets or sets the SubAccountID2 field. </summary>
        public long? SubAccountId2 { get; set; }

        /// <summary>Gets or sets the SubAccount2 field. </summary>
        public string SubAccount2 { get; set; }

        /// <summary>Gets or sets the SubAccountID3 field. </summary>
        public long? SubAccountId3 { get; set; }

        /// <summary>Gets or sets the SubAccount3 field. </summary>
        public string SubAccount3 { get; set; }

        /// <summary>Gets or sets the SubAccountID4 field. </summary>
        public long? SubAccountId4 { get; set; }


        /// <summary>Gets or sets the SubAccount4 field. </summary>
        public string SubAccount4 { get; set; }

        /// <summary>Gets or sets the SubAccountID5 field. </summary>
        public long? SubAccountId5 { get; set; }


        /// <summary>Gets or sets the SubAccount5 field. </summary>
        public string SubAccount5 { get; set; }

        /// <summary>Gets or sets the SubAccountID6 field. </summary>
        public long? SubAccountId6 { get; set; }


        /// <summary>Gets or sets the SubAccount6 field. </summary>
        public string SubAccount6 { get; set; }

        /// <summary>Gets or sets the SubAccountID7 field. </summary>
        public long? SubAccountId7 { get; set; }


        /// <summary>Gets or sets the SubAccount7 field. </summary>
        public string SubAccount7 { get; set; }

        /// <summary>Gets or sets the SubAccountID8 field. </summary>
        public long? SubAccountId8 { get; set; }


        /// <summary>Gets or sets the SubAccount8 field. </summary>
        public string SubAccount8 { get; set; }

        /// <summary>Gets or sets the SubAccountID9 field. </summary>
        public long? SubAccountId9 { get; set; }


        /// <summary>Gets or sets the SubAccount9 field. </summary>
        public string SubAccount9 { get; set; }

        /// <summary>Gets or sets the SubAccountID10 field. </summary>
        public long? SubAccountId10 { get; set; }


        /// <summary>Gets or sets the SubAccount10 field. </summary>
        public string SubAccount10 { get; set; }

        /// <summary>Gets or sets the TypeOf1099T4ID field. </summary>
        public Typeof1099T4? TypeOf1099T4Id { get; set; }


        /// <summary>Gets or sets the TypeOf1099T4 field. </summary>
        public string TypeOf1099T4 { get; set; }

        /// <summary>Gets or sets the Amount field. </summary>
        public decimal? Amount { get; set; }

        /// <summary>Gets or sets the CompanyCurrencyAmount field. </summary>
        public decimal? CompanyCurrencyAmount { get; set; }

        /// <summary>Gets or sets the CurrencyAdjustmentAmount field. </summary>
        public decimal? CurrencyAdjustmentAmount { get; set; }

        /// <summary>Gets or sets the OriginalItemID field. </summary>
        public long? OriginalItemId { get; set; }

        /// <summary>Gets or sets the OriginalItem field. </summary>
        public string OriginalItem { get; set; }

        /// <summary>Gets or sets the AccountingItemIDLink field. </summary>
        public long? AccountingItemIdLink { get; set; }

        /// <summary>Gets or sets the AccountingItemLink field. </summary>
        public string AccountingItemLink { get; set; }

        /// <summary>Gets or sets the IsChanged field. </summary>
        public bool IsChanged { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusID field. </summary>
        public TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatus field. </summary>
        public string TypeOfInactiveStatus { get; set; }

        /// <summary>Gets or sets the ReconciliationID field. </summary>
        public int? ReconciliationId { get; set; }

        /// <summary>Gets or sets the Reconciliation field. </summary>
        public string Reconciliation { get; set; }

        /// <summary>Gets or sets the IsEnterable field. </summary>
        public bool IsEnterable { get; set; }

        /// <summary>Gets or sets the AccountingItemOrigAmount field. </summary>
        public decimal? AccountingItemOrigAmount { get; set; }

        /// <summary>Gets or sets the AccountingItemTypeOfModificationID field. </summary>
        public int? AccountingItemTypeOfModificationId { get; set; }

        /// <summary>Gets or sets the AccountingItemTypeOfModification field. </summary>
        public string AccountingItemTypeOfModification { get; set; }

        /// <summary>Gets or sets the AccountingItemOrigID field. </summary>
        public long? AccountingItemOrigId { get; set; }

        /// <summary>Gets or sets the AccountingItemOrig field. </summary>
        public string AccountingItemOrig { get; set; }

        /// <summary>Gets or sets the ICTJobID field. </summary>
        public int? IctJobId { get; set; }

        /// <summary>Gets or sets the IctJob field. </summary>
        public string IctJob { get; set; }

        /// <summary>Gets or sets the ICTAccountingItemID field. </summary>
        public long? IctAccountingItemId { get; set; }

        /// <summary>Gets or sets the IctAccountingItem field. </summary>
        public string IctAccountingItem { get; set; }


        /// <summary>Gets or sets the TaxRebateID field. </summary>
        public int? TaxRebateId { get; set; }

        /// <summary>Gets or sets the TaxRebate field. </summary>
        public string TaxRebate { get; set; }

        /// <summary>Gets or sets the CurrencyOverrideRate field. </summary>
        public double? CurrencyOverrideRate { get; set; }

        /// <summary>Gets or sets the FunctionalCurrencyAmount field. </summary>
        public decimal? FunctionalCurrencyAmount { get; set; }

        /// <summary>Gets or sets the TypeOfCurrencyRateID field. </summary>
        public short? TypeOfCurrencyRateId { get; set; }

        /// <summary>Gets or sets the TypeOfCurrencyRate field. </summary>
        public string TypeOfCurrencyRate { get; set; }

        /// <summary>Gets or sets the TypeOfCurrencyID field. </summary>
        public short? TypeOfCurrencyId { get; set; }

        /// <summary>Gets or sets the TypeOfCurrency field. </summary>
        public string TypeOfCurrency { get; set; }

        /// <summary>Gets or sets the HomeCurAmount field. </summary>
        public decimal? HomeCurAmount { get; set; }

        /// <summary>Gets or sets the CustomForexRate field. </summary>
        public decimal? CustomForexRate { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long OrganizationUnitId { get; set; }

        
    }
}
